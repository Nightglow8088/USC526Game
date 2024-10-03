using System.Collections;
using UnityEngine;

public class Rope_detector : MonoBehaviour
{
    public Transform cylinder; // Բ�����λ��
    private GameObject firstDetectedItem = null; // �������ȼ�⵽������
    private GameObject currentAttachedItem = null; // ��ǰ����������
    private bool isItemAttached = false; // �Ƿ�������������
    private float cylinderRadius; // Բ����İ뾶

    // ��ʼ��ʱ��ȡԲ����İ뾶
    void Start()
    {
        // ��ȡԲ����İ뾶������Բ����ʹ���� Collider��
        CapsuleCollider capsuleCollider = cylinder.GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            cylinderRadius = capsuleCollider.radius * cylinder.localScale.x; // ��������
        }
    }

    void Update()
    {
        // ���û�����������壬���� P ����������
        if (Input.GetKeyDown(KeyCode.P) && firstDetectedItem != null && !isItemAttached)
        {
            StartCoroutine(MoveItemToBottom(firstDetectedItem));
        }

        // ��������������壬���� O ���ͷŵ�ǰ����������
        if (Input.GetKeyDown(KeyCode.O) && isItemAttached && currentAttachedItem != null)
        {
            ReleaseItem();
        }
    }

    // ��̬��ȡԲ����ĵײ�λ��
    private Vector3 GetCylinderBottomPosition()
    {
        // Բ����ײ�λ�ã����ݵ�ǰԲ�����ʵʱλ�ã�
        float fullHeight = cylinder.localScale.y;
        return cylinder.position - new Vector3(0, fullHeight / 2, 0); // ����: ʹ�ø߶ȵ�һ�룬ȷ���ײ�����
    }

    // ����������� trigger ʱ����
    private void OnTriggerEnter(Collider other)
    {
        // ֻ�����һ�������� "Item" ���壬���ҵ�ǰû�����屻����
        if (other.CompareTag("Item") && firstDetectedItem == null && !isItemAttached)
        {
            firstDetectedItem = other.gameObject; // �����һ����⵽������
            Debug.Log("��⵽�����壺" + firstDetectedItem.name); // ������Ϣ
        }
    }

    // �������뿪 trigger ʱ����
    private void OnTriggerExit(Collider other)
    {
        // �������뿪 trigger �����ü�⣬׼��������һ������
        if (other.gameObject == firstDetectedItem && !isItemAttached)
        {
            Debug.Log("�����뿪 trigger��" + firstDetectedItem.name); // ������Ϣ
            firstDetectedItem = null; // ���ü��
        }
    }

    // �������嵽Բ����ĵ׶��ⲿ
    private IEnumerator MoveItemToBottom(GameObject item)
    {
        float speed = 5f; // �����ٶ�
        Rigidbody itemRb = item.GetComponent<Rigidbody>();

        if (itemRb != null)
        {
            // ��ʱ���� Rigidbody �������ֹ��������
            itemRb.detectCollisions = false; // �����������ײ
            itemRb.useGravity = false;
            itemRb.isKinematic = true; // ��Ϊ Kinematic��ȷ�����岻��Ӱ������ϵͳ
        }

        // ��ȡ����� Collider ��С
        Collider itemCollider = item.GetComponent<Collider>();
        float itemHeight = 0f;
        if (itemCollider != null)
        {
            itemHeight = itemCollider.bounds.size.y; // ��ȡ����ĸ߶�
        }

        // ��������
        while (item != null)
        {
            // ��ȡԲ���嵱ǰ�ײ�λ��
            Vector3 bottomPosition = GetCylinderBottomPosition();

            // ���������㣺������ĵײ����뵽Բ����ĵײ�                 ԭ����itemHeight / 2
            Vector3 targetPosition = bottomPosition - new Vector3(0, itemHeight  + 0.05f, 0); // ������ȷ������ײ����뵽Բ����ײ�

            // ���ƶ����嵽Բ����׶����
            item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, speed * Time.deltaTime);

            // ����Ѿ��ӽ�Ŀ��λ�ã��˳�ѭ��
            if (Vector3.Distance(item.transform.position, targetPosition) <= 0.1f)
                break;

            yield return null;
        }

        // ������ɺ󣬽���������ΪԲ������Ӷ���ȷ���������Բ�����˶�
        item.transform.SetParent(cylinder);
        currentAttachedItem = item; // ��¼��ǰ����������
        isItemAttached = true; // ��������屻����
        firstDetectedItem = null; // ���ü��
    }

    // �ͷŵ�ǰ���������壬���ָ�����������
    private void ReleaseItem()
    {
        if (currentAttachedItem != null)
        {
            Rigidbody itemRb = currentAttachedItem.GetComponent<Rigidbody>();

            if (itemRb != null)
            {
                itemRb.detectCollisions = true; // ����������ײ
                itemRb.useGravity = true; // ��������
                itemRb.isKinematic = false; // �ָ�������Ϊ
            }

            // �������Բ����ĸ��������Ƴ�
            currentAttachedItem.transform.SetParent(null);
            currentAttachedItem = null; // �������������
            isItemAttached = false; // ���û�����屻����
            Debug.Log("�������ͷ�");
        }
    }
}
