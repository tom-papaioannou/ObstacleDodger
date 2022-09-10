using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float timeLeft = 300.0f;
    public TextMeshProUGUI timeText;
    public GameObject gameControllerObject;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (((int)timeLeft) <= 0)
        {
            gameControllerObject.GetComponent<GameController>().GameOver();
        }
        timeText.text = ((int)timeLeft).ToString();
    }
}
