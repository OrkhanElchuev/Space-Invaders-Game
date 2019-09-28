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

    // Set speed and type values
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

    // Set Laser Type
    private void SetLaserType()
    {
        switch (laserType)
        {
            // Straight Laser
            case "straight":
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                break;
            // Tilted to the left 
            case "left":
                GetComponent<Rigidbody2D>().velocity = new Vector2(-2, laserSpeed);
                break;
            // Tilted to the right 
            case "right":
                GetComponent<Rigidbody2D>().velocity = new Vector2(2, laserSpeed);
                break;
            case "leftCorner":
                GetComponent<Rigidbody2D>().velocity = new Vector2(-4, laserSpeed);
                break;
            case "rightCorner":
                GetComponent<Rigidbody2D>().velocity = new Vector2(4, laserSpeed);
                break;

        }
    }

    // Update is called once per frame
    private void Update()
    {
        SetLaserType();
    }
}
