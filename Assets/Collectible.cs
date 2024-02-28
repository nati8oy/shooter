using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Function to call when the collectible is collected
    public delegate void CollectibleAction();
    public event CollectibleAction OnCollected;

    // Called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Run the collectible's collection function
            Collect();
        }
    }

    // Function to collect the collectible
    void Collect()
    {
        // Run the OnCollected event (if subscribed)
        OnCollected?.Invoke();

        // Destroy the collectible object
        Destroy(gameObject);
    }
}
