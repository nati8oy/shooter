using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    public GameObject collectiblePrefab; // Prefab of the collectible dropped by the enemy
    public int dropChancePercent = 50; // Percentage chance of dropping the collectible

    public EnemyDataSO EnemyDataSO;
    private float healthTotal;
    private float healthRemaining;
    [SerializeField] private UnityEvent onHit;


    private void Start()
    {
        healthTotal = EnemyDataSO.health;
        healthRemaining = healthTotal;
    }

    // Implement the TakeDamage method from the IDamageable interface
    public void TakeDamage(int amount)
    {
        onHit.Invoke();

        if (healthRemaining > 0)
        {
            healthRemaining -= amount;
            Debug.Log("Enemy takes " + amount + " damage!");
            if (healthRemaining <= 0)
            {
                healthRemaining = 0;
                Die();
            }
        }

    }
    private void Die()
    {
        Destroy(gameObject); // Destroy the enemy GameObject

        // Check if the collectible should be dropped based on drop chance
        if (Random.Range(0, 100) < dropChancePercent)
        {
            // Instantiate the collectible prefab at the enemy's position
            Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
        }
    }
}
