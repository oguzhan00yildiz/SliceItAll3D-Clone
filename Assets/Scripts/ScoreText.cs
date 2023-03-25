using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{

    public TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score : " + SliceTestScript.SliceTestScriptInstant.score.ToString();
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
        int multipliedScore;

        multipliedScore = SliceTestScript.SliceTestScriptInstant.score * multipleValue; 
        
    }
}
