using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final : MonoBehaviour
{
    public GameObject checkPoint; // 最新的重生点
    public GameObject ball;



    // 检测与 "tube" 的碰撞事件
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube")) // 如果碰撞的对象有 "tube" 标签
        {
            Debug.Log("Tube Collision Detected, Returning to checkpoint");
            TeleportToCheckpoint();
        }
    }

    // 检测 savePoint 的触发器事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("savePoint")) // 如果碰撞的对象有 "savePoint" 标签
        {
            Debug.Log("Save Point Reached, updating checkpoint.");
            checkPoint = other.gameObject; // 更新 currentObject 为新的存档点
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
