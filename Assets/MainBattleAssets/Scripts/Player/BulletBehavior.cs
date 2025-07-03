using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 30f;
    public Rigidbody rb;
    public float rotationSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; // Move forward automatically
        Destroy(gameObject, 5f); // Bullet lasts 5 seconds before disappearing
    }
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        // Ensure the bullet does NOT destroy itself when hitting the player
        if (!other.CompareTag("Player"))
        {
            
            Destroy(gameObject); // Destroy bullet only if it hits something other than the player
        }
    }
}