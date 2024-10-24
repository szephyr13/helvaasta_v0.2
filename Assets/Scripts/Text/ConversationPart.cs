using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//declaración de la clase ConversationPart para tener todos los datos variables de los diálogos y así completar la UI
[System.Serializable]
public class ConversationPart
{
    public string characterName;
    public Sprite faceSprite;
    [TextArea(3,10)]
    public string currentSentence;
}
