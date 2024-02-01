using System;
using UnityEngine;

public class EndingPoint : MonoBehaviour
{
    public static Action OnPlayerWon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            OnPlayerWon?.Invoke();
        }
    }
}
