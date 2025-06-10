using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed for restart functionality

public class EventManager : MonoBehaviour
{
    public GameObject popUpWindow; // Assign a UI panel in Unity
    public Button quitButton; // Assign Quit Button
    public Button restartButton; // Assign Restart Button
    public float checkInterval = 1f;

    void Start()
    {
        popUpWindow.SetActive(false);
        InvokeRepeating("CheckForEnemies", checkInterval, checkInterval);

        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void CheckForEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            ShowEndTestMessage();
        }
    }

    void ShowEndTestMessage()
    {
        popUpWindow.SetActive(true);
        Time.timeScale = 0; // Freezes everything
    }

    void QuitGame()
    {
        Debug.Log("Closing application... Goodbye!");
        Application.Quit();
    }

    void RestartGame()
    {
        Time.timeScale = 1; // Unfreeze everything
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
    }
}