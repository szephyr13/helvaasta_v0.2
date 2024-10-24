using UnityEngine.Audio;
using UnityEngine;


//declaración de la clase AudioFile para incluir todos los archivos de audio en el AudioManager

[System.Serializable]
public class audioFile
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;

}
