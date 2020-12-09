using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int currentSceneIndex;

    void start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    } 
    public void NextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void KyleLevel()
    {
        SceneManager.LoadScene("KyleScene");
    }

    public void SkylarLevel()
    {
        SceneManager.LoadScene("SkylarScene");
    }

    public void AinohaLevel()
    {
        SceneManager.LoadScene("AinohaScene");
    }

    public void RyanLevel()
    {
        SceneManager.LoadScene("RyanScene");
    }

    public void SamLevel()
    {
        SceneManager.LoadScene("SamScene");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
