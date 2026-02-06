using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bắn")]
    public GameObject projectilePrefab;     // Kéo Prefab quả cầu vào đây
    public Transform firePoint;             // Điểm xuất phát (mũi nhân vật hoặc tay)
    public float projectileSpeed = 20f;     // Tốc độ bay
    public float fireRate = 0.5f;           // Thời gian giữa các phát bắn (giây)

    private float nextFireTime = 0f;

    void Start()
    {
        // Nếu chưa có firePoint, tạo một empty child ở đầu nhân vật
        if (firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            fp.transform.parent = transform;
            fp.transform.localPosition = new Vector3(0, 1.5f, 1f); // phía trước, cao ngang đầu
            firePoint = fp.transform;
        }
    }

    void Update()
    {
        // Bắn khi nhấn chuột trái (hoặc nút Fire1)
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Tạo quả cầu tại firePoint, hướng theo camera hoặc player
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Lấy Rigidbody của quả cầu và đẩy nó bay
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Bay theo hướng camera (third person)
            Vector3 shootDirection = Camera.main.transform.forward;
            rb.linearVelocity = shootDirection * projectileSpeed;
        }

        // Optional: tự hủy sau 5 giây để tránh rác
        Destroy(projectile, 5f);
    }
}