using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    public GameObject muzzleFlash;
    public UnityEvent onHit;
    public int damage = 1; // Damage value of the projectile
    public LayerMask targetLayer; // Layer mask for detecting target objects (player or enemy)


    private void Start()
    {
        GameObject hitEffect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
        // Destroy the hit effect after a short delay
        Destroy(hitEffect, 0.05f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {


        // Check if the collision is with an object on the enemy layer
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

        }

        // Destroy the projectile after collision
        //onHit.Invoke();
        Destroy(gameObject);
    }
}
