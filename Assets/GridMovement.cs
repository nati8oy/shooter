using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float gridScale = 1f; // Size of each grid square
    public float movementSpeed = 1f; // Speed of movement

    private Vector3 targetPosition; // Target position for movement

    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Check if the player has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            // Round the current position to the nearest grid position
            Vector3Int roundedPosition = new Vector3Int(
                Mathf.RoundToInt(transform.position.x / gridScale),
                Mathf.RoundToInt(transform.position.y / gridScale),
                Mathf.RoundToInt(transform.position.z / gridScale)
            );

            // Move to the next grid position along the Z-axis
            targetPosition = transform.position + Vector3.forward * gridScale;
        }
    }
}
