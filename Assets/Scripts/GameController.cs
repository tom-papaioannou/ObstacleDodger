using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Action OnLevelLoaded;

    [SerializeField] private GameObject gameOverPanel, winPanel, player;
    private CinemachineVirtualCamera virtualCamera;
    private bool gameOver = false, levelWon = false;
    private int currentLevel = 1;
    private Vector3? persistentSpawnPosition = null;
    private Quaternion? persistentSpawnRotation = null;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
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
        EndingPoint.OnPlayerWon += Win;
        CheckPoint.OnPlayerCheckpoint += ChangeSpawnPoint;
    }

    void SpawnPlayer()
    {
        if (persistentSpawnPosition == null)
        {
            Debug.Log("There is not spawnpoint.");
            ChangeSpawnPoint(GameObject.FindGameObjectWithTag("Start").transform);
        }
        
        
        virtualCamera.Follow = Instantiate(player, persistentSpawnPosition ?? new Vector3(), persistentSpawnRotation ?? new Quaternion()).transform;
    }

    void ChangeSpawnPoint(Transform transform)
    {
        persistentSpawnPosition = (transform != null) ? transform.position : null;
        persistentSpawnRotation = (transform != null) ? transform.rotation : null;
    }

    void UnpauseGamePlay()
    {
        Time.timeScale = 1.0f;
    }

    void PauseGamePlay()
    {
        Time.timeScale = 0.0f;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnLevelLoaded?.Invoke();
        if (SceneManager.GetActiveScene().name.Equals("Level03"))
        {
            PauseGamePlay();
            levelWon = true;
            currentLevel = 1;
        }
        else
        {
            UnpauseGamePlay();
            virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
            SpawnPlayer();
        }
    }

    void Update()
    {
        if (levelWon && (Input.GetKeyUp(KeyCode.JoystickButton0) || ((Input.acceleration.z < -0.5f) && !SceneManager.GetActiveScene().name.Equals("Level03")) || ((Input.acceleration.z > 0.5f) && SceneManager.GetActiveScene().name.Equals("Level03"))))
            LoadNextLevel();
        else if (gameOver && (Input.GetKeyDown(KeyCode.R) || (Input.touchCount > 0) || (Input.acceleration.z < -0.5f) || Input.GetKeyUp(KeyCode.JoystickButton0)))
            Restart();
    }

    private void Restart()
    {
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameOver = false;
        SceneManager.LoadSceneAsync("Level0" + currentLevel.ToString());
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
        ChangeSpawnPoint(null);
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
        EndingPoint.OnPlayerWon -= Win;
        CheckPoint.OnPlayerCheckpoint -= ChangeSpawnPoint;
    }
}
