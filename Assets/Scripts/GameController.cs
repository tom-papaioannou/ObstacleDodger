using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0.0f;
        Debug.Log("Level Complete!");
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOver = true;
        Debug.Log("Game Over!");
    }

}
