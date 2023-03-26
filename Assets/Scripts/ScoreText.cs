using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private int multipliedScore;

    void Update()
    {
        if (multipliedScore>0)
        {
            score.text = multipliedScore.ToString();
            currentScore.text = multipliedScore.ToString();
        }
        else
        {
            score.text = SliceScript.SliceTestScriptInstant.score.ToString();
            currentScore.text = SliceScript.SliceTestScriptInstant.score.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int number;
        if (other.CompareTag("2x"))
        {
            number = 2;
            ScoreMultiplier(number);
            
        }
        else if (other.CompareTag("3x"))
        {
            number = 3;
            ScoreMultiplier(number);
        }
        else if (other.CompareTag("4x"))
        {
            number =4;
            ScoreMultiplier(number);
        }
        else if (other.CompareTag("5x"))
        {
            number =5;
            ScoreMultiplier(number);
        }
        else if (other.CompareTag("10x"))
        {
            number = 10;
            ScoreMultiplier(number);
        }
    }

    private void ScoreMultiplier(int multipleValue) 
    {
        multipliedScore = SliceScript.SliceTestScriptInstant.score * multipleValue; 
        score.text = multipliedScore.ToString();
        currentScore.text = multipliedScore.ToString();
    }
}
