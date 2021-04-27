/// <summary>
/// Script para o controle do player do jogo
/// Inicia o inventario, barra de vida 
/// Atualiza a vida do player e verifica se ele tomou dano ou morreu
/// Verifica se encontrou algum coletavel
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Inventory inventoryPrefab;       // Referência ao objeto prefab criado do Inventario
    Inventory inventory;
    public HealthBar healthBarPrefab;       // Referência ao objeto prefab criado da HealthBar
    HealthBar healthBar;
    public DamagePoints damagePoints;       //  armazena o valor da vida do objeto

    /* Instancia, ao iniciar o jogo, o inventario e a barra de vida
     * Inicia o player com o numero de vida inicial
     */
    private void Start()
    {
        inventory = Instantiate(inventoryPrefab);
        damagePoints.value = StartDamagePoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }
    /* Corotina heradada da classe Character
     * Inicia a corrotina de piscar o player ao tomar dano
     * Diminui a vida do player
     * Verifica se a vida foi a zero, caso verdadeiro, mata o player
     * Caso contrario espera um pequeno intervalo e sai da corotina
     */
    public override IEnumerator DanoCharactere(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            damagePoints.value = damagePoints.value - damage;
            if (damagePoints.value <= float.Epsilon)
            {
                KillCharactere();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;

            }
        }
    }
    /* Função que reinicia o player
     * Instancia o inventario e a barra de vida
     * Muda a vida atual para a vida inicial 
     */
    public override void ResetCharactere()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        damagePoints.value = StartDamagePoints;
    }
    /* Função herdada da classe Character quando o player morre
     * Pega a função base e adiciona destruir o inventario e a barra da vida
     * Muda a cena para a cena de derrota
     */
    public override void KillCharactere()
    {
        base.KillCharactere();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
        SceneManager.LoadScene("GameOver");
    } 
    /* Função que verifica a colisão do player com um objeto com o trigger ativo
     * Se for um consumivel ou coletavel verifica o tipo de item e adiciona ao inventario ou muda a vida do personagem
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("Consumable"))
        {
            Item DamageObject = collision.gameObject.GetComponent<Consumable>().item;
            if(DamageObject != null)
            {
                bool Invisible = false;
                // print("Get " + DamageObject.ObjectName);
                switch (DamageObject.itemType)
                {
                    case Item.ItemType.COIN:
                        Invisible = inventory.AddItem(DamageObject);

                        break;
                    case Item.ItemType.DIAMOND:
                        Invisible = inventory.AddItem(DamageObject);

                        break;
                    case Item.ItemType.SAPPHIRE:
                        Invisible = inventory.AddItem(DamageObject);

                        break;
                    case Item.ItemType.HEALTH:
                        Invisible = DamagePointsUpdate(DamageObject.Quantity);
                        break;
                    default:
                        break;
                }
                if (Invisible)
                    Destroy(collision.gameObject);      // Destroi o objeto que colidiu
            }
        }
    }
    /* Função que atualiza a vida 
     *  Se a vida atual for menor que a vida maxima
     *  Atualiza a vida com a quantidade de vida adicionada
     *  Retorna verdadeiro
     */
    public bool DamagePointsUpdate(int quantity)
    {
        if (damagePoints.value < MaxDamagePoints)
        {
            damagePoints.value = damagePoints.value + quantity;
            print("Health Update by " + quantity + " New Health = " + damagePoints.value);
            return true;
        }
        else return false;
    }

}
