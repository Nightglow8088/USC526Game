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
        // Get input from the left and right arrow keys (or A/D keys) for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");  // X-axis movement (left/right)

        // Get input from the up and down arrow keys (or W/S keys) for vertical movement
        float moveVertical = Input.GetAxis("Vertical");  // Z-axis movement (forward/backward)

        // Combine the horizontal and vertical movement into a direction vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the object in the direction vector at the specified speed
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
