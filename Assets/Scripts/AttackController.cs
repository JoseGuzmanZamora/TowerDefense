using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject objective;
    public BulletController bulletPrefab;
    public bool isShooting = false;
    public float shootingInterval = 0.5f;
    private float shootingCounter = 0;

    void Update()
    {
        if (isShooting)
        {
            shootingCounter += Time.deltaTime;
            if (shootingCounter >= shootingInterval)
            {
                // Instantiate bullet
                if (objective != null)
                {
                    var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    newBullet.objective = objective;
                }
                shootingCounter = 0;
            }
        }
    }
}
