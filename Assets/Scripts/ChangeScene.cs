using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneBuildIndex; //numero de escena
    public Animator transition; //interaccion con el Animator
    public float transitionTime = 1f; //segundos que tarda la escena en cargar tras la animacion

    public static int lastSceneIndex; //última escena jugada
    

    //Cuando el jugador entra en la zona de trigger, se carga la escena especificada
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            StartCoroutine(LoadScene(sceneBuildIndex));
        }
    }

    //Al cargar la escena se espera una cantidad de segundos estipulada en el complemento
    IEnumerator LoadScene(int levelIndex){
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex; //guardo escena anterior
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
