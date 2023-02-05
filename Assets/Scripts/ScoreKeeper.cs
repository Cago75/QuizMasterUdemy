using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int questionsCompleted = 0;
    int correctAnswers     = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int getQuestionCompleted()
    {
        return questionsCompleted;
    }

    public void IncrementQuestionsCompleted()
    {
        questionsCompleted++;
    }
    public int CalculateScore()
    {
        return Mathf.RoundToInt((float)correctAnswers/questionsCompleted*100);
    }
}
