using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionBackup : MonoBehaviour
{

    public GameObject currentObject; // 最新的重生点
    public GameObject parent; // 整个最上层的父物体 (version2)
    private SpringJoint springJoint; // 圆柱体的 Spring Joint
    public GameObject connectedGameObject; // 你想连接的 GameObject

    private Rigidbody originalConnectedBody; // 原始连接的刚体

    // Start is called before the first frame update
    void Start()
    {
        // 获取 Spring Joint 组件
        springJoint = GetComponent<SpringJoint>();

        // 从 connectedGameObject 获取 Rigidbody
        if (springJoint != null && connectedGameObject != null)
        {
            originalConnectedBody = connectedGameObject.GetComponent<Rigidbody>(); // 从 GameObject 获取 Rigidbody
            springJoint.connectedBody = originalConnectedBody; // 将 SpringJoint 连接到 Rigidbody
        }
    }

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
            currentObject = other.gameObject; // 更新 currentObject 为新的存档点
        }
    }

    // 将 version2（parent）传送到 currentObject（存档点）并保持圆柱体直立
    private void TeleportToCheckpoint()
    {
        // 暂时断开 Spring Joint 以防止干扰
        if (springJoint != null)
        {
            springJoint.connectedBody = null;
        }

        // 停止所有物理运动，确保 version2 内的物体静止
        Rigidbody[] rigidbodies = parent.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // 将所有刚体设置为 Kinematic，暂时禁止物理运动
        }

        // 传送 parent 到当前的存档点位置
        parent.transform.position = currentObject.transform.position;
        parent.transform.rotation = currentObject.transform.rotation;

        // 保持圆柱体直立（只调整圆柱体的 X 和 Z 轴的旋转）
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // 保持 Y 轴旋转，重置 X 和 Z 轴
        transform.rotation = Quaternion.Euler(0, 0, 0); // 重置 所有轴


        // 重置 connectedRigiBody 的旋转为 (0, 0, 0)
        if (originalConnectedBody != null)
        {
            originalConnectedBody.transform.rotation = Quaternion.identity; // 旋转重置为 (0, 0, 0)
            Debug.Log("connectedRigiBody's rotation has been reset to (0, 0, 0)");
        }

        // 强制更新物理状态
        Physics.SyncTransforms();

        Debug.Log("Version2 and all attached objects have returned to the checkpoint and stopped.");

        // 在一定时间后恢复物理属性
        StartCoroutine(StopSwingingAndReconnectSpringJoint());
    }

    // 逐步恢复物理状态，并重新连接 Spring Joint
    private IEnumerator StopSwingingAndReconnectSpringJoint()
    {
        // 延长等待时间至 1 秒，确保物体完全静止
        yield return new WaitForSeconds(1.0f);

        // 恢复物理运动
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // 恢复物理运动
            //rb.angularVelocity = Vector3.zero; // 确保角速度为零，停止任何摇摆
        }

        // 重新连接 Spring Joint
        if (springJoint != null && originalConnectedBody != null)
        {
            originalConnectedBody.isKinematic = false;

            // 调低 SpringJoint 的弹性参数，防止抖动
            //springJoint.spring = 100f;  // 根据实际情况调整 spring 参数
            //springJoint.damper = 19f;  // 减小阻尼，减少弹性

            springJoint.connectedBody = originalConnectedBody; // 重新连接 Spring Joint
            Debug.Log("Spring Joint reconnected");
        }

        // 等待摇摆稳定，施加反向力矩以停止摇摆
        yield return new WaitForSeconds(0.5f);
        if (rb != null)
        {
            rb.AddTorque(-rb.angularVelocity * rb.mass, ForceMode.VelocityChange); // 施加反向力矩
        }

        // 稳定后，如果需要，重新启用 `IsKinematic`
        yield return new WaitForSeconds(0.5f);
        if (springJoint != null && originalConnectedBody != null)
        {
            originalConnectedBody.isKinematic = true; // 恢复 Kinematic
        }
    }
}
