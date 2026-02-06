using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Kéo Player vào đây
    public Vector3 offset = new Vector3(0, 5, -10);  // Khoảng cách camera
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target.position + Vector3.up * 1.5f); // Nhìn vào đầu player
    }
}