/// <summary>
/// 11/08: Código criado
/// Script para realizar a trajetória de arco da munição/// 
/// </summary>
using System.Collections;
using UnityEngine;

public class Arc : MonoBehaviour
{
    /* Corotina para realizar o arco da munição
     * Recebe o alvo e o tempo até alcançar o alvo e calcula o arco necessário
     */
    public IEnumerator arcTrajectory(Vector3 target, float duration)
    {
        var initalPos = transform.position;     // Guarda a pos inicial do player
        var completePercentual = 0.0f;          // Variavel que armazena o tempo até o arco estar completo
        while(completePercentual < 1.0f)
        {
            completePercentual += Time.deltaTime / duration;        // Incrementa o tempo
            var currentHeight = Mathf.Sin(Mathf.PI * completePercentual);   // Caclula a altura de acordo com o tempo que falta para completar
            transform.position = Vector3.Lerp(initalPos, target, completePercentual) + Vector3.up*currentHeight ;   // Altera a posição da munição
            yield return null;
        }
        gameObject.SetActive(false);    // Destroi a munição ao terminar o arco
    }
}
