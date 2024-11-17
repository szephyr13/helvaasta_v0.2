using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//VARIANTE DE DIALOGUEONMAP PARA OBJETOS
public class HouseInteraction : MonoBehaviour
{
    private bool rangeTrigger;
    private bool didDialogueStart = false;
    private int lineIndex;
    private float textSpeed = 0.05f;

    [SerializeField] private GameObject telephoneGameObject;
    private TelephoneInteraction telephoneScript;

    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject characterPlaceholder;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text sentenceField;
    [SerializeField] private GameObject nextArrow;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private ConversationPart[] beforeTelephoneText;
    [SerializeField] private ConversationPart[] cantEnter;
    [SerializeField] private ConversationPart[] interactionText;


    private void Start()
    {
        telephoneScript = telephoneGameObject.GetComponent<TelephoneInteraction>();
    }

    //en cada update se comprueba si se ha pulsado la tecla de click
    //si se está en el rango de trigger de un objeto, la función inicia el cuadro de diálogo y tiene en cuenta si la frase se ha terminado de escribir o no
    void Update(){
        if (rangeTrigger) {
            //interactButton.SetActive(true);
            if (Input.GetButtonDown("Fire1")){
                if (!didDialogueStart){
                    AudioManager.instance.PlaySFX("UI-Click");
                    if (telephoneScript.alreadyInformed)
                    {
                        interactionText = cantEnter;
                    }
                    else
                    {
                        interactionText = beforeTelephoneText;
                    }
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
        if (lineIndex < interactionText.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialogueUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    //Corrutina para ir mostrando las letras con efecto de tipeo
    private IEnumerator ShowLine(){
        characterName.text = interactionText[lineIndex].characterName;
        characterPlaceholder.GetComponent<Image>().sprite = interactionText[lineIndex].faceSprite;
        sentenceField.text = string.Empty;

        foreach (char ch in interactionText[lineIndex].currentSentence) {
            sentenceField.text += ch;
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

