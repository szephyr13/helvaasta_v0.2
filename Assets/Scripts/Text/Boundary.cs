using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boundary : MonoBehaviour
{
    private bool rangeTrigger;
    private bool timeToGo;
    private bool didDialogueStart = false;
    private int lineIndex;
    private float textSpeed = 0.05f;

    public GameObject dialogueUI;
    public GameObject characterPlaceholder;
    public TMP_Text characterName;
    public TMP_Text sentenceField;
    public GameObject nextArrow;
    public ConversationPart[] boundaryInteraction;


    //VARIANTE DE DIALOGUEONMAP
    //al inicio se activan todas las conversaciones (timeToGo las bloquea para que el jugador pueda salir del área de trigger)
    void Awake() {
        timeToGo = false;
    }

    //si se timeToGo es true, no pasa nada. En cambio, si es false, se inicia el diálogo
    //el diálogo se inicia si se está en el rango del trigger. Se tiene en cuenta si se ha iniciado el diálogo, si se ha acabado de escribir el texto o si todavía no
    void Update(){
        if (timeToGo){
        } else {
            if (rangeTrigger) {
                if (!didDialogueStart){
                    StartDialogue();
                } else if (sentenceField.text == boundaryInteraction[lineIndex].currentSentence) {
                    nextArrow.SetActive(true);
                    if (Input.GetButtonDown("Fire1")){
                        AudioManager.instance.PlaySFX("UI-Click");
                        NextDialogueLine();
                    }
                } else {
                    nextArrow.SetActive(false);
                    if (Input.GetButtonDown("Fire1")){
                        AudioManager.instance.PlaySFX("UI-Click");
                        StopAllCoroutines();
                        sentenceField.text = boundaryInteraction[lineIndex].currentSentence;
                    }
                }
            }
        }
    }

    //StartDialogue informa de que el diálogo ha empezado, activa la UI de diálogo, para el tiempo fuera de la conversación y empieza a mostrar las líneas de cero
    private void StartDialogue() {
        didDialogueStart = true;
        dialogueUI.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    //se va iterando entre las líneas cuando todavía las hay. si se acaban, se revierte el estado del diálogo a un punto anterior a StartDialogue.
    //la variable timeToGo se activa en cuanto acaba el diálogo para que el personaje pueda salir de la zona de trigger
    private void NextDialogueLine(){
        lineIndex++;
        if (lineIndex < boundaryInteraction.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialogueUI.SetActive(false);
            Time.timeScale = 1f;
            timeToGo = true;
        }
    }

    //Coroutine para mostrar según la información de diálogo lo que hay que mostrar en la UI, haciendo un efecto de tipeo y "creando" la voz del personaje. 
    private IEnumerator ShowLine(){
        characterName.text = boundaryInteraction[lineIndex].characterName;
        characterPlaceholder.GetComponent<Image>().sprite = boundaryInteraction[lineIndex].faceSprite;
        sentenceField.text = string.Empty;

        foreach (char ch in boundaryInteraction[lineIndex].currentSentence) {
            sentenceField.text += ch;
            AudioManager.instance.PlaySFX("Voice-" + boundaryInteraction[lineIndex].characterName);
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }


    //funciones para detectar si el personaje está o no en la zona del collider
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            rangeTrigger = true;
        }
    }

    //una vez se sale de la zona de trigger, timeToGo vuelve a activar el script por si el jugador decide volver
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            rangeTrigger = false;
            timeToGo = false;
        }
    }
}

