/// <summary>
/// Classe Character para ser heradada por outros personagens do jogo
/// Possui como métodos básicos resetar o personagem, tomar dano e morrer,
/// Possui como parâmetros básicos a vida máxima e a inicial
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //public int DamagePoints;
    //public int MaxDamagePoints;
    public float MaxDamagePoints;           // valor mínimo inicial de vida do personagem
    public float StartDamagePoints;         // valor máximo permitido de vida do personagem
    public abstract void ResetCharactere(); // Função para reiniciar as variaves do personagem
    
    /* Corotina que piscar o player ao receber dano
     * Muda a cor da sprite do player para vermelho, espera por 0.1s e muda para branco de novo
     */
    public virtual IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }
    public abstract IEnumerator DanoCharactere(int damage, float interval); // Corotina para causar dano ao personagem

    public virtual void KillCharactere()    // Função para matar o personagem
    {
        Destroy(gameObject);        // Destroi o game object do personagem
    }
}
