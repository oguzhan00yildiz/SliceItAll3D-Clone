using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIPanelControl : MonoBehaviour
{

    [SerializeField] private GameObject StartPanel, FailPanel, SuccesPanel, RetryButton;
    private bool isFailed, isLevelDone, isStarted;
    
    [SerializeField] private TMP_Text levelTxt;
    // Start is called before the first frame update

    void Awake()
    {
        levelTxt.text = "Seviye " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
    void Start()
    {
        StartPanel.SetActive(true);
        FailPanel.SetActive(false);
        SuccesPanel.SetActive(false);
        RetryButton.SetActive(false);

        isFailed = false;
        isLevelDone = false;
    }

    public void StartPanelButton()
    {
        StartPanel.SetActive(false);
        RetryButton.SetActive(true);
    }

    public void FailPanelButton()
    {
        StartCoroutine(Timer());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SuccesPanelButton()
    {
        StartCoroutine(Timer());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        isFailed = KnifeMovement.knifeMovementInstance.isFailed;
        isLevelDone = KnifeStuck.KnifeStuckInstance.isLevelDone;

        if (isFailed)
        {
            FailPanel.SetActive(true);
        }
        else if(isLevelDone)
        {
            SuccesPanel.SetActive(true);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
