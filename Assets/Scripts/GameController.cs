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
    private bool gameOver = false, restart = false, levelWon = false;
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
//#if !UNITY_EDITOR
//        if (Input.GetKeyUp(KeyCode.RightArrow) && !SceneManager.GetActiveScene().name.Equals("Level03"))
//        {
//            Win();
//        }
//#endif
        if (levelWon)
        {
            if (Input.GetKeyUp(KeyCode.JoystickButton0) || ((Input.acceleration.z < -0.5f) && !SceneManager.GetActiveScene().name.Equals("Level03")) || ((Input.acceleration.z > 0.5f) && SceneManager.GetActiveScene().name.Equals("Level03")))
            {
                LoadNextLevel();
            }
        }
        if (restart)
        {
            Time.timeScale = 1.0f;
            restart = false;
        }
        else if (player != null)
        {
            if (!gameOver)
            {
                CheckIfGameOver();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R) || (Input.touchCount > 0)  || (Input.acceleration.z < -0.5f) || Input.GetKeyUp(KeyCode.JoystickButton0))
                {
                    Restart();
                }
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Restart()
    {
            player = null;
            winPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            currentLevel = 1;
            gameOver = false;
            SceneManager.LoadSceneAsync("Level01");
            restart = true;
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
        levelWon = true;
        winPanel.SetActive(true);
        Debug.Log("Level Complete!");
        Time.timeScale = 0.0f;
        //SceneManager.LoadSceneAsync(levelToLoad);
        currentLevel++;
        if (currentLevel > 3)
            currentLevel = 1;
    }

    public void LoadNextLevel()
    {
        levelWon = false;
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
