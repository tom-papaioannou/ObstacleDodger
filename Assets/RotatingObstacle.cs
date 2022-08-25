using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public float rotatingSpeed = 5.0f;
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, 10.0f, 0.0f) * rotatingSpeed * Time.deltaTime);
    }
}
