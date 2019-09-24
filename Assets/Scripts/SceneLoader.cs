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

    // Quit the application 
    public void Quit()
    {
        Application.Quit();
    }
}
