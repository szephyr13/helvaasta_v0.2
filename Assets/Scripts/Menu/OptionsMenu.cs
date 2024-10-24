using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    public Button TestButton;

    //se asocia el volumen de BGM con el slider
    public void SetBGMVolume () {
        AudioManager.instance.BGMVolume(BGMSlider.value);
    }

    //se asocia el volumen de SFX con el slider
    public void SetSFXVolume () {
        AudioManager.instance.SFXVolume(SFXSlider.value);
    }

    //se determina la función de pantalla completa
    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    //se asocia un efecto de sonido al botón de prueba de SFX
    public void TestOnClick () {
        AudioManager.instance.PlaySFX("Menu-ExitGame");
    }
}


/* ANTERIOR CON AUDIOMIXER

public AudioMixer audioMixer;
    public void SetBGMVolume (float volume) {
        Debug.Log(volume);
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume (float volume) {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

*/