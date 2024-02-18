using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Action OnLevelLoaded, OnGameOver, OnLevelWin;

    [SerializeField] private GameObject player;
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private int currentLevel = 1;
    private Vector3? persistentSpawnPosition = null;
    private Quaternion? persistentSpawnRotation = null;
    private GameState gameState = GameState.Idle;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        // Transform GameController into a Singleton
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
        // The various controllers communicate with Events
        SceneManager.sceneLoaded += OnSceneLoaded;
        TimeController.OnTimeOut += GameOver;
        PlayerController.OnPlayerFall += GameOver;
        EndingPoint.OnPlayerWon += Win;
        CheckPoint.OnPlayerCheckpoint += ChangeSpawnPoint;
        UIController.OnLoadNextLevelClicked += LoadNextLevel;
    }

    void SpawnPlayer()
    {
        if (persistentSpawnPosition == null)        
            ChangeSpawnPoint(GameObject.FindGameObjectWithTag("Start").transform);
        
        virtualCamera.Follow = Instantiate(player, persistentSpawnPosition ?? new Vector3(), persistentSpawnRotation ?? new Quaternion()).transform;
    }

    void ChangeSpawnPoint(Transform transform)
    {
        // Could not store directly a Transform of a spawn point because it gets destroyed each time a level is loaded.
        // So I am storing positions and rotations and turn them to null each time a new Level is loaded.
        // If they are null, the spawn point will get position and rotation from the starting point of the Level.
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
        // Show a win screen on Level 03
        if (SceneManager.GetActiveScene().name.Equals("Level03"))
        {
            PauseGamePlay();
            gameState = GameState.Won;
            currentLevel = 1;
        }
        // Reset gameplay for next level.
        else
        {
            gameState = GameState.Idle;
            UnpauseGamePlay();
            virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
            SpawnPlayer();
        }
    }

    void Update()
    {
        // Checks only for Win and Lose conditions, more states could be added
        switch (gameState)
        {
            case GameState.Won:
                if(Input.GetKeyUp(KeyCode.JoystickButton0) || ((Input.acceleration.z < -0.5f) && !SceneManager.GetActiveScene().name.Equals("Level03")) || ((Input.acceleration.z > 0.5f) && SceneManager.GetActiveScene().name.Equals("Level03")))
                    LoadNextLevel();
                break;
            case GameState.Lost:
                if (Input.GetKeyDown(KeyCode.R) || (Input.touchCount > 0) || (Input.acceleration.z < -0.5f) || Input.GetKeyUp(KeyCode.JoystickButton0))
                    Restart();
                break;
            default:
                break;
        }
    }

    private void Restart()
    {
        gameState = GameState.Idle;
        SceneManager.LoadSceneAsync("Level0" + currentLevel.ToString());
    }

    public void Win()
    {
        gameState = GameState.Won;
        OnLevelWin?.Invoke();
        Debug.Log("Level Complete!");
        PauseGamePlay();
        currentLevel++;
        if (currentLevel > 3)
            currentLevel = 1;
    }

    public void LoadNextLevel()
    {
        gameState = GameState.Idle;
        ChangeSpawnPoint(null);
        SceneManager.LoadSceneAsync("Level0" + currentLevel.ToString());
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        gameState = GameState.Lost;
        PauseGamePlay();
        Debug.Log("Game Over!");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        TimeController.OnTimeOut -= GameOver;
        PlayerController.OnPlayerFall -= GameOver;
        EndingPoint.OnPlayerWon -= Win;
        CheckPoint.OnPlayerCheckpoint -= ChangeSpawnPoint;
        UIController.OnLoadNextLevelClicked -= LoadNextLevel;
    }
}
