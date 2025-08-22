using UnityEngine;
using UnityEngine.InputSystem; // NEW input system

public class CarMovement : MonoBehaviour
{
    public float acceleration = 500f;
    public float steering = 15f;
    public float maxSpeed = 20f;
    public float brakeForce = 800f;

    private Rigidbody rb;

    private float moveInput;
    private float steerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        // NEW input system
        if (Keyboard.current != null)
        {
            moveInput = 0;
            if (Keyboard.current.wKey.isPressed) moveInput = 1f;
            if (Keyboard.current.sKey.isPressed) moveInput = -1f;

            steerInput = 0;
            if (Keyboard.current.aKey.isPressed) steerInput = -1f;
            if (Keyboard.current.dKey.isPressed) steerInput = 1f;
        }
    }

    void FixedUpdate()
    {
        // Forward / backward
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * acceleration);
        }

        // Steering
        if (rb.linearVelocity.magnitude > 1f)
        {
            transform.Rotate(Vector3.up * steerInput * steering * Time.fixedDeltaTime);
        }

        // Brake
        if (Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(-rb.linearVelocity * brakeForce * Time.fixedDeltaTime);
        }
    }
}
