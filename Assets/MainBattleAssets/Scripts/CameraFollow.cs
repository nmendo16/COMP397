using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public float followSpeed = 5f; // Smoothness of movement
    private Vector3 offset; // Stores initial camera distance

    void Start()
    {
        // Calculate the exact offset using the player's current position
        offset = new Vector3(transform.position.x - player.position.x, 297.6f - player.position.y, -70f - player.position.z);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player's X position while maintaining Y and Z stability
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}