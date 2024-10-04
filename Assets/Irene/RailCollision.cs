using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCollision : MonoBehaviour
{
    public Vector3 jointStartPosition;
    public Vector3 ballStartPosition;
    public Vector3 cubeStartPosition;

    public GameObject ball;
    public GameObject magnetCollider;
    public GameObject cube;

    public void StopMovement(GameObject otherObject)
    {
        // Check if the other object has a Rigidbody
        Rigidbody rb = otherObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Stop all movement and rotation
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        cube.transform.SetParent(null);  // Unparent the cube from the joint

        // Reset object back to start position
        transform.position = jointStartPosition;
        ball.transform.position = ballStartPosition;
        cube.transform.position = cubeStartPosition;
        cube.transform.rotation = Quaternion.identity;  // Reset rotation to zero

        StopMovement(ball);  // Stop movement of the ball
        // Reset its velocity if it's a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation
        }

        
        StopMovement(magnetCollider);  // Stop movement of the magnet collider
    }



    // Start is called before the first frame update
    void Start()
    {
        jointStartPosition = transform.position;
        ballStartPosition = ball.transform.position;
        cubeStartPosition = cube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
