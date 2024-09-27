using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private Vector3 startPosition;
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
        // Get input from the left and right arrow keys
        float moveDirection = Input.GetAxis("Horizontal"); // Returns -1 for left, 1 for right

        // Move the object along the Z axis
        transform.Translate(Vector3.forward * moveDirection * speed * Time.deltaTime);
    }
}
