using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public audioFile[] SoundEffects, BackgroundMusic;
    public AudioSource BGMSource, SFXSource;
    public static AudioManager instance;
    public string backgroundTheme;

    //al iniciarse, si no hay un AudioManager, incluir este
    //se podría añadir en DontDestroyOnLoad, pero da problemas a la larga con los cambios de tema, por lo que se reserva para más adelante
    private void Awake(){
        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            //Destroy(gameObject);
            return;
        }
    }
    

    //tras Awake, se comienza el tema de fondo (diferenciando BGM de SFX)
    private void Start(){
        PlayBGM(backgroundTheme);
    }


    //para banda sonora, se busca un archivo de tipo AudioFile entre la Backgroundmusic con el nombre especificado
    //si no existe, se pasa aviso. si lo hace, se reproduce
    public void PlayBGM (string name) {
        audioFile s = Array.Find(BackgroundMusic, x => x.name == name);
        if (s == null) {
            Debug.LogWarning("Track " + name + " is missing.");
        } else {
            BGMSource.clip = s.clip;
            BGMSource.Play();
        }
    }

    //el proceso es similar a PlayBGM, solo que los efectos solo se reproducen una vez
    public void PlaySFX (string name) {
        audioFile s = Array.Find(SoundEffects, x => x.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " is missing.");
            return;
        } else {
            SFXSource.PlayOneShot(s.clip);
        }
    }

    //se asocia el volumen de BGM a la variable dentro del AudioSource determinado
    public void BGMVolume(float volume){
        BGMSource.volume = volume;
    }

    public void SFXVolume(float volume){
        SFXSource.volume = volume;
    }
}

/* ANTERIOR CON AUDIOMIXER

public audioFile[] SoundEffects, BackgroundMusic;
public static AudioManager instance;
public AudioSource BGMSource, SFXSource;
void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (audioFile s in SoundEffects){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (audioFile s in BackgroundMusic){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    void Start(){
        PlayBGM("MainTheme");
    }

    public void PlaySFX (string name) {
        audioFile s = Array.Find(SoundEffects, sound => sound.name == name);
        if (s == null) {
            Debug.Log (name + " SFX track was not found.");
            return;
        }
        s.source.Play();
    }

    public void PlayBGM (string name) {
        audioFile s = Array.Find(BackgroundMusic, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning (name + " BGM track was not found.");
            return;
        }
        s.source.Play();
    }
*/
