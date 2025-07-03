using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float speed = 10f; // Slower speed than player bullets
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.forward * speed;
        Destroy(gameObject, 5f); // Safety‐kill in case it never hits anything
    }

    void OnTriggerEnter(Collider other)
    {
        // 1) Ignore collisions with the Enemy that spawned it or with any other Bullet
        if (other.CompareTag("Enemy") || other.CompareTag("Bullet"))
            return;

        // 2) If it hits the Player, reduce life by 1
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
                stats.DecreaseLife(1);
        }

        // 3) Destroy this bullet on every other collision (including hitting the Player)
        Destroy(gameObject);
    }
}