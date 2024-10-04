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

            // ��ȡRigidbody��������������
            attachedRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = true;  // ��Rigidbody����ΪKinematic����ֹ��������
                attachedRigidbody.useGravity = false;  // ��������
            }

            // ��ȡCollider��������ײ
            attachedCollider = other.gameObject.GetComponent<Collider>();
            if (attachedCollider != null)
            {
                attachedCollider.enabled = false;  // ������ײ
            }

            // ��������
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

            //�������ײ���ǿ�����
            //attachedCollider.enabled = true;  // ������ײ



        }
    }



    private void Update()
    {
        // ����Ƿ��¿ո��
        if (attachedObject != null && Input.GetKey(KeyCode.Space))
        {
            ReleaseAttachedObject();
        }
    }

    private void ReleaseAttachedObject()
    {
        if (attachedObject != null)
        {
            // ȡ������
            attachedObject.SetParent(null);

            // �������������Rigidbody
            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = false;  // �ָ�����Ч��
                attachedRigidbody.useGravity = true;    // ��������
            }

            // �ָ���ײ
            if (attachedCollider != null)
            {
                attachedCollider.enabled = true;  // ������ײ
            }

            // ��������
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
