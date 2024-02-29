using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float distance = 10f; // Distance between pointA and pointB on the X-axis

    private Vector3 pointA;
    private Vector3 pointB;
    private bool movingTowardsB = true;

    [SerializeField] EnemyDataSO enemyData;
    [SerializeField] private bool rotator;
    [SerializeField] private bool canMove;


    void Start()
    {

        rotator = enemyData.rotating;
        canMove = enemyData.canMove;


        if (rotator)
        {
            gameObject.AddComponent<Rotator>();
        }

        // Set pointA to the current position of the GameObject
        pointA = transform.position;

        // Calculate pointB based on the distance on the X-axis
        pointB = pointA + new Vector3(distance, 0f, 0f);
    }

    void Update()
    {


        if (canMove)
        {
            // Determine the target position based on the direction
            Vector3 targetPosition = movingTowardsB ? pointB : pointA;

            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if reached the target position
            if (transform.position == targetPosition)
            {
                // Toggle the direction
                movingTowardsB = !movingTowardsB;
            }
        }

     
    }
}
