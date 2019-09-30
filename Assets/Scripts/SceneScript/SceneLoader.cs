using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadScoreBoardMenu()
    {
        SceneManager.LoadScene("ScoreBoard");
    }

    public void LoadPowerUpSceneMenu()
    {
        SceneManager.LoadScene("PowerUpInformation");
    }

    // Quit the application 
    public void Quit()
    {
        Application.Quit();
    }
}
