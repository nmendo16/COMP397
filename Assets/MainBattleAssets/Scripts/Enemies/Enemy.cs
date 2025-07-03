using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 5;
    private int health;

    // Public accessors
    public int MaxHealth => maxHealth;
    public int Health => health;

    [Header("Movement & Shooting")]
    public float moveSpeed = 5f;
    public GameObject enemyBulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 2.5f;

    [Header("Scoring")]
    // Remove scoreValue field if unused
    // public int scoreValue = 10;

    private Transform player;
    private PlayerStats playerStats;
    private float nextFireTime;

    public void Initialize(int initialMaxHealth)
    {
        maxHealth = initialMaxHealth;
        health = initialMaxHealth;
    }

    void Awake()
    {
        health = maxHealth;
    }

    void Start()
    {
        // Cache player and its PlayerStats
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
            playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (player != null)
            MoveTowardPlayer();

        if (Time.time >= nextFireTime &&
            enemyBulletPrefab != null &&
            bulletSpawnPoint != null)
        {
            AutoFire();
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        health -= damage;

        // Award 2 points per HP of damage
        if (playerStats != null)
            playerStats.IncreaseScore(damage * 2);

        if (health <= 0)
        {
            Die();
        }
    }

    void MoveTowardPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void AutoFire()
    {
        Instantiate(enemyBulletPrefab,
                    bulletSpawnPoint.position,
                    Quaternion.identity);

        SoundManager.PlaySound(SoundType.ENEMYSHOOT, 0.5f);
        nextFireTime = Time.time + fireRate;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            SoundManager.PlaySound(SoundType.BULLETHITSENEMY);
            Destroy(other.gameObject);
        }
    }

    void Die()
    {
        SoundManager.PlaySound(SoundType.ENEMYDEATH);
        Destroy(gameObject);
    }
}