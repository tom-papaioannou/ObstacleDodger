using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float timeLeft = 300.0f;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (((int)timeLeft) <= 0)
        {
            GameController.Instance.GameOver();
        }
        GameController.Instance.UpdateTimeUI(((int)timeLeft).ToString());
    }

    public void ResetTime()
    {
        timeLeft = 300.0f;
    }
}
