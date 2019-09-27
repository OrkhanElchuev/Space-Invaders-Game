[System.Serializable]
public class PlayerData
{
  public int score;

  public PlayerData(PlayerInfo playerInfo)
  {
    score = playerInfo.GetScore();
  }
}

