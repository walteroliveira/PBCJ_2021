using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageBotoes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);             // Cria a variavél do jogador score, com o valor 0
    }

    public void StartMundoGame()
    {
        SceneManager.LoadScene("Lab1");             // Muda para a cena Lab 1
    }

    public void MostrarCreditos()
    {
        SceneManager.LoadScene("Lab1_creditos");    // Muda para a cena Lab1_creditos
    }

    public void FecharJogo ()
    {
        SceneManager.LoadScene("Lab1_final");       // Muda para a cena de agradecimento
        Application.Quit();                         // Finaliza o jogo
    }
}

