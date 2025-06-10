using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    public int health = 4; // Enemy starts with 4 health
    public float moveSpeed = 5f; // Movement speed toward the player
    public float rotationSpeed = 100f; // Speed for continuous rotation
    public GameObject enemyBulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 2.5f;

    private Transform player;
    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    void Update()
    {
        MoveTowardPlayer();
        RotateEnemy(); // New function to rotate the cube
        AutoFire();
    }

    void MoveTowardPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void RotateEnemy()
    {
        // Rotate the cube around its own axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void AutoFire()
    {
        if (enemyBulletPrefab != null && Time.time >= nextFireTime)
        {
            Instantiate(enemyBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { Die(); }
    }

    void Die()
    {
        Debug.Log("Enemy Cube Destroyed!");
        Destroy(gameObject);
    }
}