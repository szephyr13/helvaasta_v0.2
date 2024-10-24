using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput playerInput;
    private InputAction moveAction;

    //al iniciarse, se toma el componente PlayerImput y se estipulan en componente las acciones categorizadas con la string "Move"
    private void Awake(){
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    //cada carga se lee el vector resultante de los controles
    private void Update(){
        Movement = moveAction.ReadValue<Vector2>();
    }

}
