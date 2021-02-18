using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private int numTentativas;      // inteiro para armazenar o número de tentativas
    private int maxNumTentativas;   // inteiro para armazenar o número máximo de tentativas
    private int score;              // inteiro para armazenar a pontuação do usuário

    public GameObject letra;        // Game Object que gera uma letra para a forca
    public GameObject centro;       // Game Object para armazenar o centro da tela

    private string palavraOculta = "";      // Inicialização da palavra oculta a ser  descoberta pelo usuário
    //private string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" };   // Utilizado nas etapas anteriores do LAB

    private int tamanhoPalavraOculta;       // Inteiro para armazenar o tamanho da palavra oculta
    char[] letrasOcultas;                   // Array de caracteres que armazena as letras ainda não descobertas pelo usuário
    bool[] letrasDescobertas;               // Array que armazena as letras descobertas pelo usuário
     
    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");   // Encontra o centro da tela a partir do GameObject criado no Unity
        InitGame();                                 // Chamada da função que inicia o jogo
        InitLetras();                               // Chamada da função que inicia as letras da palavra oculta
        numTentativas = 0;                          // Reset do número de tentativas
        maxNumTentativas = 10;                      // Limite máximo de tentativas 
        UpdateNumTentativas();                      // Atualiza a UI com o número de tentativas
        UpdateScore();                              // Atualiza a UI com a pontuação
    }

    // Update is called once per frame
    void Update()
    {
        CheckTeclado();                             // Verifica se alguma tecla do teclado foi apertada a cada frame
    }   

    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;       // numLetras recebe o tamanho da Palavra Oculta
        for (int i = 0; i < numLetras; i++)         // Para cada letra na palavra oculta
        {   
            Vector3 novaPosicao;                    // Cria um vetor de 3 dimensões para armazenar a posição da letra
            novaPosicao = new Vector3(centro.transform.position.x + ((i - numLetras/2.0f)*80), centro.transform.position.y, centro.transform.position.z);   // Vetor novaPosicao transforma seus valores de x, y e z
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);        // Instancia um game object para a nova letra com na posição contida em novaPosicao
            l.name = "letra" + (i + 1);                                                             // Nomeia a letra 
            l.transform.SetParent(GameObject.Find("Canvas").transform);                             // Transforma a letra como filho do GameObject Canvas
        }
        
    }

    void InitGame()
    {
        //palavraOculta = "Elefante";                                       // Utilizado em etapas anteriores do LAB
        //int numeroAleatorio = Random.Range(0, palavrasOcultas.Length);    // Utilizado em etapas anteriores do LAB
        //palavraOculta = palavrasOcultas[numeroAleatorio];                 // Utilizado em etapas anteriores do LAB

        palavraOculta = PegaUmaPalavraDoArquivo();                          // Armazena em palavraOculta uma palavra aleatória do arquivo texto
        tamanhoPalavraOculta = palavraOculta.Length;                        // Armazena o tamanho da palavraOculta
        palavraOculta = palavraOculta.ToUpper();                            // Muda os caracteres da palavraOculta para maiúsculo
        letrasOcultas = new char[tamanhoPalavraOculta];                     // Cria um array de caracteres com o tamanho da palavraOculta
        letrasDescobertas = new bool[tamanhoPalavraOculta];                 // Cria um array de booleanos com o tamanho da palavraOculta
        letrasOcultas = palavraOculta.ToCharArray();                        // Coloca em letrasOcultas os caracteres de palavraOculta
    }

    void CheckTeclado()
    {
        if (Input.anyKeyDown)                                               // Caso qualquer tecla seja acionada
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];         // Armazena a tecla acionada
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada); // Converte a tecla acionada para inteiro

            if (letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)    // Se a tecla estiver entre o intervalo de 'A' até 'Z' em ASCII
            {
                numTentativas++;                                            // Incrementa o número de tentativas
                UpdateNumTentativas();                                      // Atualiza a UI de tentativas
                if (numTentativas > maxNumTentativas)                       // Se o número de tentativas for maior que o máximo do número de tentativas
                {
                    SceneManager.LoadScene("Lab1_forca");                   // Troca para a cena de derrota
                }
                for (int i = 0; i < tamanhoPalavraOculta; i++)              // Para cada letra da palavraOculta
                {
                    if (!letrasDescobertas[i])                              // Se a letra ainda não foi descoberta
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);   // Converte a tecla acionada para maiúscula
                        if (letrasOcultas[i] == letraTeclada)               // Se a letra oculta foi a letra acionada
                        {
                            letrasDescobertas[i] = true;                    // Muda a posição da letraDescoberta para true
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString();     // Mostra no jogo a letra descoberta na posição equivalente
                            score = PlayerPrefs.GetInt("score");            // Coloca em score o valor atual do score do jogador
                            score++;                                        // Incrementa o valor do score
                            PlayerPrefs.SetInt("score", score);             // Armazena no score do jogador o novo valor de score
                            UpdateScore();                                  // Atualiza a UI de score
                            VerificaSePalavraDescoberta();                  // Verifica se todas as letras da palavraOculta foram descobertas
                        }
                    }
                }
            }

        }
    }
    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxNumTentativas;  // Atualiza um Text no Unity com o número atual de tentativas e o máximo de tentativas
    }

    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score " + score;    // Atualiza no Text scoreUI o valor atual do score
    }
    void VerificaSePalavraDescoberta()
    {
        bool condicao = true;                                                       // Seta condicao como true
        for (int i = 0; i < tamanhoPalavraOculta; i++)                              // Para cada letra na palavra oculta
        {
            condicao = condicao && letrasDescobertas[i];                            // Armazena em condicao a operação AND condicao e cada posicao de letraDescoberta
        }
        if (condicao)                                                               // Se condicao se mantiver como true
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);            // Seta em ultimaPalavraOculta do jogador a palavraOculta da partida
            SceneManager.LoadScene("Lab1_salvo");                                   // Muda para a cena de vitória
        }
    }
    string PegaUmaPalavraDoArquivo()                                    
    {
        TextAsset t1 = (TextAsset)Resources.Load("palavras", typeof(TextAsset));    // Armazena em t1 o contéudo do resource palavras
        string s = t1.text;                                                         // Armazena na string s o texto de t1
        string[] palavras = s.Split(' ');                                           // Armazena no array de strings palavras o conteúdo de s separado por espaço
        int palavraAleatoria = Random.Range(0, palavras.Length);                    // Armazena um palavraAleatoria um numero de 0 até o número de palavras do texto
        return (palavras[palavraAleatoria]);                                        // Retorna a palavra aleatória
    }
}
