using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//VARIACIÓN DE DIALOGUEONMAP PARA LA CASA DE KAIDA
//Solo se necesita para que la conversación suceda una sola vez 
public class DialogueKaida : MonoBehaviour
{
    private bool rangeTrigger;
    public bool firstEntrance;
    private bool didDialogueStart = false;
    private int lineIndex;
    private float textSpeed = 0.05f;

    public GameObject dialogueUI;
    public GameObject characterPlaceholder;
    public TMP_Text characterName;
    public TMP_Text sentenceField;
    public GameObject nextArrow;
    public ConversationPart[] initialConversation;

    //Según se empieza, firstEntrance indica que todavía no se ha hablado
    void Awake() {
        firstEntrance = true;
    }

    //el jugador inicia la conversación si está en el trigger y todavía no ha hablado con Kaida
    //se tiene en cuenta si la frase está terminada de escribir o no
    void Update(){
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
        }
    }

    //Corrutina para mostrar poco a poco la frase, con efecto de tipeo.
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


    //funciones para detectar el trigger
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
