using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//VARIANTE DE DIALOGUEONMAP PARA LA ESCENA AUXILIAR
public class Thinking : MonoBehaviour
{
    private bool didDialogueStart = false;
    private bool timeToEnd;
    private int lineIndex;
    private float textSpeed = 0.05f;
    public Animator transition;

    public GameObject dialogueUI;
    public GameObject characterPlaceholder;
    public TMP_Text characterName;
    public TMP_Text sentenceField;
    public GameObject nextArrow;
    public GameObject interactButton;
    public ConversationPart[] interactionText;

    //se utiliza el timeToEnd para cambiar de escena
    void Awake(){
        timeToEnd = false;
    }

    //al pulsar el botón de interacción se inicia la conversación. se tiene en cuenta también si las líneas se han acabado o no para seguir con la siguiente frase
    //al terminar la interacción se deriva a la función EndGame
    void Update(){
        if (!timeToEnd){
            if (Input.GetButtonDown("Fire1")){
                if (!didDialogueStart){
                    AudioManager.instance.PlaySFX("UI-Click");
                    StartDialogue();
                } else if (sentenceField.text == interactionText[lineIndex].currentSentence) {
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
                        sentenceField.text = interactionText[lineIndex].currentSentence;
                    }
                }
            }
        } else{
            StartCoroutine(EndGame());
        }
    } 

    //inicia el dialogo con la corrutina de mostrar línea
    private void StartDialogue() {
        didDialogueStart = true;
        dialogueUI.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    //se muestra la siguiente línea si hay. si no, indica el timeToEnd
    private void NextDialogueLine(){
        lineIndex++;
        if (lineIndex < interactionText.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            timeToEnd = true;
        }
    }

    //Corrutina para el efecto de tipeo
    private IEnumerator ShowLine(){
        characterName.text = interactionText[lineIndex].characterName;
        characterPlaceholder.GetComponent<Image>().sprite = interactionText[lineIndex].faceSprite;
        sentenceField.text = string.Empty;

        foreach (char ch in interactionText[lineIndex].currentSentence) {
            sentenceField.text += ch;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    //corrutina para la transición a la pantalla de fin.
    private IEnumerator EndGame(){
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(4);
    }

}

