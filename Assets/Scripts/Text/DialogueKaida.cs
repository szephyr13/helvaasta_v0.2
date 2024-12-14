using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//VARIACIÓN DE DIALOGUEONMAP PARA LA CASA DE KAIDA
//Solo se necesita para que la conversación suceda una sola vez 
public class DialogueKaida : MonoBehaviour
{
    private bool rangeTrigger;
    [SerializeField] private bool firstEntrance;
    [SerializeField] private bool optionMode;
    private bool didDialogueStart = false;
    private int lineIndex;
    private float textSpeed = 0.05f;

    [SerializeField] private TimeManager timeManager;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject characterPlaceholder;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text sentenceField;
    [SerializeField] private GameObject nextArrow;
    [SerializeField] private ConversationPart[] currentConversation;
    [SerializeField] private ConversationPart[] houseEntrance;
    [SerializeField] private ConversationPart[] lookingAround;
    [SerializeField] private ConversationPart[] alibiCheck;
    [SerializeField] private ConversationPart[] throughWindow;
    [SerializeField] private ConversationPart[] thisKindOfPeople;
    [SerializeField] private ConversationPart[] atNight;
    [SerializeField] private ConversationPart[] unknownBoy;
    [SerializeField] private ConversationPart[] before25th;
    [SerializeField] private ConversationPart[] aboutNil;
    [SerializeField] private ConversationPart[] whyIsNilGone;
    [SerializeField] private ConversationPart[] helpWithInvestigation;
    [SerializeField] private ConversationPart[] thankYou;
    [SerializeField] private ConversationPart[] after20min;

    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    [SerializeField] private Button button5;
    [SerializeField] private Button button6;

    //Según se empieza, firstEntrance indica que todavía no se ha hablado
    void Awake() {
        firstEntrance = true;
        currentConversation = houseEntrance;
        optionMode = false;
    }

    //el jugador inicia la conversación si está en el trigger y todavía no ha hablado con Kaida
    //se tiene en cuenta si la frase está terminada de escribir o no
    void Update(){
        if (rangeTrigger && firstEntrance && !optionMode) {
            if (!didDialogueStart){
                StartDialogue();
            } else if (sentenceField.text == currentConversation[lineIndex].currentSentence) {
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
                    sentenceField.text = currentConversation[lineIndex].currentSentence;
                }
            }
        } if (optionMode)
        {
            Time.timeScale = 0f;
            CheckAndActivate(button1);
            CheckAndActivate(button2);
            CheckAndActivate(button3);
            CheckAndActivate(button4);
            CheckAndActivate(button5);
        } else
        {
            CheckAndDeactivate(button1);
            CheckAndDeactivate(button2);
            CheckAndDeactivate(button3);
            CheckAndDeactivate(button4);
            CheckAndDeactivate(button5);
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
        if (lineIndex < currentConversation.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialogueUI.SetActive(false);
            firstEntrance = false;
            Time.timeScale = 1f;
            optionMode = true;
        }
    }

    //Corrutina para mostrar poco a poco la frase, con efecto de tipeo.
    private IEnumerator ShowLine(){
        characterName.text = currentConversation[lineIndex].characterName;
        characterPlaceholder.GetComponent<Image>().sprite = currentConversation[lineIndex].faceSprite;
        sentenceField.text = string.Empty;

        foreach (char ch in currentConversation[lineIndex].currentSentence) {
            sentenceField.text += ch;
            AudioManager.instance.PlaySFX("Voice-" + currentConversation[lineIndex].characterName);
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

    public void OnClickButton(Button button)
    {
        firstEntrance = true;
        if (button.name == "Button 1")
        {
            currentConversation = alibiCheck;
            timeManager.UpdateClock(10);
            optionMode = false;
            Destroy(button);
            return;
        }
        else if (button.name == "Button 2")
        {
            currentConversation = unknownBoy;
            timeManager.UpdateClock(5);
            optionMode = false;
            Destroy(button);
            return;
        }
        else if (button.name == "Button 3")
        {
            currentConversation = before25th;
            timeManager.UpdateClock(2);
            optionMode = false;
            Destroy(button);
            return;
        }
        else if (button.name == "Button 4")
        {
            currentConversation = helpWithInvestigation;
            timeManager.UpdateClock(5);
            optionMode = false;
            Destroy(button);
            return;
        }
        else if (button.name == "Button 5")
        {
            currentConversation = thankYou;
            timeManager.UpdateClock(0);
            optionMode = false;
            Destroy(button);
            firstEntrance = false;
            Time.timeScale = 1f;
        } 
    }

    private void CheckAndActivate(Button button)
    {
        if (button)
        {
           button.gameObject.SetActive(true);
        }
    }

    private void CheckAndDeactivate(Button button)
    {
        if (button)
        {
            button.gameObject.SetActive(false);
        }
    }
}
