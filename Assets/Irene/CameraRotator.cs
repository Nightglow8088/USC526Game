using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed;
    void Update()
    {
        // transform the camera to rotate around the y axis with scroll wheel
        // Maximum rotation 180 degrees

        // Detect input if the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            // Rotate the camera counter-clockwise around the object
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), speed * Time.deltaTime);
        }
        else if (Input.GetMouseButton(0))
        {
            // Rotate the camera clockwise around the object
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), speed * Time.deltaTime);

        }

        // DEBUG: Rotate camera around object automatically
        //transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
