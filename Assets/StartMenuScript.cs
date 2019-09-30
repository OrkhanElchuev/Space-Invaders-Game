using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.LoadPlayer();
        Debug.Log(playerInfo.GetScoreBoard());
    }
}
