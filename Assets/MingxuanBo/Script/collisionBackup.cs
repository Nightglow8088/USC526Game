using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionBackup : MonoBehaviour
{

    public GameObject currentObject; // ���µ�������
    public GameObject parent; // �������ϲ�ĸ����� (version2)
    private SpringJoint springJoint; // Բ����� Spring Joint
    public GameObject connectedGameObject; // �������ӵ� GameObject

    private Rigidbody originalConnectedBody; // ԭʼ���ӵĸ���

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ Spring Joint ���
        springJoint = GetComponent<SpringJoint>();

        // �� connectedGameObject ��ȡ Rigidbody
        if (springJoint != null && connectedGameObject != null)
        {
            originalConnectedBody = connectedGameObject.GetComponent<Rigidbody>(); // �� GameObject ��ȡ Rigidbody
            springJoint.connectedBody = originalConnectedBody; // �� SpringJoint ���ӵ� Rigidbody
        }
    }

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
            currentObject = other.gameObject; // ���� currentObject Ϊ�µĴ浵��
        }
    }

    // �� version2��parent�����͵� currentObject���浵�㣩������Բ����ֱ��
    private void TeleportToCheckpoint()
    {
        // ��ʱ�Ͽ� Spring Joint �Է�ֹ����
        if (springJoint != null)
        {
            springJoint.connectedBody = null;
        }

        // ֹͣ���������˶���ȷ�� version2 �ڵ����徲ֹ
        Rigidbody[] rigidbodies = parent.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // �����и�������Ϊ Kinematic����ʱ��ֹ�����˶�
        }

        // ���� parent ����ǰ�Ĵ浵��λ��
        parent.transform.position = currentObject.transform.position;
        parent.transform.rotation = currentObject.transform.rotation;

        // ����Բ����ֱ����ֻ����Բ����� X �� Z �����ת��
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // ���� Y ����ת������ X �� Z ��
        transform.rotation = Quaternion.Euler(0, 0, 0); // ���� ������


        // ���� connectedRigiBody ����תΪ (0, 0, 0)
        if (originalConnectedBody != null)
        {
            originalConnectedBody.transform.rotation = Quaternion.identity; // ��ת����Ϊ (0, 0, 0)
            Debug.Log("connectedRigiBody's rotation has been reset to (0, 0, 0)");
        }

        // ǿ�Ƹ�������״̬
        Physics.SyncTransforms();

        Debug.Log("Version2 and all attached objects have returned to the checkpoint and stopped.");

        // ��һ��ʱ���ָ���������
        StartCoroutine(StopSwingingAndReconnectSpringJoint());
    }

    // �𲽻ָ�����״̬������������ Spring Joint
    private IEnumerator StopSwingingAndReconnectSpringJoint()
    {
        // �ӳ��ȴ�ʱ���� 1 �룬ȷ��������ȫ��ֹ
        yield return new WaitForSeconds(1.0f);

        // �ָ������˶�
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // �ָ������˶�
            //rb.angularVelocity = Vector3.zero; // ȷ�����ٶ�Ϊ�㣬ֹͣ�κ�ҡ��
        }

        // �������� Spring Joint
        if (springJoint != null && originalConnectedBody != null)
        {
            originalConnectedBody.isKinematic = false;

            // ���� SpringJoint �ĵ��Բ�������ֹ����
            //springJoint.spring = 100f;  // ����ʵ��������� spring ����
            //springJoint.damper = 19f;  // ��С���ᣬ���ٵ���

            springJoint.connectedBody = originalConnectedBody; // �������� Spring Joint
            Debug.Log("Spring Joint reconnected");
        }

        // �ȴ�ҡ���ȶ���ʩ�ӷ���������ֹͣҡ��
        yield return new WaitForSeconds(0.5f);
        if (rb != null)
        {
            rb.AddTorque(-rb.angularVelocity * rb.mass, ForceMode.VelocityChange); // ʩ�ӷ�������
        }

        // �ȶ��������Ҫ���������� `IsKinematic`
        yield return new WaitForSeconds(0.5f);
        if (springJoint != null && originalConnectedBody != null)
        {
            originalConnectedBody.isKinematic = true; // �ָ� Kinematic
        }
    }
}
