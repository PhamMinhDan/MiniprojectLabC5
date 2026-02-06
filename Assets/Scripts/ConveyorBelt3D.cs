using UnityEngine;

public class Conveyor3D : MonoBehaviour
{
    [Header("Conveyor Settings")]
    public float speed = 6f;
    public Vector3 direction = Vector3.left;

    [Header("Visual (Optional)")]
    public Material conveyorMaterial;
    public float scrollSpeed = 0.5f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (rend != null && conveyorMaterial != null)
        {
            float offset = Time.time * scrollSpeed;
            rend.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {

            Vector3 targetVelocity = direction.normalized * speed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

            Debug.Log($"Conveyor moving: {collision.gameObject.name}");
        }

        CharacterController cc = collision.gameObject.GetComponent<CharacterController>();

        if (cc != null)
        {
            Vector3 move = direction.normalized * speed * Time.deltaTime;
            cc.Move(new Vector3(move.x, 0, move.z));
        }
    }
}