using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Jump Pad Settings")]
    public float jumpForce = 10f; // Z�plama kuvveti

    private void OnTriggerEnter(Collider other)
    {
        // E�er �arpan nesne oyuncuysa
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Oyuncuya z�plama kuvveti uygula
                playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, jumpForce, playerRb.linearVelocity.z);
            }
        }
    }
}
