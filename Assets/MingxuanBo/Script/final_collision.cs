using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class final : MonoBehaviour
{
    public GameObject checkPoint; // ���µ�������
    public GameObject ball;
    public GameObject magnetCollider;
    public GameObject targetItem;



    // ����� "tube" ����ײ�¼�
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube")) // �����ײ�Ķ����� "tube" ��ǩ
        {
            Debug.Log("Tube Collision Detected, Returning to checkpoint");
            TeleportToCheckpoint();
        }
    }

    // ��� savePoint �Ĵ������¼�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("savePoint")) // �����ײ�Ķ����� "savePoint" ��ǩ
        {
            Debug.Log("Save Point Reached, updating checkpoint.");
            checkPoint = other.gameObject; // ���� currentObject Ϊ�µĴ浵��
        }
    }

    private void TeleportToCheckpoint(){
        ball.transform.position = checkPoint.transform.position;
        ball.transform.rotation = Quaternion.identity;  // Reset rotation to zero

        StopMovement(ball);  // Stop movement of the ball
        transform.rotation = Quaternion.identity;  // Reset rotation to zero
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation

        }

        
        StopMovement(magnetCollider);  // Stop movement of the magnet collider
        StopMovement(targetItem);  // Stop movement of the target item
    }


    public void StopMovement(GameObject otherObject)
    {
        // Check if the other object has a Rigidbody
        Rigidbody rb = otherObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                //rb.MoveRotation(Quaternion.identity);
            }
            else 
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

}
