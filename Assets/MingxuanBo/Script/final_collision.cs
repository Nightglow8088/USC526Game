using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final : MonoBehaviour
{
    public GameObject checkPoint; // ���µ�������
    public GameObject ball;



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
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation
        }

        StopMovement(ball);  // Stop movement of the ball

    }


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

}
