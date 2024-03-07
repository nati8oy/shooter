using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilForce = 10f; // The force of the recoil
    public float recoilDuration = 0.1f; // The duration of the recoil

    private bool isRecoiling = false; // Flag to track if the player is currently recoiling
    private Vector3 recoilDirection; // The direction of the recoil

    // Update is called once per frame
    void Update()
    {
        // Check for input to trigger recoil (e.g., shooting)
        if (Input.GetKeyDown(KeyCode.Space) && !isRecoiling)
        {
            StartRecoil();
        }
    }

   public void StartRecoil()
    {
        // Calculate recoil direction (backward from the player's current facing direction)
        recoilDirection = -transform.forward;

        // Apply recoil force
        GetComponent<Rigidbody>().AddForce(recoilDirection * recoilForce, ForceMode.Impulse);

        // Set flag to indicate that the player is recoiling
        isRecoiling = true;

        // Invoke method to end recoil after specified duration
        Invoke("EndRecoil", recoilDuration);
    }

    public void EndRecoil()
    {
        // Reset flag
        isRecoiling = false;
    }
}
