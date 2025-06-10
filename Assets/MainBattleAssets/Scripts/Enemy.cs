using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 4; // Enemy starts with 4 health
    public float moveSpeed = 5f; // Slower movement speed
    public GameObject enemyBulletPrefab; // Bullet prefab
    public Transform bulletSpawnPoint; // Bullet spawn location
    public float fireRate = .5f; // Slower firing rate than the player

    private Transform player;
    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Locate the player
    }

    void Update()
    {
        MoveTowardPlayer();
        AutoFire();
    }

    void MoveTowardPlayer()
    {
        if (player != null)
        {
            // Move towards the player at a slower speed
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
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
        // Check if the collision is with a bullet
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1); // Each bullet hit reduces health by 1
            Destroy(other.gameObject); // Remove the bullet upon impact
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Destroyed!");
        Destroy(gameObject); // Remove enemy when health reaches 0
    }
}