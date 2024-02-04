using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public static Action OnTimeOut;
    public static Action<int> OnTimeUpdate;

    private int _timeLeft = 5;
    private float _timeFactor = 1.0f;
    private bool _gameOver = false;
    private Coroutine _timeCoroutine;

    private void OnEnable()
    {
        GameController.OnLevelLoaded += ResetTime;
    }

    // Coroutine that decreases time each second.
    // Smaller _timeFactor means the countdown goes faster.
    // Larger _timeFactor slows down the countdown.
    private IEnumerator AdvanceTime()
    {
        while (!_gameOver)
        {
            yield return new WaitForSeconds(_timeFactor);
            _timeLeft--;
            OnTimeUpdate?.Invoke(_timeLeft);
            if (_timeLeft <= 0)
            {
                _gameOver = true;
                OnTimeOut?.Invoke();
            }
        }
    }

    private void ResetTime()
    {
        _timeLeft = 300;
        OnTimeUpdate?.Invoke(_timeLeft);
        _gameOver = false;
        if (_timeCoroutine != null)
            StopCoroutine(_timeCoroutine);

        _timeCoroutine = StartCoroutine(AdvanceTime());
    }
    private void OnDisable()
    {
        GameController.OnLevelLoaded -= ResetTime;
    }
}

