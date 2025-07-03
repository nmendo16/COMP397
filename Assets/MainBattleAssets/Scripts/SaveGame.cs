using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveGame
{
    public Vector3 location;
    public int health;
    public int score;

    public SaveGame(Vector3 location, int health, int score)
    {
        this.location = location;
        this.health = health;
        this.score = score;
    }

    //Creates a snapshot of this class and writes it to the disc
    public void save()
    {
        string location = Path.Combine(Application.persistentDataPath, "saveOne.json");
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(location, json);
        Debug.Log("Game Saved To: " + location);
    }
}
