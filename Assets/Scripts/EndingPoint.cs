using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameController.Instance.Win();
        }
    }
}
