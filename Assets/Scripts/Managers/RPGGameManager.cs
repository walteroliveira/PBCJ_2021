/// <summary>
/// Script de manager do jogo
/// Realiza os spawns dos inimigos e do player
/// Realiza a mudança de cena ao player pegar todos os coletaveis
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;     // instancia do game manager 
    public SpawnPoint playerSpawnPoint;                     // Objeto para o spawn do player
    public SpawnPoint enemySpawnPoint;                      // Objeto para o spawn do inimigo
    public RPGCameraManager cameraManager;                  // Objeto da camera
    public string newScene;                                 // String para a nova cena

    /* Ao iniciar, verifica se um game manager que não for essa já foi instanciada
    * Caso tenha outra game manager, destroi essa instância,
    * Caso contrario mantem esse manager
    */
    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
            
        }
    }
    void Start()
    {

        SetupScene();   // Inicia a cena
    }


    public void SetupScene()
    {
        SpawnPlayer();  // Realiza o spawn do player
        SpawnEnemy();   // Realiza o spawn do inimigo
    }
    /* Função para o spawn do player
     * Se o objeto que mantem o spawm não for vazio
     * Realiza o spawn do player e manda a camera seguí-lo 
     */
    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.Spawn0();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
    /* Função para o spawn do player
    * Se o objeto que mantem o spawm não for vazio
    * Realiza o spawn do inimigo
    */
    public void SpawnEnemy()
    {
        if (enemySpawnPoint != null)
        {
            GameObject enemy = enemySpawnPoint.Spawn0();

        }
    }
    /* Verifica se todos os coletaveis já foram pegos
     * Gera uma lista com todos os coletaveis na hierarquia
     * Se o tamanho da lista for igual a zero e tenha uma nova cena para mudar
     * Muda para a cena
     */
    void FixedUpdate()
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");

        Debug.Log(collectables.Length);
        if (collectables.Length == 0 && newScene != null)
        {            
            ChangeScene(newScene);
        }
    }

    private void ChangeScene(string newScene)
    {
        
        SceneManager.LoadScene(newScene);   // Carrega a cena
    }

}
