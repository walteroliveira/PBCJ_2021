/// <summary>
/// Script que realiza a movimentação do personagem
/// Realiza a leitura das entradas horizontal e vertical e atualiza a posição e a animação do player
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public float MovimentSpeed = 3.0f;          // Impulso de movimento a ser dado no player
    Vector2 Moviment = new Vector2();           // Detectar movimento pelo teclado
    Animator Animator;                          // Guarda o componente de Controlador de Animação
    // string AnimationState = "AnimationState";   // Variável que garda o nome do parâmetro de animação
    Rigidbody2D Rb2D;                           // Guarda o RigidBody2D do player


  /*  enum CharacterState
    {
        east  = 1,
        west  = 2,
        north = 3,
        south = 4,
        idle  = 5
    }
  */

    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();     // Pega o componente rb2d do player
        Animator = GetComponent<Animator>();    // Pega o componente de animação do player
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();      // Atualiza a animação
    }
    private void FixedUpdate()
    {
        MoveCharacter();    // Move o player
    }

    /* Função para mover o player
     * Pega o input das teclas de locomoção do player, normaliza entre 0 ou 1
     * Muda a velocidade do Rigidbody com o valor do movimento mais a velocidade de movimento do player
     * 
     */
    private void MoveCharacter()
    {
        Moviment.x = Input.GetAxisRaw("Horizontal");
        Moviment.y = Input.GetAxisRaw("Vertical");
        Moviment.Normalize();
        Rb2D.velocity = Moviment * MovimentSpeed;    
    }
    /* Função que atualiza a animação
     * Se o movimento do player estiver próximo de 0 nos dois eixos, seta a variavel de animação andando para falso
     * Caso contrario, seta para 1
     * Muda as variaveis de direção de acordo com o valor do movimento 
     */
    void UpdateState()
    {
        if (Mathf.Approximately(Moviment.x, 0) && (Mathf.Approximately(Moviment.y, 0)))
        {
            Animator.SetBool("Walking", false);
        }
        else
        {
            Animator.SetBool("Walking", true);
        }
        Animator.SetFloat("dirX", Moviment.x);
        Animator.SetFloat("dirY", Moviment.y);
    }
    /*
    private void UpdateState()
    {


        
        if (Moviment.x > 0)
        {
            Animator.SetInteger(AnimationState, (int)CharacterState.east);
        }
        else if(Moviment.x < 0)
        {
            Animator.SetInteger(AnimationState, (int)CharacterState.west);

        }
        else if (Moviment.y > 0)
        {
            Animator.SetInteger(AnimationState, (int)CharacterState.north);

        }
        else if (Moviment.y < 0)
        {
            Animator.SetInteger(AnimationState, (int)CharacterState.south);

        }
        else
        {
            Animator.SetInteger(AnimationState, (int)CharacterState.idle);
        }
    }*/
}
