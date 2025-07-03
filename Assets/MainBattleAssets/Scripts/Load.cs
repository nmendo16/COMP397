using System.IO;
using UnityEngine;

public class Load : MonoBehaviour
{
    public GameObject player;  // Assign the Player GameObject in the Inspector
    private PlayerStats playerStats; // Reference to PlayerStats component

    void Start()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    public GameObject optionsPanel; // Assign this in the Inspector

    public void load()
    {
        string location = Path.Combine(Application.persistentDataPath, "saveOne.json");

        if (File.Exists(location))
        {
            string json = File.ReadAllText(location);
            SaveGame s1 = JsonUtility.FromJson<SaveGame>(json);
            Debug.Log("Load Successful! Location=" + s1.location + ", health=" + s1.health + ", score=" + s1.score);

            // Hide options panel
            if (optionsPanel != null)
                optionsPanel.SetActive(false);
            
            // Resume normal game speed
            Time.timeScale = 1f;

            // Apply loaded data to the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerStats stats = player.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.lifePoints = s1.health;
                    stats.score = s1.score;
                    stats.UpdateScoreUI();
                    stats.UpdateLivesUI();
                }

                player.transform.position = s1.location; // Move player to saved location
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }
}