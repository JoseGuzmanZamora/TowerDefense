using System.Collections;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
    public int life = 100;
    public int enemyValue = 200;
    public CityEconomyManager economyManager;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    public void GotHit()
    {
        var originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(255, 0, 0, 255);
        StartCoroutine(ChangeColor(originalColor));
        //spriteRenderer.color = originalColor;
    }

    IEnumerator ChangeColor(Color originalColor)
    {
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.white;
    }
}
