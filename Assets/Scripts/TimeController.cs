using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public static Action OnTimeOut;
    public static Action<int> OnTimeUpdate;

    private int timeLeft = 5;
    private bool _gameOver = false;
    private Coroutine timeCoroutine;

    private void OnEnable()
    {
        GameController.OnLevelLoaded += ResetTime;
    }

    private IEnumerator AdvanceTime()
    {
        while (!_gameOver)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
            OnTimeUpdate?.Invoke(timeLeft);
            if (timeLeft <= 0)
            {
                _gameOver = true;
                OnTimeOut?.Invoke();
            }
        }
    }

    private void ResetTime()
    {
        timeLeft = 300;
        OnTimeUpdate?.Invoke(timeLeft);
        _gameOver = false;
        if (timeCoroutine != null)
            StopCoroutine(timeCoroutine);

        timeCoroutine = StartCoroutine(AdvanceTime());
    }
    private void OnDisable()
    {
        GameController.OnLevelLoaded -= ResetTime;
    }
}

