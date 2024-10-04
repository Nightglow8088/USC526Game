using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 startPosition;
    public float speed = 1.0f;

    void OnCollisionEnter(Collision collision)
    {
        // Reset objecy back to start position
        transform.position = startPosition;

        // Reset its velocity if it's a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the A/D keys for horizontal movement (X-axis)
        float moveHorizontal = Input.GetAxis("Horizontal");  // A/D or Left/Right arrows for X-axis movement

        // Get input from the W/S keys for forward/backward movement (Z-axis)
        float moveVertical = Input.GetAxis("Vertical");  // W/S or Up/Down arrows for Z-axis movement

        // Get input from the Up/Down arrow keys for vertical movement (Y-axis)
        float moveY = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1f; // Move up
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f; // Move down
        }

        // Combine the horizontal and vertical movement into a direction vector
        Vector3 movement = new Vector3(moveHorizontal, moveY, -moveVertical);

        // Move the object in the direction vector at the specified speed
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }
}
