using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverPanel;
    private float gameOverThreshold = -50.0f;
    private bool gameOver = false;

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

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOver = true;
        Debug.Log("Game Over!");
    }

}
