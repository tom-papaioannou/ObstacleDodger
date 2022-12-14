using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverPanel, winPanel;
    private float gameOverThreshold = -50.0f;
    private bool gameOver = false;
    private int currentLevel = 1, currentCheckpoint = 1;
    [SerializeField] private Transform startingPoint;

    public static GameController Instance { get; private set; }

    public UIController UIController { get; private set; }
    public TimeController TimeController { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        UIController = GetComponentInChildren<UIController>();
        TimeController = GetComponentInChildren<TimeController>();
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TimeController.ResetTime();
    }

    void Update()
    {
        if (player != null)
        {
            if (!gameOver)
            {
                CheckIfGameOver();
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void UpdateTimeUI(string time)
    {
        UIController.UpdateTimeText(time);
    }

    void CheckIfGameOver()
    {
        if (player.transform.position.y < gameOverThreshold)
        {
            GameOver();
        }
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Debug.Log("Level Complete!");
        Time.timeScale = 0.0f;
        //SceneManager.LoadSceneAsync(levelToLoad);
        currentLevel++;
        if (currentLevel > 2)
            currentLevel = 1;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1.0f;
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
    }

}
