using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeController.OnTimeUpdate += UpdateTimeText;
    }

    private void UpdateTimeText(int time)
    {
        timeText.text = time.ToString();
    }

    private void OnDisable()
    {
        TimeController.OnTimeUpdate -= UpdateTimeText;
    }
}
