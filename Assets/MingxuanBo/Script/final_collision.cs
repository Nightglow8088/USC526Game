using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class final : MonoBehaviour
{
    public Vector3 jointStartPosition;
    public Vector3 ballStartPosition;
    public Vector3 magnentStartPosition;


    public GameObject checkPoint; 
    public GameObject ball;
    public GameObject magnetCollider;
    public GameObject joint;

    //public GameObject targetItem;
    public GameObject nextPosition;


    //public float stopTime = 1.0f;  // Time to stop movement after collision


    void Start()
    {
        jointStartPosition = joint.transform.position;
        ballStartPosition = ball.transform.position;
        magnentStartPosition = magnetCollider.transform.position;
    }

    public void freezerTool(GameObject currentObject, Vector3 position)
    {
        currentObject.transform.position = position;
        currentObject.transform.rotation = Quaternion.identity;  // Reset rotation to zero

        Rigidbody currentRB= currentObject.GetComponent<Rigidbody>();
        currentRB.constraints = RigidbodyConstraints.FreezeAll;  // Freeze all movement and rotation

        currentRB.velocity = Vector3.zero;  // Stop all movement
        currentRB.angularVelocity = Vector3.zero;  // Stop rotation


    }

    public void UnFreezeerTool(GameObject currentObject)
    {

        Rigidbody currentRB = currentObject.GetComponent<Rigidbody>();
        currentRB.constraints = RigidbodyConstraints.None;  // Unfreeze all movement and rotation

    }



    // ����� "tube" ����ײ�¼�
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube")) 
        {

            Debug.Log("Tube Collision Detected, Returning to checkpoint");
            freezerTool(ball,ballStartPosition);
            freezerTool(magnetCollider, magnentStartPosition);
            freezerTool(gameObject, jointStartPosition);

            //transform.position = jointStartPosition;
            //transform.rotation = Quaternion.identity;  // Reset rotation to zero
            //ball.transform.position = ballStartPosition;
            //ball.transform.rotation = Quaternion.identity;  // Reset rotation to zero
            //magnetCollider.transform.position = magnentStartPosition;
            //magnetCollider.transform.rotation = Quaternion.identity;  // Reset rotation to zero


            //// Reset its velocity if it's a Rigidbody
            //Rigidbody rb = GetComponent<Rigidbody>();
            //Rigidbody magnentRb = magnetCollider.GetComponent<Rigidbody>();

            //rb.constraints = RigidbodyConstraints.FreezeAll;  // Freeze all movement and rotation
            //magnentRb.constraints = RigidbodyConstraints.FreezeAll;  // Freeze all movement and rotation


            //rb.velocity = Vector3.zero;  // Stop all movement
            //rb.angularVelocity = Vector3.zero;  // Stop rotation
            //magnentRb.velocity = Vector3.zero;  // Stop all movement
            //magnentRb.angularVelocity = Vector3.zero;  // Stop rotation


            // Wait for the specified stop time
            //yield return new WaitForSeconds(stopTime);

            UnFreezeerTool(ball);
            UnFreezeerTool(magnetCollider);
            UnFreezeerTool(gameObject);

            //rb.constraints = RigidbodyConstraints.None;  // Unfreeze all movement and rotation
            //magnentRb.constraints = RigidbodyConstraints.None;  // Unfreeze all movement and rotation


        }
    }

    // ��� savePoint �Ĵ������¼�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("savePoint")) // �����ײ�Ķ����� "savePoint" ��ǩ
        {
            Debug.Log("Save Point Reached, updating checkpoint.");
            jointStartPosition = nextPosition.transform.Find("Joint").transform.position;
            ballStartPosition = nextPosition.transform.Find("Ball").transform.position;
            magnentStartPosition = nextPosition.transform.Find("Magnet Collider").transform.position;

        }
    }




}
