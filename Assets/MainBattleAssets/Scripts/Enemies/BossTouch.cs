using UnityEngine;

public class BossTouchDamage : MonoBehaviour
{
    [Tooltip("How many life points to remove on touch")]
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Only damage the Player
        if (!other.CompareTag("Player")) return;

        // Try to grab the PlayerStats component
        var stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.DecreaseLife(damage);
        }
    }
}