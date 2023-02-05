using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO         question;
    [SerializeField] TextMeshProUGUI    questionText;
    [Header("Answers")]
    [SerializeField] GameObject[]       answerButtons;     
                     int                correctAnswerIndex;
                     bool               hasAnsweredEarly;
    [Header("Button Colors")]
    [SerializeField] Sprite             defaultAnswerSprite;
    [SerializeField] Sprite             correctAnswerSprite;  
    [SerializeField] Sprite             incorrectAnswerSprite; 
    [Header("Timer")]
    [SerializeField] Image              timerImage;
    Timer timer;   
 
    void Start()
    {
        //DisplayQuestion();
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();        
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly        = false;
            timer.loadNextQuestion  = false;
            GetNextQuestion();
            
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }

    }

    void GetNextQuestion()
    {
        SetButtonState(true); 
        setDefaultButtonSprties();
        DisplayQuestion();
        
    }

    void DisplayQuestion()
    {
        questionText.text = question.getQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.getAnswer(i);
            
        }
    }

    public void OnAnswerSelected(int index) {
        {
            hasAnsweredEarly = true;
            DisplayAnswer(index);
            SetButtonState(false);
            timer.cancelTimer(); 
        }
    }
    //Highlight correct answer and display correct answer
    void DisplayAnswer(int index)
    {
            string  reason              = "Incorrect";
            int     correctAnswerIndex  = question.getCorrectAnswerIndex();            
            //Image buttonImage;
            if (index<0)
            {
                reason = "TimeOut";
            }
            
            if (index == correctAnswerIndex)
            {
                questionText.text = "Correct";
                SetButtonSprite(index,correctAnswerSprite);
            }
            else 
            {                
                //Highlight incorrect answer
                if(index > 0)
                {
                    SetButtonSprite(index,incorrectAnswerSprite);
                }                
                //Highlight correct answer and display correct answer
                questionText.text = reason + ". The correct answer is: \n\"" + question.getAnswer(correctAnswerIndex) + "\"";
                SetButtonSprite(correctAnswerIndex,correctAnswerSprite);
                
            }
        
    }

    void setDefaultButtonSprties()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponentInChildren<Image>();
            buttonImage.sprite = defaultAnswerSprite;              
        }   
    }

    void SetButtonSprite(int index, Sprite sprite)
    {
        Image buttonImage;
        buttonImage = answerButtons[index].GetComponent<Image>();
        buttonImage.sprite = sprite;
    }

    void SetButtonState(bool state)
    {
  
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponentInChildren<Button>();

            button.interactable = state;   
        }         

    }

}
