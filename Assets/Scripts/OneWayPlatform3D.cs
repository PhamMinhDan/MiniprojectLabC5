using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider platformCollider;

    void Start()
    {
        platformCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null && playerRb.linearVelocity.y > 0) // Nhảy từ dưới lên
            {
                platformCollider.enabled = false;
                Invoke("EnableCollider", 0.3f);
            }
        }
    }

    void EnableCollider()
    {
        platformCollider.enabled = true;
    }
}