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

    private void Start()
    {
        playerInfoScript = PlayerInfo.GetComponent<PlayerInfo>();
        playerInfoScript.LoadPlayer();
        SetInitialScoreBoardValues();
        SettingTextValues();
    }

    // Fill the values of Text component with values from score board arr
    private void SettingTextValues()
    {
        int[] scoreBoardArr = playerInfoScript.GetScoreBoard();
        for (int i = 0; i < scoreBoardArr.Length; i++)
        {
            leaderBoardTextArr[i].text = scoreBoardArr[i].ToString();
        }
    }

    // Create list of predefined values for scoreboard
    private void SetInitialScoreBoardValues()
    {
        int[] array = { 252500, 175400, 95200, 54300, 34200, 7800 };
        playerInfoScript.SetScoreBoard(array);
        playerInfoScript.SavePlayer();
    }
}

