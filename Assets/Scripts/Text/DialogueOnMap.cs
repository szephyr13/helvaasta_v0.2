using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//script para la conversación con los guardias en la escena 2
public class DialogueOnMap : MonoBehaviour
{
    private bool rangeTrigger;
    public bool firstEntrance;
    private bool timeToGo;
    public bool nextInteraction;
    private bool didDialogueStart = false;
    private int lineIndex;
    private float textSpeed = 0.05f;

    public GameObject dialogueUI;
    public GameObject characterPlaceholder;
    public TMP_Text characterName;
    public TMP_Text sentenceField;
    public GameObject nextArrow;
    public ConversationPart[] initialConversation;
    public ConversationPart[] downBoundary;

    //al iniciar, se indica que se va a tener la primera conversación (firstEntrance) y que se iniciará la conversación según se entre en la zona de trigger (timeToGo)
    void Awake() {
        firstEntrance = true;
        nextInteraction = false;
        timeToGo = false;
    }

    //se comprueba si se tiene que iniciar la conversación (timeToGo) y, dependiendo de si es la primera conversación o la segunda, se utiliza una función u otra 
    //la diferencia se aprecia en la primera línea tras el else if (nextInteraction&&rangeTrigger) - en próximas versiones se optimizará
    void Update(){
        if (timeToGo) {
        } else{
            if (rangeTrigger && firstEntrance) {
                if (!didDialogueStart){
                    StartDialogue();
                } else if (sentenceField.text == initialConversation[lineIndex].currentSentence) {
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
                        sentenceField.text = initialConversation[lineIndex].currentSentence;
                    }
                }
            } else if (nextInteraction && rangeTrigger) {
                initialConversation = downBoundary;
                if (!didDialogueStart){
                    StartDialogue();
                } else if (sentenceField.text == initialConversation[lineIndex].currentSentence) {
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
                        sentenceField.text = initialConversation[lineIndex].currentSentence;
                    }
                }
            }
        }    
    }

    //cuando el dialogo se inicia, se activa la UI, se para el tiempo y se empieza a escribir la frase desde cero
    private void StartDialogue() {
        didDialogueStart = true;
        dialogueUI.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    //si sigue habiendo líneas, las muestra. si no, declara que la conversación ya se ha tenido y devuelve la función al punto original
    private void NextDialogueLine(){
        lineIndex++;
        if (lineIndex < initialConversation.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialogueUI.SetActive(false);
            firstEntrance = false;
            Time.timeScale = 1f;
            timeToGo = true;
        }
    }

    ////Corrutina para mostrar poco a poco la frase, con efecto de tipeo.
    private IEnumerator ShowLine(){
        characterName.text = initialConversation[lineIndex].characterName;
        characterPlaceholder.GetComponent<Image>().sprite = initialConversation[lineIndex].faceSprite;
        sentenceField.text = string.Empty;

        foreach (char ch in initialConversation[lineIndex].currentSentence) {
            sentenceField.text += ch;
            AudioManager.instance.PlaySFX("Voice-" + initialConversation[lineIndex].characterName);
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }


    //funciones para detectar el trigger, teniendo en cuenta que la segunda detiene el inicio de la interacción si no se ha salido de la zona de trigger
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            rangeTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            rangeTrigger = false;
            timeToGo = false;
            if (firstEntrance == false){
                nextInteraction = true;
            }
        }
    }
}
