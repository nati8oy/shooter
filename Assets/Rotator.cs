using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the object around its local up axis (Y-axis)
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
