using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3((Input.GetAxis("Horizontal") * speed * Time.deltaTime)/ rb.mass, 0.0f, (Input.GetAxis("Vertical") * speed * Time.deltaTime)/rb.mass));
    }
}
