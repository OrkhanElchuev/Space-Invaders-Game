using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
  [SerializeField] GameObject playerInfo;
  private GameObject gameOverMenuCanvas;
  private TextMeshProUGUI scoreText;
  void Start()
  {
    gameOverMenuCanvas = transform.parent.Find("GameOverMenuCanvas").gameObject;
    scoreText = gameOverMenuCanvas.transform.Find("ScoreFieldText").gameObject.GetComponent<TextMeshProUGUI>();
    playerInfo.GetComponent<PlayerInfo>().LoadPlayer();
    scoreText.text = playerInfo.GetComponent<PlayerInfo>().GetScore().ToString();
  }
}
