using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public float shootingInterval;
    private float lastBulletTime;

    // Đối tượng sẽ quay (ở đây bạn đặt là "HuongQuay")
    public Transform rotatingPart;

    void Update()
    {
        if (Time.time - lastBulletTime > shootingInterval)
        {
            ShootBullet();
            lastBulletTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy == null)
        {
            return;
        }

        Vector3 direction = enemy.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Xoay phần thân pháo
        if (rotatingPart != null)
        {
            rotatingPart.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        // Tạo viên đạn theo góc
        Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(0f, 0f, angle));
    }
}
