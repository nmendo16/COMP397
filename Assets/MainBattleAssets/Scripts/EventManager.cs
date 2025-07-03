using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("UI References")]
    public GameObject popUpWindow;
    public Text messageText;
    public Text pauseText;
    public Button quitButton;
    public Button restartButton;

    bool isPausedByPlayer = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        popUpWindow.SetActive(false);
        pauseText.enabled = false;
        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popUpWindow.activeSelf && isPausedByPlayer)
                ResumeGame();
            else
                PauseForPlayer();
        }
    }

    public void BossDefeated()
    {
        ShowPopup("Level Clear");
    }

    public void ShowPopup(string msg)
    {
        messageText.text = msg;
        popUpWindow.SetActive(true);
        Time.timeScale = 0f;
        isPausedByPlayer = false;
    }

    void PauseForPlayer()
    {
        messageText.text = "Paused";
        popUpWindow.SetActive(true);
        Time.timeScale = 0f;
        isPausedByPlayer = true;
        pauseText.enabled = true;
    }

    void ResumeGame()
    {
        popUpWindow.SetActive(false);
        Time.timeScale = 1f;
        isPausedByPlayer = false;
        pauseText.enabled = false;
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}