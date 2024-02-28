using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed;
    private float rotationSpeed;
    private float accuracy;

    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        movementSpeed = playerData.movementSpeed;
        rotationSpeed = playerData.rotationSpeed;
        accuracy = playerData.accuracy;
    }

    void Update()
    {
        // Move forward constantly
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);

        // Rotate left when A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            RotatePlayer(rotationSpeed * Time.deltaTime);
        }

        // Rotate right when D key is pressed
        if (Input.GetKey(KeyCode.D))
        {
            RotatePlayer(-rotationSpeed * Time.deltaTime);
        }
    }

    void RotatePlayer(float rotationAmount)
    {
        // Rotate the player
        transform.Rotate(Vector3.forward * rotationAmount);
    }

}
