using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ProjectileShooter : MonoBehaviour
{
    public UnityEvent onFire;
    public Projectile projectilePrefab; // Prefab of the projectile to shoot
    public float projectileSpeed = 10f; // Speed of the projectile
    public float fireInterval = 2f; // Interval between each shot
    public float projectileLifetime = 3f; // Lifetime of the projectile
    public float shootingAngleRange = 3f; // Range of random shooting angle offset in degrees
    public List<Transform> firePoints = new List<Transform>();
    private ObjectPool<Projectile> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Projectile>(CreateProjectile, null, null, defaultCapacity: 150);
    }

    private Projectile CreateProjectile()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.gameObject.SetActive(false); // Ensure the projectile is initially inactive
        return projectile;
    }

    void Start()
    {
        // Start shooting projectiles on a timer
        StartCoroutine(ShootProjectilesOnInterval());
    }

    IEnumerator ShootProjectilesOnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);
            onFire.Invoke();
            // Calculate random shooting angle offset within the specified range
            float randomAngleOffset = Random.Range(-shootingAngleRange, shootingAngleRange);
            // Apply random rotation offset to the firing direction
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngleOffset);
            // Shoot projectile with random rotation offset
            foreach (Transform t in firePoints)
            {
                Projectile newProjectile = _pool.Get();

                newProjectile.SetObjectPool(_pool); // This line sets the object pool reference for the projectile

                // Set other properties of the projectile
                newProjectile.transform.position = t.position;
                newProjectile.transform.rotation = t.rotation * randomRotation;
                newProjectile.gameObject.SetActive(true);
                // Set velocity for the projectile
                Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = newProjectile.transform.up * projectileSpeed;
                }
                // Destroy the projectile after its lifetime
                StartCoroutine(DestroyProjectileAfterLifetime(newProjectile));
            }
        }
    }

    IEnumerator DestroyProjectileAfterLifetime(Projectile projectile)
    {
        yield return new WaitForSeconds(projectileLifetime);
        _pool.Release(projectile); // Release the projectile back to the object pool
        //Debug.Log("Item returned to pool");
    }
}
