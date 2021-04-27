/// <summary>
/// Script responsável pelo manager das armas do player
/// Ao clicar com o botão do mouse, realiza o tiro 
/// Verifica a posição que ele está mirando para chamar a animação de ataque correta/// 
/// </summary>
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Weapons : MonoBehaviour
{
    public GameObject ammoPrefab;       // armazena o prefab de Ammo
    static List<GameObject> ammoPool;   // Pool de Ammo
    public int poolLenght;              // Tamanho da pool
    public float speedWeapon;           // velocidade da arma
    bool shooting;                      // armazena se está atirando
    [HideInInspector]
    public Animator animator;           // armazena o animator do playuer
    Camera cameraLocation;              // armazena a posição da camera
    float positiveSlope;                // varivavel para realizar o calculo do quadrante
    float negativeSlope;                // varivavel para realizar o calculo do quadrante
    enum Quadrant                       // Lista de quadrantes possíveis
    {
        East,
        South,
        North,
        West
    }
    /* Ao iniciar, seta shooting como falso, pega o componente de animação e os extremos da tela
     * Após isso, calcula a inclinação positiva e negativa para verificar o quadrante 
     */
    private void Start()
    {
        animator = GetComponent<Animator>();
        shooting = false;
        cameraLocation = Camera.main;
        Vector2 belowLeft = cameraLocation.ScreenToWorldPoint(new Vector2(0, 0));                           // Esquerda baixo
        Vector2 aboveRight = cameraLocation.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));   // Direita cima
        Vector2 aboveLeft = cameraLocation.ScreenToWorldPoint(new Vector2(0, Screen.height));               // Esquerda cima
        Vector2 belowRight = cameraLocation.ScreenToWorldPoint(new Vector2(Screen.width, 0));               // Direita baixo
        positiveSlope = GetSlope(belowLeft, aboveRight);
        negativeSlope = GetSlope(aboveLeft, belowRight);
    }
    bool AbovePositiveSlope(Vector2 posInput)
    {
        Vector2 posPlayer = gameObject.transform.position;
        Vector2 posMouse = cameraLocation.ScreenToWorldPoint(posInput);
        float interceptionY = posPlayer.y - (positiveSlope * posPlayer.x);
        float inputInterception = posMouse.y - (positiveSlope * posPlayer.x);
        return inputInterception > interceptionY;
    }
    bool AboveNegativeSlope(Vector2 posInput)
    {
        Vector2 posPlayer = gameObject.transform.position;
        Vector2 posMouse = cameraLocation.ScreenToWorldPoint(posInput);
        float interceptionY = posPlayer.y - (negativeSlope * posPlayer.x);
        float inputInterception = posMouse.y - (negativeSlope * posPlayer.x);
        return inputInterception > interceptionY;
    }

    Quadrant GetQuadrant()
    {
        Vector2 posMouse = Input.mousePosition;
        Vector2 posPlayer = transform.position;
        bool abovePositiveSlope = AbovePositiveSlope(Input.mousePosition);
        bool aboveNegativeSlope = AboveNegativeSlope(Input.mousePosition);
        if(!abovePositiveSlope && aboveNegativeSlope)
        {
            return Quadrant.East;
        }
        if (!abovePositiveSlope && !aboveNegativeSlope)
        {
            return Quadrant.South;
        }
        if (abovePositiveSlope && !aboveNegativeSlope)
        {
            return Quadrant.West;
        }
        else
        {
            return Quadrant.North;

        }    
    }
    void UpdateState()
    {
        if (shooting)
        {
            Vector2 vectorQuadrant;
            Quadrant  quadrantEnum = GetQuadrant();
            switch (quadrantEnum)
            {
                case Quadrant.East:
                    vectorQuadrant = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrant.South:
                    vectorQuadrant = new Vector2(0.0f, -1.0f);
                    break;
                case Quadrant.West:
                    vectorQuadrant = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrant.North:
                    vectorQuadrant = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    vectorQuadrant = new Vector2(0.0f, 0.0f);
                    break;
            }
            animator.SetBool("Shooting", true);
            animator.SetFloat("shootX", vectorQuadrant.x);
            animator.SetFloat("shootY", vectorQuadrant.y);
            shooting = false;
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }
    public void Awake()
    {
        if(ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }
        for (int i = 0; i < poolLenght; i++)
        {
            GameObject ammoO = Instantiate(ammoPrefab);
            ammoO.SetActive(false);
            ammoPool.Add(ammoO);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            TriggerAmmo();
        }
        UpdateState();
    }

    float GetSlope(Vector2 point1, Vector2 point2)
    {
        return (point2.y - point1.y) / (point2.x - point1.x);
    }
    public GameObject SpawnAmmo(Vector3 pos)
    {
        foreach(GameObject ammo in ammoPool)
        {
            if(ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = pos;
                return ammo;
            }
        }
        return null;
    }
    void TriggerAmmo()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);
        if( ammo != null)
        {
            Arc arcScript = ammo.GetComponent<Arc>();
            float durationTrajectory = 1.0f / speedWeapon;
            StartCoroutine(arcScript.arcTrajectory(mousePos, durationTrajectory));
        }
    }
    private void OnDestroy()
    {
        ammoPool = null;
    }
}
