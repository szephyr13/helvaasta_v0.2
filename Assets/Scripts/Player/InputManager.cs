using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    [SerializeField] Vector3SO nemoMapPosition;
    [SerializeField] GameObject nemo;

    private PlayerInput playerInput;
    private InputAction moveAction;

    //al iniciarse, se toma el componente PlayerInput y se estipulan en componente las acciones categorizadas con la string "Move"
    private void Awake(){
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        this.gameObject.transform.position = nemoMapPosition.Value;
    }

    //cada carga se lee el vector resultante de los controles
    private void Update(){
        Movement = moveAction.ReadValue<Vector2>();
        nemoMapPosition.Value = nemo.transform.position;
    }

}
