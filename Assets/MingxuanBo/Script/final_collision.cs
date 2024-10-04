using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final : MonoBehaviour
{
    public GameObject checkPoint; // 最新的重生点
    public GameObject ball;
    public GameObject joint;
    public GameObject magnent;


    // 检测与 "tube" 的碰撞事件
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube")) // 如果碰撞的对象有 "tube" 标签
        {
            HideObjectAndChildren(joint);
            HideObjectAndChildren(magnent);

            Debug.Log("Tube Collision Detected, Returning to checkpoint");
            TeleportToCheckpoint();

            StartCoroutine(ShowHiddenAfterDelay());



        }
    }

    IEnumerator ShowHiddenAfterDelay()
    {
        // 等待一秒
        yield return new WaitForSeconds(0.6f);

        // 一秒后显示对象和其子对象
        ShowObjectAndChildren(joint);
        ShowObjectAndChildren(magnent);
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
        //parent.transform.position = checkPoint.transform.position;

        foreach (Transform child in ball.transform)
        {
            // 检查子对象是否有 Rigidbody 组件
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 检查子对象的名字是否是 "ball"，如果是则跳过
                if (child.name == "ball")
                {
                    continue;  // 跳过 ball
                }
                // 停止所有的移动和旋转
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }


    }

    public void HideObjectAndChildren(GameObject parentObject)
    {
        // 先隐藏父对象的 MeshRenderer
        MeshRenderer parentMeshRenderer = parentObject.GetComponent<MeshRenderer>();
        if (parentMeshRenderer != null)
        {
            parentMeshRenderer.enabled = false;
        }

        // 遍历父对象的所有子对象并隐藏它们的 MeshRenderer
        foreach (Transform child in parentObject.transform)
        {
            MeshRenderer childMeshRenderer = child.GetComponent<MeshRenderer>();
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false;
            }
        }
    }


    public void ShowObjectAndChildren(GameObject parentObject)
    {
        // 先隐藏父对象的 MeshRenderer
        MeshRenderer parentMeshRenderer = parentObject.GetComponent<MeshRenderer>();
        if (parentMeshRenderer != null)
        {
            parentMeshRenderer.enabled = true;
        }

        // 遍历父对象的所有子对象并隐藏它们的 MeshRenderer
        foreach (Transform child in parentObject.transform)
        {
            MeshRenderer childMeshRenderer = child.GetComponent<MeshRenderer>();
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = true;
            }
        }
    }



}
