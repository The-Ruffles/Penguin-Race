using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public bool isPaused;
    public bool isCountdownTimerActive = true;
    public float levelTime = 30f;
    public float countdownTimer = 5f;
    public TextMeshProUGUI levelTimer_TMP, countdowntimer_TMP;
    public string timeUnit;
    Scene currentSceneName;
    int currentSceneIndex;

    public GameObject preGamePanel, ranOutOfTimePanel, completedLevelPanel;
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentSceneName = SceneManager.GetActiveScene();
        currentSceneIndex = currentSceneName.buildIndex;
        GameTimer();
    }
    
    void Update() 
    {
        if (!isPaused && !isCountdownTimerActive)
        {
            levelTime -= 1 * Time.deltaTime;
            GameTimer();
        }
        if (isCountdownTimerActive)
        {
            countdownTimer -= 1 * Time.deltaTime;
            CoutdownTimer();
            if (countdownTimer < 1)
            {
                countdownTimer = 0;
                preGamePanel.SetActive(false);
                isCountdownTimerActive = false;
            }
        }
        if (levelTime < 0.1)
        {
            //reloadScene
            levelTime = 0;
            LevelFinished(ranOutOfTimePanel);
        }
    }
    void GameTimer()
    {
        string textTime = levelTime.ToString("0.0");
        levelTimer_TMP.text = textTime + timeUnit;
    }
    void CoutdownTimer()
    {
        string countdownTime = countdownTimer.ToString("0");
        countdowntimer_TMP.text = countdownTime;
    }

    public void LevelFinished(GameObject panelType)
    {
        Cursor.lockState = CursorLockMode.Confined;
        panelType.SetActive(true);
    }

    public void TryLevelAgain()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
