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

    public float stopTime = 1.0f;  // Time to stop movement after collision

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

    IEnumerator OnCollisionEnter()  // Use IEnumerator to allow WaitForSeconds
    {
        cube.transform.SetParent(null);  // Unparent the cube from the joint

        transform.position = jointStartPosition;
        transform.rotation = Quaternion.identity;  // Reset rotation to zero
        ball.transform.position = ballStartPosition;
        ball.transform.rotation = Quaternion.identity;  // Reset rotation to zero
        cube.transform.position = cubeStartPosition;
        cube.transform.rotation = Quaternion.identity;  // Reset rotation to zero

        // Reset its velocity if it's a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;  // Freeze all movement and rotation

            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation

            StopMovement(magnetCollider);

            // Wait for the specified stop time
            yield return new WaitForSeconds(stopTime);

            rb.constraints = RigidbodyConstraints.None;  // Unfreeze all movement and rotation
        }
    }


    /*
    void OnCollisionEnter(Collision collision)
    {
        cube.transform.SetParent(null);  // Unparent the cube from the joint

        transform.position = jointStartPosition;
        ball.transform.position = ballStartPosition;
        ball.transform.rotation = Quaternion.identity;  // Reset rotation to zero
        cube.transform.position = cubeStartPosition;
        cube.transform.rotation = Quaternion.identity;  // Reset rotation to zero

        // Reset its velocity if it's a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation

            rb.constraints = RigidbodyConstraints.FreezeAll;  // Freeze all movement and rotation

            // Wait for the specified stop time
            yield return new WaitForSeconds(stopTime);

            rb.constraints = RigidbodyConstraints.None;  // Unfreeze all movement and rotation
        }

        
        StopMovement(magnetCollider);  // Stop movement of the magnet collider
    }
    */



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
