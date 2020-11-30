using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    
    public bool isPaused;
    public float levelTime = 30f;
    public TextMeshProUGUI levelTimer_TMP;
    public string timeUnit;
    public string currentSceneName;
    void Update() 
    {
        if (!isPaused)
        {
            levelTime -= 1 * Time.deltaTime;
            string textTime = levelTime.ToString("0.0");
            levelTimer_TMP.text = textTime + timeUnit;
        }
        if (levelTime < 0.1)
        {
            //reloadScene
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
