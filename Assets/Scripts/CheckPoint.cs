using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Action<Transform> OnPlayerCheckpoint;

    // Next time the player loses they will spawn from this location
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("CheckPoint!");
            OnPlayerCheckpoint?.Invoke(gameObject.transform);
        }
    }
}
