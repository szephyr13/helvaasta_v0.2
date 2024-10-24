using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenu : MonoBehaviour
{

    public Animator transition; 
    public float transitionTime = 1f;

    //para playgame se para la música, se reproduce un SFX y se carga la escena 1
    public void PlayGame () {
        AudioManager.instance.BGMSource.Stop();
        AudioManager.instance.PlaySFX("Menu-PlayGame");
        StartCoroutine(LoadScene(1));
    }

    //para el menú de opciones se reproduce el click normal (el SetActive está indicado en el propio objeto del menú)
    public void OptionsMenu () {
        AudioManager.instance.PlaySFX("UI-Click");
    }

    //para salir del juego se para la música, se reproduce un efecto, se inicia la transición y se sale del juego
    public void ExitGame () {
        AudioManager.instance.BGMSource.Stop();
        AudioManager.instance.PlaySFX("Menu-ExitGame");
        transition.SetTrigger("Start");
        Application.Quit();
    }

    //Coroutine reutilizada para cargar otra escena, pasando transición, esperando el tiempo especificado y cargando la escena especificada
    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
