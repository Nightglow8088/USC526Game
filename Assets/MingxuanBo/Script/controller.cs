using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controller : MonoBehaviour
{
    public float moveSpeed;  // �ƶ��ٶ�
    public float verticalSpeed;  // �����ƶ��ٶ�
    private bool canMoveUp = true;  // �����Ƿ���Լ��������ƶ�

    void Update()
    {
        // ��ȡ WASD ����������
        float horizontal = 0f;
        float forward = 0f;

        // A �� D ���������ƶ� (X��)
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }

        // W �� S ����ǰ���ƶ� (Z��)
        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }

        // ��ȡ���¼�ͷ�������룬ֻӰ��Y��
        float rise = 0f;
        if (canMoveUp)  // ֻ�������������²�������
        {
            if (Input.GetKey(KeyCode.UpArrow))  // �ϼ�ͷ����
            {
                rise = verticalSpeed;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))  // �¼�ͷ�½�
        {
            rise = -verticalSpeed;
        }

        // �����ƶ�����
        Vector3 movement = new Vector3(horizontal, rise, forward) * moveSpeed * Time.deltaTime;

        // Ӧ���ƶ�
        transform.Translate(movement);
    }

    // ��������⣬ֹͣ�����ƶ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestCube"))
        {
            canMoveUp = false;  // ���� "testitem" ʱֹͣ�����ƶ�
        }
    }

    // �������뿪������ʱ�������ٴ������ƶ�
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestCube"))
        {
            canMoveUp = true;  // �뿪 "testitem" ʱ�ָ������ƶ�
        }
    }
}
