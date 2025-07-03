
using UnityEngine;

public class Save : MonoBehaviour
{
    public void save()
    {
        Debug.Log("Saving in progress!");

        // Find the Player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            if (stats != null)
            {
                // Save real player values (current position, health, score)
                SaveGame s1 = new SaveGame(player.transform.position, stats.lifePoints, stats.score);
                s1.save();
            }
        }
    }
}
