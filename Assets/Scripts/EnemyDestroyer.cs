using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public CityLifeController cityLifeController;
    private void OnTriggerEnter2D(Collider2D other) {
        // Destroy the enemies reaching the city so they don't add up in the scene
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);

            // Count the hit amount to the city life controller
            cityLifeController.hitAmount ++;
        }
    }
}
