using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMesh Pro types

public class PlayerStats : MonoBehaviour
{
    // Player stats
    public int lifePoints = 5;  // Starting life points
    public int score = 0;

    [Header("UI for topdown camera")]
    // UI Elements for text (assign in the Inspector)
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI locationText;

    // For life display using a 3D coin prefab
    public GameObject coinPrefab;  // Your 3D coin prefab with Mesh Renderer components
    public Transform coinContainer; // A container (e.g., a GameObject in a World Space Canvas) where coins will be instantiated

    [Header("UI for pilot camera")]
    // same UI elements as for topdown camera but this time, for pilot camera (assigned in the inspector)
    public TextMeshProUGUI pilotScoreText;
    public TextMeshProUGUI pilotLocationText;
    // same life display using a 3D coin prefab
    public GameObject pilotCoinPrefab;  // Your 3D coin prefab for pilot camera
    public Transform pilotCoinContainer; // A container for pilot camera coins


    // Internal list to keep track of instantiated coin prefabs
    private List<GameObject> coinInstances = new List<GameObject>();
    private List<GameObject> pilotCoinInstances = new List<GameObject>();

    void Start()
    {
        SetupLives();
        UpdateScoreUI();
        UpdateLocationUI();
    }

    void Update()
    {
        // Update the player's location display (if needed)
        UpdateLocationUI();
    }

    // Instantiate coin prefabs corresponding to the player's life points
    void SetupLives()
    {
        // Clear any existing coins, if applicable.
        foreach (GameObject coin in coinInstances)
        {
            Destroy(coin);
        }
        coinInstances.Clear();

        // Arrange coins horizontally: starting at X = -165, increasing by 50 for each coin.
        for (int i = 0; i < lifePoints; i++)
        {
            GameObject coin = Instantiate(coinPrefab, coinContainer);
            // Set fixed Y = 0, and adjust X by 50 units per coin.
            coin.transform.localPosition = new Vector3(-165f + i * 150f, 0f, 0f);

            // Set the coin's scale to (80, 80, 80)
            coin.transform.localScale = new Vector3(280f, 280f, 280f);
            coinInstances.Add(coin);

            GameObject pilotCoin = Instantiate(pilotCoinPrefab, pilotCoinContainer);
            // Set fixed Y = 0, and adjust X by 50 units per coin.
            pilotCoin.transform.localPosition = new Vector3(-165f + i * 150f, 0f, 0f);

            // Set the coin's scale to (80, 80, 80)
            pilotCoin.transform.localScale = new Vector3(280f, 280f, 280f);
            pilotCoinInstances.Add(pilotCoin);
        }
    }

    // Increase the player's score and update the score UI
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // Decrease life points (for example, when hit by an enemy bullet), update the lives UI, and check for game over.
    public void DecreaseLife(int amount)
    {
        lifePoints -= amount;
        SoundManager.PlaySound(SoundType.BULLETHITSPLAYER);
        if (lifePoints < 0) lifePoints = 0;

        UpdateLivesUI();

        if (lifePoints == 0)
        {
            // instead of Debug.Log:
            if (EventManager.Instance != null)
            {
                SoundManager.PlaySound(SoundType.PLAYERDEATH);
                EventManager.Instance.ShowPopup("Game Over");
            }
            else
                Debug.LogWarning("EventManager not found!");
        }
    }


    // Update the score text on the UI
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
            pilotScoreText.text = score.ToString();
        }
    }

    // Optionally update a UI display for the player's location
    public void UpdateLocationUI()
    {
        if (locationText != null)
        {
            Vector3 pos = transform.position;
            locationText.text = "Pos: " + pos.x.ToString("F1") + ", " + pos.y.ToString("F1") + ", " + pos.z.ToString("F1");
            pilotLocationText.text = "Pos: " + pos.x.ToString("F1") + ", " + pos.y.ToString("F1") + ", " + pos.z.ToString("F1");
        }
    }

    // Update the 3D coin display based on current lifePoints.
    // Coins with indices higher than current lifePoints will be deactivated.
    public void UpdateLivesUI()
    {
        for (int i = 0; i < coinInstances.Count; i++)
        {
            coinInstances[i].SetActive(i < lifePoints);
        }

        for (int i = 0; i < pilotCoinInstances.Count; i++)
        {
            pilotCoinInstances[i].SetActive(i < lifePoints);
        }
    }

    // Detect collision with enemy bullet and decrease life
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyBullet"))
        {
            DecreaseLife(1);
            Destroy(other.gameObject);
        }
    }

}