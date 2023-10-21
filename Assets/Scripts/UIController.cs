using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public void UpdateTimeText(string time)
    {
        timeText.text = time;
    }
}
