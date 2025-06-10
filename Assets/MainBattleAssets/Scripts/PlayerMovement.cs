using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50f;  // Adjust as needed
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;

    private float nextFireTime;

    void FixedUpdate()
    {
        MovePlayer();
        AutoFire();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, vertical, 0);
        transform.position += moveDirection * speed * Time.fixedDeltaTime;
    }

    void AutoFire()
    {
        if (bulletPrefab != null && Time.time >= nextFireTime)
        {
            Vector3 spawnPosition = bulletSpawnPoint.position + transform.up * 5f; // Offset to prevent instant collision
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            nextFireTime = Time.time + fireRate;
        }
    }
}