using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50f;  // Adjust as needed
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.1f;

    private float nextFireTime;
    public Rigidbody rb;

    public float minX = -100f, maxX = 100f;
    public float minZ = -60f, maxZ = 500f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }
    void FixedUpdate()
    {
        MovePlayer();
        AutoFire();
    }

    void MovePlayer()
    {
        // read input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // compute raw movement delta
        Vector3 delta = new Vector3(h, 0f, v)
                      * speed
                      * Time.fixedDeltaTime;

        // compute the next position
        Vector3 nextPos = rb.position + delta;

        // clamp X and Z into your allowed range
        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
        nextPos.z = Mathf.Clamp(nextPos.z, minZ, maxZ);

        // move there
        rb.MovePosition(nextPos);
    }



    void AutoFire()
    {
        if (bulletPrefab != null && Time.time >= nextFireTime)
        {
            // Spawn the bullet 5 units in the world forward (Z) direction from the bulletSpawnPoint
            Vector3 spawnPosition = bulletSpawnPoint.position + Vector3.forward * 5f;
            Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            SoundManager.PlaySound(SoundType.PLAYERSHOOT,0.5f);
            nextFireTime = Time.time + fireRate;
        }
    }
}