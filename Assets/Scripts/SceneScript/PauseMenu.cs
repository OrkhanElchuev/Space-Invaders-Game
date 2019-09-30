using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] SceneLoader sceneLoader;

    // When escape key pressed, pause or resume the game
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        // Make pause panel inactive
        pauseMenuUI.SetActive(false);
        // Resume the game flow
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    // Load start menu and resume the game flow
    public void LoadStartMenu()
    {
        Time.timeScale = 1.0f;
        sceneLoader.LoadStartMenu();
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        // Freeze the game flow
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
