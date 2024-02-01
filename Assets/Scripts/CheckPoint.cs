using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Action<Transform> OnPlayerCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("CheckPoint!");
            OnPlayerCheckpoint?.Invoke(gameObject.transform);
        }
    }
}
