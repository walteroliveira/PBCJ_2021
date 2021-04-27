/// <summary>
/// Script para realizar o spawn de personagem 
/// Repete o spawn a cada periodo de tempo
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnPrefab;          // Prefab de spawn 
    public float repeatInterval;            // Intervalo de repetição
    
    void Start()
    {
        if (repeatInterval > 0)     // Se o tempo de repetição for maior que zero
        {
            InvokeRepeating("Spawn0", 0.0f, repeatInterval);    // Chama a função de spawn a cada intervalo de tempo
        }
    }

    /* Função que spawna o personagem
     * Se o prefab de spawn não for nulo
     * Instancia o prefab de spawn
     */
    public GameObject Spawn0()
    {
        if(spawnPrefab != null)
        {
            return Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        }
        return null;
    }
}
