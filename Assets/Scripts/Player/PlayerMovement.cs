using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    //función para debug, para comprobar la última escena
    private void Start(){
        Debug.Log("PlayerMovement knows that last scene was " + ChangeScene.lastSceneIndex);
    }

    //al iniciar, se leen los componentes del jugador relativos a Rigidbody y la animación de movimiento
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //en cada update se leen las entradas de controles y se responde con la animación y el movimiento adecuados
    private void Update(){
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb.velocity = movement * moveSpeed;
        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);

        if (movement != Vector2.zero){
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical, movement.y);
        }
    }
}
