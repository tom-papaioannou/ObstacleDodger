using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static Action OnLoadNextLevelClicked;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverPanel, winPanel;

    private void OnEnable()
    {
        TimeController.OnTimeUpdate += UpdateTimeText;
        GameController.OnLevelLoaded += HidePanels;
        GameController.OnLevelWin += ShowWinPanel;
        GameController.OnGameOver += ShowGameOverPanel;
    }

    public void LoadNextLevelClicked()
    {
        OnLoadNextLevelClicked?.Invoke();
    }

    private void HidePanels()
    {
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void UpdateTimeText(int time)
    {
        timeText.text = time.ToString();
    }

    private void OnDisable()
    {
        TimeController.OnTimeUpdate -= UpdateTimeText;
        GameController.OnLevelLoaded -= HidePanels;
        GameController.OnLevelWin -= ShowWinPanel;
        GameController.OnGameOver -= ShowGameOverPanel;
    }
}
