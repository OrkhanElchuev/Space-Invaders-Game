using UnityEngine;
using System;

public class PlayerInfo : MonoBehaviour
{
    private int score;

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
    }
}
