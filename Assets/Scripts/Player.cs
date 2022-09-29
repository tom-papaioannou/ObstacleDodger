using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float moveThreshold = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        rb.AddForce(new Vector3((Input.GetAxis("Horizontal") * speed * Time.deltaTime) / rb.mass, 0.0f, (Input.GetAxis("Vertical") * speed * Time.deltaTime) / rb.mass));
#else
        float x = Input.acceleration.x > moveThreshold ? Input.acceleration.x - moveThreshold : Input.acceleration.x < -moveThreshold ? Input.acceleration.x + moveThreshold : 0.0f;
        float z = Input.acceleration.z > moveThreshold ? Input.acceleration.z - moveThreshold : Input.acceleration.z < -moveThreshold ? Input.acceleration.z + moveThreshold : 0.0f;
        rb.AddForce(new Vector3((x * speed * Time.deltaTime)/ rb.mass, 0.0f, (z * speed * Time.deltaTime)/rb.mass));
#endif
    }
}
