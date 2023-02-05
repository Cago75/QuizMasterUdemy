using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    float timerValue;
    [SerializeField] float  timeToCompleteQuestion   = 30f;
    [SerializeField] float  timeToShowAnswer         = 8f;
    [Header("Sprites")]
    [SerializeField] GameObject timerImageGO;  
    [SerializeField] Sprite answeringSprite;
    [SerializeField] Sprite pauseSprite;

    public bool  loadNextQuestion    = true;
    public bool  isAnsweringQuestion = false;
    public float fillFraction;

    void Update()
    {
        UpdateTimer();            
    }

    public void cancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        Image timerImage;
        timerValue -= Time.deltaTime;
        timerImage = timerImageGO.GetComponent<Image>();
        if(timerValue <= 0 )
        {
            if (isAnsweringQuestion == true)
            {
                isAnsweringQuestion = false;
                timerValue          = timeToShowAnswer;
                timerImage.sprite   = pauseSprite;
            }
            else 
            {
                loadNextQuestion    = true;
                isAnsweringQuestion = true;
                timerValue          = timeToCompleteQuestion;
                timerImage.sprite   = answeringSprite;
            }

            
        }
        else //continue
        {
            float divider;
            if(isAnsweringQuestion == true)
            {
                divider = timeToCompleteQuestion;
            }
            else
            {
                divider = timeToShowAnswer;
            }
            fillFraction = timerValue/divider;
        }

        Debug.Log(isAnsweringQuestion + ":" +timerValue + ":" + fillFraction);
    }



}
