using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
  public static void SavePlayer(PlayerInfo playerInfo)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/palyer.pd";
    FileStream stream = new FileStream(path, FileMode.Create);

    PlayerData data = new PlayerData(playerInfo);

    formatter.Serialize(stream, data);
    stream.Close();
  }

  public static PlayerData LoadPlayer()
  {
    string path = Application.persistentDataPath + "/palyer.pd";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);

      PlayerData data = formatter.Deserialize(stream) as PlayerData;
      stream.Close();
      return data;
    }
    Debug.LogError("Save file not found in " + path);
    return null;
  }
}