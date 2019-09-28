using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] int damage = 100;
    private float laserSpeed;
    private string laserType;

    public int GetDamage()
    {
        return damage;
    }

    public void CreateItself(float speed, string type)
    {
        laserSpeed = speed;
        laserType = type;
    }

    // Destroy Laser Object
    public void Hit()
    {
        Destroy(gameObject);
    }

    private void Update()
    {

        switch (laserType)
        {
            case "straight":
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                break;
            case "left":
                GetComponent<Rigidbody2D>().velocity = new Vector2(-2, laserSpeed);
                break;
            case "right":
                GetComponent<Rigidbody2D>().velocity = new Vector2(2, laserSpeed);
                break;
        }
    }
}
