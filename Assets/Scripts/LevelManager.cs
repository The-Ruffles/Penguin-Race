using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //variable list
    public bool isCountdownTimerActive = true;
    public bool levelFinished = false;
    public float levelTime = 30f;
    public float countdownTimer = 5f;
    public TextMeshProUGUI levelTimer_TMP, countdowntimer_TMP;
    public string timeUnit;
    int currentSceneIndex;

    public GameObject preGamePanel, ranOutOfTimePanel, completedLevelPanel;
    
    /*
        start method

        lock cursor
        give the int variable the current scene index number
        call game timer
    */
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameTimer();
    }
    
    /*
        Update method

        if countdown is not active, coutdown from level time
        if countdwon is active, countdown from the coutdown time and then set the bool to false so the game timer starts
        if you run out of time call the method with run out panel as the parameter
    */
    void Update() 
    {
        if (!isCountdownTimerActive)
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
            levelFinished = true;
            LevelFinished(ranOutOfTimePanel);
        }
    }
    /*
        set the UI to the current time 
    */
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

    /*
        level finished method

        activate cursor
        active chosen panel
    */
    public void LevelFinished(GameObject chosenPanel)
    {
        Cursor.lockState = CursorLockMode.Confined;
        chosenPanel.SetActive(true);
    }

    /*
        try again method
        
        called on try again button on ran out of time panel
    */
    public void TryLevelAgain()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    /*
        next level method
        
        called on next level button on completed level panel
        loads the current scene + 1 e.g if current is 1, loads 2
    */
    public void NextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    /*
        main menu method
        
        called on main menu buttons
        loads main menu scene which is index 0 in build settings
    */
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
