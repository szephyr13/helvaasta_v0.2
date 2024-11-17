using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//VARIANTE DE DIALOGUEONMAP PARA EL TELÉFONO
public class TelephoneInteraction : MonoBehaviour
{
    private bool rangeTrigger;
    private bool didDialogueStart = false;
    private int lineIndex;
    private float textSpeed = 0.05f;

    public GameObject dialogueUI;
    public GameObject characterPlaceholder;
    public TMP_Text characterName;
    public TMP_Text sentenceField;
    public GameObject nextArrow;
    public GameObject interactButton;
    public ConversationPart[] firstTelephone;
    public ConversationPart[] nextTelephone;
    public ConversationPart[] currentInteraction;

    //se determina que la interacción actual es la primera (al inicio)
    void Start(){
        currentInteraction = firstTelephone;
    }

    //en cada update se comprueba si se está en el rango de trigger y se pulsa el botón de interacción.
    //siendo así, se inicia el diálogo y se tiene en cuenta si el texto se ha terminado de escribir o no
    void Update(){
        if (rangeTrigger) {
            //interactButton.SetActive(true);
            if (Input.GetButtonDown("Fire1")){
                if (!didDialogueStart){
                    AudioManager.instance.PlaySFX("UI-Click");
                    StartDialogue();
                } else if (sentenceField.text == currentInteraction[lineIndex].currentSentence) {
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
                        sentenceField.text = currentInteraction[lineIndex].currentSentence;
                    }
                }
            }
        } else{
            //interactButton.SetActive(false);
        }
    } 

    //para comenzar el diálogo se indica, al a vez que se activa la UI y se llama a la correspondiente corrutina.
    private void StartDialogue() {
        didDialogueStart = true;
        dialogueUI.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    //se van añadiendo líneas, que se fragmentan con la corrutina hasta que se termina el repositorio de líneas
    private void NextDialogueLine(){
        lineIndex++;
        if (lineIndex < currentInteraction.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialogueUI.SetActive(false);
            Time.timeScale = 1f;
            currentInteraction = nextTelephone;
        }
    }

    //Corrutina para ir mostrando las letras con efecto de tipeo
    private IEnumerator ShowLine(){
        characterName.text = currentInteraction[lineIndex].characterName;
        characterPlaceholder.GetComponent<Image>().sprite = currentInteraction[lineIndex].faceSprite;
        sentenceField.text = string.Empty;

        foreach (char ch in currentInteraction[lineIndex].currentSentence) {
            sentenceField.text += ch;
            AudioManager.instance.PlaySFX("Voice-" + currentInteraction[lineIndex].characterName);
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }


    //funciones para detectar si el jugador está en el área de trigger
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            rangeTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            rangeTrigger = false;
        }
    }
}

