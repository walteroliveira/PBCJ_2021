/// <summary>
/// 23/04: Criação do código
/// 24/04: Adição da cena de crédito
/// Script para realizar o manager dos botões de menu
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Lab5_RPGSetup");    // Carrega a cena de inicio de jogo
    }
   
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");     // Carrega a cena de créditos
    }

    public void ExitGame()
    {
        Application.Quit();         // Fecha o jogo
    }
}
