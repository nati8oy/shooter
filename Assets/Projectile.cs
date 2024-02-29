using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    public GameObject muzzleFlash;
    public UnityEvent onHit;
    public int damage = 1; // Damage value of the projectile
    public LayerMask targetLayer; // Layer mask for detecting target objects (player or enemy)

    private ObjectPool<Projectile> _pool;

    // Set the object pool for the projectile
    public void SetObjectPool(ObjectPool<Projectile> pool)
    {
        _pool = pool;
    }

    private void Start()
    {
        GameObject hitEffect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
        // Destroy the hit effect after a short delay
        Destroy(hitEffect, 0.05f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with an object on the target layer
        if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            // Get the damageable component of the collided object
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            // Check if the collided object has a damageable component
            if (damageable != null)
            {
                GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                // Destroy the hit effect after a short delay
                Destroy(hitEffect, 0.05f);

                // Deal damage to the collided object
                damageable.TakeDamage(damage);
            }

            // Destroy the projectile after collision
            
        }

        //return it to the pool even if it hit anything else
        ReturnToPool();
    }

    // Return the projectile to the object pool
    private void ReturnToPool()
    {
        if (_pool != null)
        {
            gameObject.SetActive(false);
            _pool.Release(this);
        }
        else
        {
            Debug.LogWarning("Object pool reference is null. Projectile cannot be returned to the pool.");
            Destroy(gameObject); // If pool reference is null, destroy the projectile
        }
    }
}
