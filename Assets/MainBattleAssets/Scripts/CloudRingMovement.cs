using UnityEngine;

public class CloudRingMovement : MonoBehaviour
{
    public float amplitude = 0.5f;  // Maximum movement range
    public float speed = 5f;      // Movement speed
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Create smooth floating motion
        float offsetX = Mathf.Sin(Time.time * speed) * amplitude;
        float offsetY = Mathf.Cos(Time.time * speed * 0.7f) * amplitude * 0.5f; // Slight variation
        float offsetZ = Mathf.Sin(Time.time * speed * 0.5f) * amplitude * 0.3f;

        // Apply movement to the cloud ring as a whole
        transform.position = initialPosition + new Vector3(offsetX, offsetY, offsetZ);
    }
}