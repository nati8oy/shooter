using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
   [SerializeField] private UnityEvent onHit;
    public int maxHealth; // Maximum health of the player
    public int currentHealth; // Current health of the player
   [SerializeField] private PlayerData playerData;

    void Start()
    {
        maxHealth = playerData.health;
        currentHealth = maxHealth; // Set initial health
    }

    

    // Implement the TakeDamage method from the IDamageable interface
    public void TakeDamage(int amount)
    {
        onHit.Invoke();
        // Reduce player's health
        currentHealth -= amount;

        // Check if player's health has reached zero
        if (currentHealth <= 0)
        {
            Die(); // Player dies if health reaches zero
        }
    }

    // Function to handle player's death
    void Die()
    {
        

        Debug.Log("Player died!");
        // Implement game over or respawn logic here
        // For example, you can reload the scene or show a game over screen
    }

}
