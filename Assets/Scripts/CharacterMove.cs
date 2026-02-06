using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    CharacterController controller;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;

    [Header("Jump & Gravity")]
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (groundCheck == null)
        {
            GameObject gc = new GameObject("GroundCheck");
            gc.transform.parent = transform;
            gc.transform.localPosition = new Vector3(0, -1f, 0);
            groundCheck = gc.transform;
        }
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Conveyor3D conveyor = hit.collider.GetComponent<Conveyor3D>();

        if (conveyor != null)
        {
            Vector3 conveyorMove =
                conveyor.direction.normalized
                * conveyor.speed
                * Time.deltaTime;

            controller.Move(conveyorMove);
        }
    }

}
