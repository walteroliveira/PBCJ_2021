using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraUltimaPalavraOculta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("ultimaPalavraOculta"); // Procura o conteúdo da última palavra oculta para mostrar na UI
    }


}
