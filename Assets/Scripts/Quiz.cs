using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
                     QuestionSO         currentQuestion;
    [SerializeField] TextMeshProUGUI    questionText;
    [SerializeField] List<QuestionSO>   questions = new List<QuestionSO>();
    
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
    
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
 
    void Awake()
    {
        //DisplayQuestion();
        timer                   = FindObjectOfType<Timer>(); 
        scoreKeeper             = FindObjectOfType<ScoreKeeper>();  
        scoreText.text          = "Score: 0%";  
        progressBar.maxValue    = questions.Count;
        progressBar.value       = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
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
        if(questions.Count>0)
        {
            SetButtonState(true); 
            setDefaultButtonSprties();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsCompleted();
        }
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0,questions.Count);
        //Debug.Log("Question Index: " + index);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.getQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
            
        }
    }

    public void OnAnswerSelected(int index) {
        {
            
            hasAnsweredEarly = true;
            DisplayAnswer(index);
            SetButtonState(false);
            timer.cancelTimer(); 
            scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";


        }
    }
    //Highlight correct answer and display correct answer
    void DisplayAnswer(int index)
    {
        string  reason              = "Incorrect";
        int     correctAnswerIndex  = currentQuestion.getCorrectAnswerIndex();            
        //Image buttonImage;
        if (index<0)
        {
            reason = "TimeOut";
        }
        
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct";
            SetButtonSprite(index,correctAnswerSprite);
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {            
            //Highlight incorrect answer
            if(index > -1)
            {
                SetButtonSprite(index,incorrectAnswerSprite);
            }                
            //Highlight correct answer and display correct answer
            questionText.text = reason + ". The correct answer is: \n\"" + currentQuestion.getAnswer(correctAnswerIndex) + "\"";
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
