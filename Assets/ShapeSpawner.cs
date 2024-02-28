using UnityEngine;

public class ShapeSpawner : PoolerBase<Projectile>
{
    [SerializeField] private Projectile projectilePrefab;

    private void Start()
    {
        InitPool(projectilePrefab); // Initialize the pool

        var projectile = Get(); // Pull from the pool
        Release(projectile); // Release back to the pool
    }

    // Optionally override the setup components
    protected override void GetSetup(Projectile projectile)
    {
        base.GetSetup(projectile);
        projectile.transform.position = Vector3.zero;

    }
}