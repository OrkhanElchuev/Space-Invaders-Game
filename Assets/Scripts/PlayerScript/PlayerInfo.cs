using UnityEngine;
using System;

public class PlayerInfo : MonoBehaviour
{
    private int score;
    private int[] scoreBoardArray = new int[10];

    public void SetScoreBoard(int[] newScoreBoardArray)
    {
        scoreBoardArray = newScoreBoardArray;
    }

    public int[] GetScoreBoard()
    {
        return scoreBoardArray;
    }

    public void SetScore(int amount)
    {
        score = amount;
    }
    public int GetScore()
    {
        return score;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        score = data.GetScorePlayerData();
        scoreBoardArray = data.GetScoreBoardPlayerData();
    }
}
