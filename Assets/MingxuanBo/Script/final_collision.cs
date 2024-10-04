using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final : MonoBehaviour
{
    public GameObject checkPoint; // ���µ�������
    public GameObject ball;
    public GameObject joint;
    public GameObject magnent;


    // ����� "tube" ����ײ�¼�
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube")) // �����ײ�Ķ����� "tube" ��ǩ
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
        // �ȴ�һ��
        yield return new WaitForSeconds(0.6f);

        // һ�����ʾ��������Ӷ���
        ShowObjectAndChildren(joint);
        ShowObjectAndChildren(magnent);
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
        //parent.transform.position = checkPoint.transform.position;

        foreach (Transform child in ball.transform)
        {
            // ����Ӷ����Ƿ��� Rigidbody ���
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ����Ӷ���������Ƿ��� "ball"�������������
                if (child.name == "ball")
                {
                    continue;  // ���� ball
                }
                // ֹͣ���е��ƶ�����ת
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }


    }

    public void HideObjectAndChildren(GameObject parentObject)
    {
        // �����ظ������ MeshRenderer
        MeshRenderer parentMeshRenderer = parentObject.GetComponent<MeshRenderer>();
        if (parentMeshRenderer != null)
        {
            parentMeshRenderer.enabled = false;
        }

        // ����������������Ӷ����������ǵ� MeshRenderer
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
        // �����ظ������ MeshRenderer
        MeshRenderer parentMeshRenderer = parentObject.GetComponent<MeshRenderer>();
        if (parentMeshRenderer != null)
        {
            parentMeshRenderer.enabled = true;
        }

        // ����������������Ӷ����������ǵ� MeshRenderer
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
