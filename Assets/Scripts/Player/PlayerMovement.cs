using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3SO nemoMapPosition;
    [SerializeField] private GameObject pauseMenu;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    //al iniciar, se leen los componentes del jugador relativos a Rigidbody y la animación de movimiento
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.gameObject.transform.position = nemoMapPosition.Value;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf && Time.timeScale != 0)
            {
                pauseMenu.SetActive(true);
                pauseMenu.GetComponentInParent<PauseMenu>().UpdateLog();
                Time.timeScale = 0f;
            } else if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
