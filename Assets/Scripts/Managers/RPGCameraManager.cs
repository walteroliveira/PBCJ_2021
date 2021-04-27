/// <summary>
/// Script para configurar a camera do jogo
/// Utiliza como base o cinemachine
/// </summary>

using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager sharedInstance = null;   // variavel do tipo camera manager iniciada como nula
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;      // variavel da camera virtual cinemachine 

    /* Ao iniciar, verifica se uma camera que não for essa já foi instanciada
     * Caso tenha outra camera, destroi essa instância,
     * Caso contrario procura a virtual camera da hierarquia e pega o componente virtual camera 
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
        GameObject vCamGameObject = GameObject.FindWithTag("Virtual Camera");
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }

}
