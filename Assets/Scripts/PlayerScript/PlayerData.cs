[System.Serializable]
public class PlayerData
{
    private int score;

    public int GetScorePlayerData()
    {
        return score;
    }

    public PlayerData(PlayerInfo playerInfo)
    {
        score = playerInfo.GetScore();
    }
}

