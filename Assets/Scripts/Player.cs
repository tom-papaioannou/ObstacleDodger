using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float minMoveThreshold = 0.1f;
    [SerializeField] private float maxMoveThreshold = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        rb.AddTorque(new Vector3((Input.GetAxis("Vertical") * speed * Time.deltaTime) / rb.mass, 0.0f, - (Input.GetAxis("Horizontal") * speed * Time.deltaTime) / rb.mass));
#else
        float x = Input.acceleration.x > minMoveThreshold ? (Input.acceleration.x > maxMoveThreshold ? maxMoveThreshold - minMoveThreshold : Input.acceleration.x - minMoveThreshold) : Input.acceleration.x < (-minMoveThreshold) ? (Input.acceleration.x < (- maxMoveThreshold) ? (- maxMoveThreshold) + minMoveThreshold : Input.acceleration.x + minMoveThreshold) : 0.0f;
        float z = Input.acceleration.z > minMoveThreshold ? (Input.acceleration.z > maxMoveThreshold ? maxMoveThreshold - minMoveThreshold : Input.acceleration.z - minMoveThreshold) : Input.acceleration.z < (-minMoveThreshold) ? (Input.acceleration.z < (- maxMoveThreshold) ? (- maxMoveThreshold) + minMoveThreshold : Input.acceleration.z + minMoveThreshold) : 0.0f;
        rb.AddTorque(new Vector3(- ((2 * z * speed * Time.deltaTime)/rb.mass), 0.0f, - ((2 * x * speed * Time.deltaTime)/rb.mass)));
#endif
    }
}
