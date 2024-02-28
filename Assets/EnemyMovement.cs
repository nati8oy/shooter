using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Enum to represent different movement patterns
    public enum MovementPattern
    {
        MoveBetweenTwoPoints,
        StayStill,
        MoveInRandomDirections
    }

    public MovementPattern movementPattern; // Selected movement pattern for the enemy

    // Variables specific to each movement pattern
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    public float idleDuration = 2f;
    public float randomMoveRadius = 5f;
    public int damageToPlayer = 1;

    private Vector3 randomDestination;
    private float idleTimer;

    void Start()
    {
        pointA = GameObject.Find("PointA").transform;
        pointB = GameObject.Find("PointB").transform;

        if (movementPattern == MovementPattern.MoveBetweenTwoPoints)
        {
            // Start moving towards point B initially
            transform.position = pointA.position;
            MoveTo(pointB.position);
        }
        else if (movementPattern == MovementPattern.MoveInRandomDirections)
        {
            SetRandomDestination();
        }
    }

    void Update()
    {
        if (movementPattern == MovementPattern.MoveBetweenTwoPoints)
        {
            // Move between two points
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                MoveTo(pointA.position);
            }
            else if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                MoveTo(pointB.position);
            }
        }
        else if (movementPattern == MovementPattern.StayStill)
        {
            // Stay still
            // You can add behavior specific to this pattern here
        }
        else if (movementPattern == MovementPattern.MoveInRandomDirections)
        {
            // Move in random directions
            if (Vector3.Distance(transform.position, randomDestination) < 0.1f)
            {
                SetRandomDestination();
            }
            else
            {
                MoveTo(randomDestination);
            }
        }

        // Update idle timer if enemy is staying still
        if (movementPattern == MovementPattern.StayStill)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                idleTimer = 0f;
                SetRandomMovementPattern();
            }
        }
    }

    void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void SetRandomDestination()
    {
        randomDestination = transform.position + Random.insideUnitSphere * randomMoveRadius;
        randomDestination.z = transform.position.z; // Ensure enemy stays on the same z-axis
    }

    void SetRandomMovementPattern()
    {
        movementPattern = (MovementPattern)Random.Range(0, System.Enum.GetValues(typeof(MovementPattern)).Length);
    }



    void OnTriggerEnter (Collider2D collider)
    {
        Debug.Log("collided");
        // Check if the collided object is the player
        if (collider.gameObject.CompareTag("Player"))
        {
            // Get the player's health component
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // If the player's health component is found, reduce its health
                playerHealth.TakeDamage(damageToPlayer);
            }
        }
    }
}
