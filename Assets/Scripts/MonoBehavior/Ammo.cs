/// <summary>
/// Script para a utilização da munição do player
/// Verifica se  a bala atingiu um inimigo e causa dano nele
/// </summary>
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int dealtDamage;     // Dano causado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            //Debug.Log("Attack enemy");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();       // Pega o componente Enemy do enemy que houve a colisão
            StartCoroutine(enemy.DanoCharactere(dealtDamage, 0.0f));        // Inicia a corotina de dano
            gameObject.SetActive(false);                                    // Desativa a munição
        }
    }
}
