using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    [SerializeField] Sprite attackSpeedSprite;
    [SerializeField] Sprite healthSprite;
    [SerializeField] Sprite weaponSprite;
    [SerializeField] Sprite scoreSprite;

    private Sprite powerUpSprite;
    private string powerUpType;
    private int powerUpSpeed = 6;

    // Setting power Up type and changing sprite accordingly
    public void SetPowerUpType(string type)
    {
        powerUpType = type;
        SetPowerUpSprite(powerUpType);
    }

    private void SetPowerUpSprite(string spriteType){
        // Set relevant sprite according to the type
        switch (spriteType)
        {
            case "HealthPowerUp":
                powerUpSprite = healthSprite;
                break;
            case "AttackSpeedPowerUp":
                powerUpSprite = attackSpeedSprite;
                break;
            case "ScorePowerUp":
                powerUpSprite = scoreSprite;
                break;
            case "WeaponPowerUp":
                powerUpSprite = weaponSprite;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = powerUpSprite;
    }

    public string GetPowerUpType()
    {
        return powerUpType;
    }

    // Moving PowerUp
    private void MovePowerUp()
    {
        // Moving downward 
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUpSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        MovePowerUp();
    }
}
