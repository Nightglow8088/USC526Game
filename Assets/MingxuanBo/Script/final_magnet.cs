using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_magnet : MonoBehaviour
{
    public LayerMask jointLayerPart;
    public int playerLayerPart;

    public string targetTag;
    //public GameObject joint;

    private Renderer objectRenderer;
    private Rigidbody objectRigidbody;


    private Rigidbody attachedRigidbody = null;
    private Transform attachedObject = null;
    private Collider attachedCollider = null;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(targetTag))
        {

            // 获取Rigidbody并禁用物理作用
            attachedRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = true;  // 将Rigidbody设置为Kinematic，禁止物理作用
                attachedRigidbody.useGravity = false;  // 禁用重力
            }

            // 获取Collider并禁用碰撞
            attachedCollider = other.gameObject.GetComponent<Collider>();
            if (attachedCollider != null)
            {
                attachedCollider.enabled = false;  // 禁用碰撞
            }

            // 吸附物体
            attachedObject = other.transform;

            other.transform.SetParent(transform);
            //other.transform.SetParent(joint.transform);

            other.gameObject.layer = playerLayerPart;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position - other.transform.position.normalized, out hit, Mathf.Infinity, jointLayerPart))
            {
                other.transform.forward = hit.normal;

                other.transform.position = hit.point;

                // Offset the position slightly along the forward direction
                other.transform.position += other.transform.forward * other.transform.localScale.z * 0.5f;

            }

            //黏贴完碰撞还是开启下
            //attachedCollider.enabled = true;  // 禁用碰撞



        }
    }



    private void Update()
    {
        // 检查是否按下空格键
        if (attachedObject != null && Input.GetKey(KeyCode.Space))
        {
            ReleaseAttachedObject();
        }
    }

    private void ReleaseAttachedObject()
    {
        if (attachedObject != null)
        {
            // 取消吸附
            attachedObject.SetParent(null);

            // 重新启用物体的Rigidbody
            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = false;  // 恢复物理效果
                attachedRigidbody.useGravity = true;    // 启用重力
            }

            // 恢复碰撞
            if (attachedCollider != null)
            {
                attachedCollider.enabled = true;  // 启用碰撞
            }

            // 重置引用
            attachedRigidbody = null;
            attachedCollider = null;
            attachedObject = null;
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
