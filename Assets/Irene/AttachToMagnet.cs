using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToMagnet : MonoBehaviour
{
    public LayerMask jointLayer;
    public int playerLayer;

    public string tagToAdd;
    //public GameObject joint;

    private Renderer objectRenderer;
    private Rigidbody objectRigidbody;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(tagToAdd))
        {
            other.transform.SetParent(transform);
            //other.transform.SetParent(joint.transform);
            
            other.gameObject.layer = playerLayer;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position - other.transform.position.normalized, out hit, Mathf.Infinity, jointLayer))
            {
                other.transform.forward = hit.normal;

                other.transform.position = hit.point;
                
                // Offset the position slightly along the forward direction
                other.transform.position += other.transform.forward * other.transform.localScale.z * 0.5f;

            }
            
        }
    }


    void Start()
    {
        // Get the Renderer component
        objectRenderer = GetComponent<Renderer>();
        
        // Get the Rigidbody component
        objectRigidbody = GetComponent<Rigidbody>();

        // Check if the Renderer and Rigidbody are available
        if (objectRenderer != null)
        {
            // Disable the Renderer to make the object invisible
            //objectRenderer.enabled = false;
        }

        if (objectRigidbody != null)
        {
            // Rigidbody is still active, so no need to do anything here
            // You can access its properties as needed
        }
    }
}
