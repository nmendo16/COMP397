using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;
    public GameObject prefab;
    public int spawnCount = 5;
    public float spacingX = 20f;
    public List<float> spawnZPositions = new List<float> { 80f, 160f, 240f, 350f };

    [Header("Offset & Patterns")]
    public float zOffset = 40f;     // Always spawn this far ahead of the player’s Z
    public enum SpawnPattern
    {
        Line,
        ZigZag,
        DiagonalIncrease,
        DiagonalDecrease
    }

    private int nextSpawnIndex = 0;
    private System.Random rnd = new System.Random();

    void Update()
    {
        if (nextSpawnIndex >= spawnZPositions.Count || prefab == null || player == null)
            return;

        float triggerZ = spawnZPositions[nextSpawnIndex];
        if (player.position.z >= triggerZ)
        {
            // Compute actual world Z to spawn at
            float spawnZ = player.position.z + zOffset;
            // Pick a random pattern
            SpawnPattern pattern = (SpawnPattern)rnd.Next(Enum.GetNames(typeof(SpawnPattern)).Length);
            // Execute the chosen layout
            SpawnAtPattern(pattern, spawnZ);
            nextSpawnIndex++;
        }
    }

    void SpawnAtPattern(SpawnPattern pattern, float z)
    {
        switch (pattern)
        {
            case SpawnPattern.Line:
                SpawnLine(z);
                break;
            case SpawnPattern.ZigZag:
                SpawnZigZag(z);
                break;
            case SpawnPattern.DiagonalIncrease:
                SpawnDiagonal(z, increasing: true);
                break;
            case SpawnPattern.DiagonalDecrease:
                SpawnDiagonal(z, increasing: false);
                break;
        }
    }

    void SpawnLine(float z)
    {
        float totalWidth = spacingX * (spawnCount - 1);
        float startX = player.position.x - totalWidth * 0.5f;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = new Vector3(startX + i * spacingX,
                                      player.position.y,
                                      z);
            CreateEnemy(pos);
        }
    }

    void SpawnZigZag(float z)
    {
        float baseX = player.position.x;
        for (int i = 0; i < spawnCount; i++)
        {
            float x = baseX + (i % 2 == 0 ? -spacingX * i : spacingX * i);
            Vector3 pos = new Vector3(x, player.position.y, z + (i * 2f));
            CreateEnemy(pos);
        }
    }

    void SpawnDiagonal(float z, bool increasing)
    {
        float totalWidth = spacingX * (spawnCount - 1);
        float startX = player.position.x - totalWidth * 0.5f;
        for (int i = 0; i < spawnCount; i++)
        {
            float x = startX + i * spacingX;
            float zz = increasing
                ? z + i * (spacingX * 0.5f)
                : z - i * (spacingX * 0.5f);
            CreateEnemy(new Vector3(x, player.position.y, zz));
        }
    }

    void CreateEnemy(Vector3 position)
    {
        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        Enemy e = go.GetComponent<Enemy>();
        e.Initialize(UnityEngine.Random.Range(1, 8));
    }
}