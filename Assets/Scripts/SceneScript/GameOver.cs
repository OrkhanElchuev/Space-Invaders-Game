using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    private GameObject gameOverMenuCanvas;
    private TextMeshProUGUI scoreText;
    [SerializeField] SceneLoader loadScene;

    
    void Start()
    {
        gameOverMenuCanvas = transform.parent.Find("GameOverMenuCanvas").gameObject;
        scoreText = gameOverMenuCanvas.transform.Find("ScoreFieldText").gameObject.GetComponent<TextMeshProUGUI>();
        playerInfo.LoadPlayer();
        scoreText.text = playerInfo.GetScore().ToString();
    }

    // Update score board 
    private void UpdateScoreBoard(PlayerInfo playerInfo)
    {
        // Initialize arrays and set current score value to local variable
        int scoreValue = playerInfo.GetScore();
        int[] array = playerInfo.GetScoreBoard();
        int[] newArray = new int[7];
        int[] fixedFinalArray = new int[6];

        // Fill up newArray with values from scoreBoard
        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        // Assign current score to the last index of array
        newArray[newArray.Length - 1] = scoreValue;
        // Sort array in descending order
        Array.Sort(newArray);
        Array.Reverse(newArray);

        // Fill up final array with first 10 values of sorted newArray  
        for (int i = 0; i < fixedFinalArray.Length; i++)
        {
            fixedFinalArray[i] = newArray[i];
        }

        // Reset the values in scoreboard
        playerInfo.SetScoreBoard(fixedFinalArray);
        playerInfo.SetScore(0);
        playerInfo.SavePlayer();
    }

    // When home button pressed, update scoreboard
    public void LoadHomeScene()
    {
        playerInfo.LoadPlayer();
        UpdateScoreBoard(playerInfo);
        loadScene.LoadStartMenu();
    }
}
