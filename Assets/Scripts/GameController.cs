using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static Action OnLevelLoaded;

    [SerializeField] private GameObject gameOverPanel, winPanel;
    private bool gameOver = false, levelWon = false;
    private int currentLevel = 1, currentCheckpoint = 1;
    [SerializeField] private Transform startingPoint;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        TimeController.OnTimeOut += GameOver;
        PlayerController.OnPlayerFall += GameOver;
        PlayerController.OnPlayerSpawn += UnpauseGamePlay;
    }

    void UnpauseGamePlay()
    {
        Time.timeScale = 1.0f;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnLevelLoaded?.Invoke();
        if (SceneManager.GetActiveScene().name.Equals("Level03"))
        {
            Time.timeScale = 0.0f;
            levelWon = true;
            currentLevel = 1;
        }
        //Debug.Log(player.transform.position.y);
    }

    void Update()
    {
        if (levelWon)
        {
            if (Input.GetKeyUp(KeyCode.JoystickButton0) || ((Input.acceleration.z < -0.5f) && !SceneManager.GetActiveScene().name.Equals("Level03")) || ((Input.acceleration.z > 0.5f) && SceneManager.GetActiveScene().name.Equals("Level03")))
                LoadNextLevel();
        }
        else if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R) || (Input.touchCount > 0) || (Input.acceleration.z < -0.5f) || Input.GetKeyUp(KeyCode.JoystickButton0))
                Restart();
        }
    }

    private void Restart()
    {
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        currentLevel = 1;
        gameOver = false;
        SceneManager.LoadSceneAsync("Level01");
    }

    public void Win()
    {
        levelWon = true;
        winPanel.SetActive(true);
        Debug.Log("Level Complete!");
        Time.timeScale = 0.0f;
        currentLevel++;
        if (currentLevel > 3)
            currentLevel = 1;
    }

    public void LoadNextLevel()
    {
        levelWon = false;
        winPanel.SetActive(false);
        SceneManager.LoadSceneAsync("Level0" + currentLevel.ToString());
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOver = true;
        Time.timeScale = 0.0f;
        Debug.Log("Game Over!");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        TimeController.OnTimeOut -= GameOver;
        PlayerController.OnPlayerFall -= GameOver;
        PlayerController.OnPlayerSpawn -= UnpauseGamePlay;
    }
}
