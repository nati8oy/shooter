using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyDataSO enemyData; // Reference to the ScriptableObject containing enemy data

    void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            // Deal damage to the enemy
            TakeDamage(projectile.damage);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyData.health -= damage;
        if (enemyData.health <= 0)
        {
            // Enemy defeated
            Destroy(gameObject);
        }
    }
}
