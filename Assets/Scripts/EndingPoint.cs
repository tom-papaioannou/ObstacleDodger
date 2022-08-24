using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPoint : MonoBehaviour
{

    public GameObject gameControllerObject;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            gameController.Win();
        }
    }
}
