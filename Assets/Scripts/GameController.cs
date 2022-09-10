using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (!gameOver)
        {
            CheckIfGameOver();
        }
    }

    void CheckIfGameOver()
    {
        if (player.transform.position.y < gameOverThreshold)
        {
            GameOver();
        }
    }

    public void Win(string levelToLoad)
    {
        winPanel.SetActive(true);
        Debug.Log("Level Complete!");
        SceneManager.LoadSceneAsync(levelToLoad);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOver = true;
        Time.timeScale = 0.0f;
        Debug.Log("Game Over!");
    }

}
