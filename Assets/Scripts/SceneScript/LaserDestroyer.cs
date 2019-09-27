using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestroyer : MonoBehaviour
{
    // Destroy redundant objects(Lasers) in case of going out of screen
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
