using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int movementSpeed = 1;
    public int damageAmount = 10;
    public GameObject objective;
    void Update()
    {
        if (objective != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, objective.transform.position, (Time.deltaTime * movementSpeed));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            var enemyLife = other.gameObject.GetComponent<EnemyLifeController>();
            enemyLife.life -= damageAmount;
            Destroy(gameObject);
        }
    }
}
