using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
    public int life = 100;
    public int enemyValue = 200;
    public CityEconomyManager economyManager;
    void Start()
    {
        
    }

    void Update()
    {
        if (life <= 0)
        {
            // Get parent 
            var parent = transform.parent;
            var enemySpawner = parent.gameObject.GetComponent<EnemySpawner>();
            enemySpawner.availableEnemies.Remove(gameObject);
            Destroy(gameObject);
            economyManager.IncreaseMoneyAmount(enemyValue);
        }
    }
}
