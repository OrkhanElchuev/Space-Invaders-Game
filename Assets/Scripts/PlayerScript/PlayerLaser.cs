using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    // Destroy Laser Object
    public void Hit()
    {
        Destroy(gameObject);
    }
}
