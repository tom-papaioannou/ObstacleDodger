using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static Action OnPlayerFall;

    private Rigidbody rb;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float minMoveThreshold = 0.1f;
    [SerializeField] private float maxMoveThreshold = 0.6f;
    [SerializeField] private float fellDownThreshold = -30.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (CheckIfPlayerFell())
            Destroy(this);

    // On editor the game uses the arrow buttons, but on the Android devices it uses the accelerometer to move the ball
#if UNITY_EDITOR
        rb.AddTorque(new Vector3((Input.GetAxis("Vertical") * speed * Time.deltaTime) / rb.mass, 0.0f, -(Input.GetAxis("Horizontal") * speed * Time.deltaTime) / rb.mass));
#else
        float x = Input.acceleration.x > minMoveThreshold ? (Input.acceleration.x > maxMoveThreshold ? maxMoveThreshold - minMoveThreshold : Input.acceleration.x - minMoveThreshold) : Input.acceleration.x < (-minMoveThreshold) ? (Input.acceleration.x < (- maxMoveThreshold) ? (- maxMoveThreshold) + minMoveThreshold : Input.acceleration.x + minMoveThreshold) : 0.0f;
        float z = Input.acceleration.z > minMoveThreshold ? (Input.acceleration.z > maxMoveThreshold ? maxMoveThreshold - minMoveThreshold : Input.acceleration.z - minMoveThreshold) : Input.acceleration.z < (-minMoveThreshold) ? (Input.acceleration.z < (- maxMoveThreshold) ? (- maxMoveThreshold) + minMoveThreshold : Input.acceleration.z + minMoveThreshold) : 0.0f;
        rb.AddTorque(new Vector3(- ((2 * z * speed * Time.deltaTime)/rb.mass), 0.0f, - ((2 * x * speed * Time.deltaTime)/rb.mass)));
#endif
    }

    bool CheckIfPlayerFell()
    {
        // Triggers a Game Over Event for the GameController to handle
        if (transform.position.y < fellDownThreshold)
        {
            OnPlayerFall?.Invoke();
            return true;
        }

        return false;
    }
}
