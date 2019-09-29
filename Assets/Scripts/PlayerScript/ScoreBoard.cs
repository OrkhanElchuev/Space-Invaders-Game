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
        SettingTextValues();
    }

    // Fill the values of Text component with values from score board array
    private void SettingTextValues(){
        int[] scoreBoardArr = playerInfoScript.GetScoreBoard();
        for (int i = 0; i < scoreBoardArr.Length; i++)
        {
            leaderBoardTextArr[i].text = scoreBoardArr[i].ToString();
        }
    }
}
