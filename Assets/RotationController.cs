using UnityEngine;
using DG.Tweening;

public class RotationController : MonoBehaviour
{
    public float rotationAmount = 90f; // Amount of rotation in degrees
    public float rotationDuration = 0.5f; // Duration of rotation animation

    private bool rotating = false;

    void Update()
    {
        if (!rotating)
        {
            // Check for input to rotate
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotatePlayer(rotationAmount); // Rotate left
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotatePlayer(-rotationAmount); // Rotate right
            }
        }
    }

    void RotatePlayer(float amount)
    {
        // Set rotating flag to true to prevent multiple rotations simultaneously
        rotating = true;

        // Calculate target rotation
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, amount);

        // Use DOTween to animate the rotation
        transform.DORotateQuaternion(targetRotation, rotationDuration).SetEase(Ease.OutQuart)
            .OnComplete(() => rotating = false); // Set rotating flag to false when rotation is complete
    }
}
