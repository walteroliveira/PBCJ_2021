/// <summary>
/// Script para realizar o passeio aleatório dos personagem
/// Verifica se ele pode seguir o player ou não
/// Altera a velocidade de acordo com o modo que o personagem está
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class WanderAround : MonoBehaviour
{
    public float followSpeed;               // Armazena a velocidade de perseguição
    public float wanderSpeed;               // Armazena a velocidade de idle do personagem
    float currentSpeed;                     // Armazena a velocidade atual
    public float changeDirectionInterval;   // Armazena o tempo de mudança de direação
    public bool followPlayer;               // Armazena se está perseguindo o player

    Coroutine MoveCoroutine;                // Corotina de mover o personagem

    Rigidbody2D rb2D;                   // Armazena o componente de rigidbody 
    Animator animator;                  // armazena o componente Animator

    Transform transformTarget = null;  // armazena o component transform do alvo

    Vector3 finalPosition;              // Vector3 da posiçao final do personagem
    float actualAngle = 0;              // Angulo atual da direção 

    CircleCollider2D circleCollider; // armazena o componente de spot

    /* Ao iniciar, pega o componente de animação
     * Muda a velocidade atual para a de perseguição
     * Pega o componente de rb2d e o circle collider
     * Inicia a corotina de passear
     */
    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = followSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // Função para debug para ver a o raio do circle collider
    private void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawSphere(transform.position, circleCollider.radius);
        }
    }
    /* Corotina para mover o inimigo 
    * Procura um novo ponto final e move o player
    * Espera por um tempo
    */
    public IEnumerator WanderRoutine()
    {
        while(true)
        {
            ChooseNewFinalPoint();
            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
            }
            MoveCoroutine = StartCoroutine(Move(rb2D, currentSpeed));
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }
    // Função que escolhe um novo ponto para se mover de maneira aleatoria
    void ChooseNewFinalPoint()
    {
        actualAngle += Random.Range(0, 360);
        actualAngle = Mathf.Repeat(actualAngle, 360);
        finalPosition += Vector3ForAngle(actualAngle);
    }
    // Função que retorna o angulo de movimento do inimigo
    Vector3 Vector3ForAngle(float actualAngleDegrees)
    {
        float actualAngleRadians = actualAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(actualAngleRadians), Mathf.Sin(actualAngleRadians), 0);
    }
    public IEnumerator Move(Rigidbody2D rbToMove, float speed)
    {
        float missedDistance = (transform.position - finalPosition).sqrMagnitude;
        while(missedDistance > float.Epsilon)
        {
            if(transformTarget != null)
            {
                finalPosition = transformTarget.position;
            }
            if(rbToMove != null)
            {
                animator.SetBool("Walking", true);
                Vector3 newPosition = Vector3.MoveTowards(rbToMove.position, finalPosition, speed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                //Debug.Log(finalPosition);

                missedDistance = (transform.position - finalPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Walking", false);
    }
    /* Ao entrar em colisão, caso seja um player que esteja perseguindo
    * Se for o boss, tocar a musica tema do boss
    * Muda a velocidade do inimigo e muda o target para a posicao do player
    * Inicia a corotina de movimento do inimigo 
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = followSpeed;
            transformTarget = collision.gameObject.transform;
            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
            }
            MoveCoroutine = StartCoroutine(Move(rb2D, currentSpeed));
        }
    }

    /* Ao parar de perseguir  um player
     * Seta a animação de andar para falso
     * Muda a velocidade para a de passeio
     * Para a corotina de se mover
     * Seta o alvo para nulo
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Walking", false);
            currentSpeed = wanderSpeed;
            if(MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
            }
            transformTarget = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rb2D.position, finalPosition, Color.red); // Mostra uma linha para a nova direção para debug
    }
}
}
