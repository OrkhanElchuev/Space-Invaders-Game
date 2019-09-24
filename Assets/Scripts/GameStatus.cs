using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private int playerScore = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        // If there are more than one GameStatus objects destroy itself
        int numGameSessions = FindObjectsOfType<GameStatus>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Get score value 
    public int GetScore()
    {
        return playerScore;
    }

    // Add to score value
    public void AddToScore(int scoreValue)
    {
        playerScore += scoreValue;
    }

    // Destroy GameStatus object
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
