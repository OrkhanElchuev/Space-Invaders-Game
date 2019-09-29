[System.Serializable]
public class PlayerData
{
    private int score;
    private int[] scoreBoardPlayerDataArr = new int[10];

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
        scoreBoardPlayerDataArr = playerInfo.GetScoreBoard();
        score = playerInfo.GetScore();
    }
}

