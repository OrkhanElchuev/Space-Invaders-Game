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
    private int powerUpSpeed = 5;

    public void SetPowerUpType(string type)
    {
        powerUpType = type;
        switch (type)
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

    private void MovePowerUp()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUpSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePowerUp();
    }
}
