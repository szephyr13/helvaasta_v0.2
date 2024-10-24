using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksForPlaying : MonoBehaviour
{

    public Animator transition; 
    public float transitionTime = 1f;

    //en el botón de ir a la pantalla de inicio se reproduce un sonido tras parar la banda sonora. 
    public void StartScreen () {
        AudioManager.instance.BGMSource.Stop();
        AudioManager.instance.PlaySFX("Menu-PlayGame");
        StartCoroutine(LoadScene(0));
    }

    //en el botón de salir se para tod y se sale de la aplicación 
    //en el futuro se podría aplicar una Coroutine para que suene el efecto de sonido
    public void ExitGame () {
        AudioManager.instance.BGMSource.Stop();
        AudioManager.instance.PlaySFX("Menu-ExitGame");
        transition.SetTrigger("Start");
        Application.Quit();
    }

    //Coroutine para aplicar efecto de Crossfade al cargar la escena indicada
    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
