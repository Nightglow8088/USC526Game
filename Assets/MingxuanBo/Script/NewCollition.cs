using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCollition : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject rope;


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("aaaaaaaaaaaaaaaaaa");
        // Reset objecy back to start position
        rope.transform.position = new Vector3(0.49f, 0.53f, 0.19f);
            //currentObject.transform.position;

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
    //    startPosition = transform.position;
    }

}
