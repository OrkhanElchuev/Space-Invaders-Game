using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int numberOfHealthSprites;
    [SerializeField] Image[] healthSprites;

    void Update()
    {
        for (int i = 0; i < healthSprites.Length; i++)
        {
            if(i < numberOfHealthSprites)
            {
                healthSprites[i].enabled = true;
            }
            else
            {
                healthSprites[i].enabled = false;
            }
        }
    }
}
