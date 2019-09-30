[System.Serializable]
public class PlayerData
{
    private int score;
    private int[] scoreBoardPlayerDataArr = new int[6];

    public int GetScorePlayerData()
    {
        return score;
    }

    // Get scoreboard array
    public int[] GetScoreBoardPlayerData()
    {
        return scoreBoardPlayerDataArr;
    }

    // Assign score board array and score to relevant values
    public PlayerData(PlayerInfo playerInfo)
    {
        score = playerInfo.GetScore();
        scoreBoardPlayerDataArr = playerInfo.GetScoreBoard();
    }
}

