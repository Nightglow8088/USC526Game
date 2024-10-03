using System.Collections;
using UnityEngine;

public class Rope_detector : MonoBehaviour
{
    public Transform cylinder; // 圆柱体的位置
    private GameObject firstDetectedItem = null; // 保存最先检测到的物体
    private GameObject currentAttachedItem = null; // 当前吸附的物体
    private bool isItemAttached = false; // 是否有吸附的物体
    private float cylinderRadius; // 圆柱体的半径

    // 初始化时获取圆柱体的半径
    void Start()
    {
        // 获取圆柱体的半径（假设圆柱体使用了 Collider）
        CapsuleCollider capsuleCollider = cylinder.GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            cylinderRadius = capsuleCollider.radius * cylinder.localScale.x; // 考虑缩放
        }
    }

    void Update()
    {
        // 如果没有吸附的物体，按下 P 键触发吸附
        if (Input.GetKeyDown(KeyCode.P) && firstDetectedItem != null && !isItemAttached)
        {
            StartCoroutine(MoveItemToBottom(firstDetectedItem));
        }

        // 如果有吸附的物体，按下 O 键释放当前吸附的物体
        if (Input.GetKeyDown(KeyCode.O) && isItemAttached && currentAttachedItem != null)
        {
            ReleaseItem();
        }
    }

    // 动态获取圆柱体的底部位置
    private Vector3 GetCylinderBottomPosition()
    {
        // 圆柱体底部位置（根据当前圆柱体的实时位置）
        float fullHeight = cylinder.localScale.y;
        return cylinder.position - new Vector3(0, fullHeight / 2, 0); // 修正: 使用高度的一半，确保底部对齐
    }

    // 当有物体进入 trigger 时触发
    private void OnTriggerEnter(Collider other)
    {
        // 只保存第一个碰到的 "Item" 物体，并且当前没有物体被吸附
        if (other.CompareTag("Item") && firstDetectedItem == null && !isItemAttached)
        {
            firstDetectedItem = other.gameObject; // 保存第一个检测到的物体
            Debug.Log("检测到的物体：" + firstDetectedItem.name); // 调试信息
        }
    }

    // 当物体离开 trigger 时重置
    private void OnTriggerExit(Collider other)
    {
        // 当物体离开 trigger 后重置检测，准备吸附下一个物体
        if (other.gameObject == firstDetectedItem && !isItemAttached)
        {
            Debug.Log("物体离开 trigger：" + firstDetectedItem.name); // 调试信息
            firstDetectedItem = null; // 重置检测
        }
    }

    // 吸附物体到圆柱体的底端外部
    private IEnumerator MoveItemToBottom(GameObject item)
    {
        float speed = 5f; // 吸附速度
        Rigidbody itemRb = item.GetComponent<Rigidbody>();

        if (itemRb != null)
        {
            // 暂时禁用 Rigidbody 组件，防止物理作用
            itemRb.detectCollisions = false; // 禁用物体的碰撞
            itemRb.useGravity = false;
            itemRb.isKinematic = true; // 设为 Kinematic，确保物体不会影响物理系统
        }

        // 获取物体的 Collider 大小
        Collider itemCollider = item.GetComponent<Collider>();
        float itemHeight = 0f;
        if (itemCollider != null)
        {
            itemHeight = itemCollider.bounds.size.y; // 获取物体的高度
        }

        // 吸附过程
        while (item != null)
        {
            // 获取圆柱体当前底部位置
            Vector3 bottomPosition = GetCylinderBottomPosition();

            // 计算吸附点：将物体的底部对齐到圆柱体的底部                 原本是itemHeight / 2
            Vector3 targetPosition = bottomPosition - new Vector3(0, itemHeight  + 0.05f, 0); // 修正：确保物体底部对齐到圆柱体底部

            // 逐步移动物体到圆柱体底端外侧
            item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, speed * Time.deltaTime);

            // 如果已经接近目标位置，退出循环
            if (Vector3.Distance(item.transform.position, targetPosition) <= 0.1f)
                break;

            yield return null;
        }

        // 吸附完成后，将物体设置为圆柱体的子对象，确保物体跟随圆柱体运动
        item.transform.SetParent(cylinder);
        currentAttachedItem = item; // 记录当前吸附的物体
        isItemAttached = true; // 标记有物体被吸附
        firstDetectedItem = null; // 重置检测
    }

    // 释放当前吸附的物体，并恢复其物理属性
    private void ReleaseItem()
    {
        if (currentAttachedItem != null)
        {
            Rigidbody itemRb = currentAttachedItem.GetComponent<Rigidbody>();

            if (itemRb != null)
            {
                itemRb.detectCollisions = true; // 重新启用碰撞
                itemRb.useGravity = true; // 启用重力
                itemRb.isKinematic = false; // 恢复物理行为
            }

            // 将物体从圆柱体的父对象中移除
            currentAttachedItem.transform.SetParent(null);
            currentAttachedItem = null; // 清空吸附的物体
            isItemAttached = false; // 标记没有物体被吸附
            Debug.Log("物体已释放");
        }
    }
}
