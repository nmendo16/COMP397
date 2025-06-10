using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float speed = 10f; // Slower speed than player bullets
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Reverse movement direction to go "backward"
        rb.velocity = -transform.forward * speed;

        Destroy(gameObject, 5f); // Destroy bullet after 5 seconds
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) // Prevent bullets from destroying the enemy itself
        {
            Destroy(gameObject);
        }
    }
}