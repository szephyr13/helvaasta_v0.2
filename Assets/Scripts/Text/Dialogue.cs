using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//declaración de la clase Dialogue para el método utilizado en la escena de introducción
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
    
}
