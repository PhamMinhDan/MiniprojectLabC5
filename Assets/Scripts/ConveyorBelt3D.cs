using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction = Vector3.right;  // sang phải

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            rb.AddForce(direction * speed, ForceMode.VelocityChange);
        }
    }
}