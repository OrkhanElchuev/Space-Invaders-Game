using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] GameObject PlayerInfo;
    [SerializeField] TextMeshProUGUI[] leaderBoardTextArr;
    private PlayerInfo playerInfoScript;
    private void Awake()
    {
        playerInfoScript = PlayerInfo.GetComponent<PlayerInfo>();
        playerInfoScript.LoadPlayer();
        
        //SetInitialScoreBoardValues();
        UpdateScoreBoard();
        SettingTextValues();
    }

    // Fill the values of Text component with values from score board arr
    private void SettingTextValues(){
        int[] scoreBoardArr = playerInfoScript.GetScoreBoard();
        for (int i = 0; i < scoreBoardArr.Length; i++)
        {
            leaderBoardTextArr[i].text = scoreBoardArr[i].ToString();
        }
    }

    // Update score board 
    private void UpdateScoreBoard()
    {   
        //
        int value = playerInfoScript.GetScore();
        int[] array = playerInfoScript.GetScoreBoard();
        int[] newArray = new int[7];
        int[] fixedFinalArray = new int[6];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        // Assign current score to the last index of array
        newArray[newArray.Length - 1] = value;
        // Sort array in descending order
        Array.Sort(newArray);
        Array.Reverse(newArray);
        
        // Fill up final array with first 10 values of sorted newArray  
        for (int i = 0; i < fixedFinalArray.Length; i++)
        {
            fixedFinalArray[i] = newArray[i];
        }

        // Reset the values in scoreboard
        playerInfoScript.SetScoreBoard(fixedFinalArray);
        playerInfoScript.SetScore(0);
        playerInfoScript.SavePlayer();
    }
    
    // Create list of predefined values for scoreboard
       private void SetInitialScoreBoardValues()
    {
        int[] array = { 225300, 152900, 52300, 35000, 15100, 5900};
        playerInfoScript.SetScoreBoard(array);
        playerInfoScript.SavePlayer();
    }
}

