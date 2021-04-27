/// <summary>
/// Script para o inimigo
/// Reinicia o inimigo
/// Verifica se colidiu com player e causa dano ao player
/// Mata o inimigo se a vida for igual a zero
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    float healthPoints;         // vida do inimigo
    public int forceDamage;     // dano do inimigo

    Coroutine damageCoroutine;  // corotina de causar dano

    // Ao ativar o objeto reinicia o inimigo
    private void OnEnable()
    {
        ResetCharactere();
    }
    /* Ao entrar em uma colisão
     * Se for com o player e houver uma corotina de dano
     * Causa dano ao inimigo 
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine == null)
            {
                Debug.Log("Hit enemy");
                damageCoroutine = StartCoroutine(player.DanoCharactere(forceDamage, 1.0f));
            }
        }
    }
    /* Ao sair da colisão com o player
     * Para a corotina de causar dano
     */
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                   
            }
        }
    }
    /* Corotina de causar dano herdada da classe Character
     * Inicia a corotina de piscar o inimigo
     * Diminui a vida do inimigo
     * Se a vida do inimigo chegar a zero, chama a função de matar o inimigo
     * Caso contrario, espera um tempo e termina a corotina
     */
    public override IEnumerator DanoCharactere(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            healthPoints = healthPoints - damage;
            if (healthPoints <= float.Epsilon)
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
    public override void ResetCharactere()
    {
        healthPoints = StartDamagePoints;   // Muda a vida atual para a vida inicial do inimigo
    }

}
