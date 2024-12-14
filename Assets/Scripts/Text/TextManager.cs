using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//SCRIPT SOLO PARA LA INTRODUCCIÓN
//en el futuro se tratará de dejar todo el texto escrito en pantalla
public class TextManager : MonoBehaviour
{
    [SerializeField] private FloatSO timeCount;
    [SerializeField] private Vector3SO nemoPosition;
    private Queue<string> introduction;
    public Dialogue text;
    public TextMeshProUGUI report;
    public float textSpeed;
    public bool typingOver;
    public bool reportOver;
    public string storedText;
    public Animator transition; 
    public float transitionTime;

    //al inicio se realiza una Queue de textos y se inicia el diálogo especificando la velocidad de tipeo y otros factores con respecto al efecto
    void Start()
    {
        introduction = new Queue<string>();
        StartDialogue(text);
        textSpeed = .1f;
        typingOver = true;
        reportOver = false;
        transitionTime = 1f;

        //inicialización
        timeCount.Value = 3;
        nemoPosition.Value = Vector3.zero;
    }

    //se van mostrando una a una las frases de la Queue
    public void StartDialogue (Dialogue dialogue){
        foreach (string sentence in dialogue.sentences){
            introduction.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    //se pasa a la siguiente frase si la hay, pasándola por una corrutina de tipeo, y con la opción de 
    //en caso de que no queden frases, se termina el diálogo
    public void DisplayNextSentence(){
        AudioManager.instance.PlaySFX("UI-Click");
        if (introduction.Count == 0 && typingOver == true){
            EndDialogue();
            return;
        } else{
            if (typingOver == true) {
                string sentence = introduction.Dequeue();
                storedText = sentence;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            } else {
                StopAllCoroutines();
                report.text = storedText;
                typingOver = true;
            }
        } 
    }

    //Corrutina de efecto de tipeo con sonido
    IEnumerator TypeSentence (string sentence) {
        report.text = "";
        foreach (char letter in sentence.ToCharArray()){
            typingOver = false;
            report.text += letter;
            AudioManager.instance.PlaySFX("Voice-TypingMachine");
            yield return new WaitForSeconds(textSpeed);
        }
        typingOver = true;
    }

    //terminar el diálogo lleva a la escena 2 (el mapa)
    void EndDialogue() {
        StopAllCoroutines();
        report.text = "";
        StartCoroutine(LoadScene(2));
    }

    //Corrutina para cargar la escena con transición
    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
