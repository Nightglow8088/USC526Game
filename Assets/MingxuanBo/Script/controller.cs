using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controller : MonoBehaviour
{
    public float moveSpeed;  // 移动速度
    public float verticalSpeed;  // 上下移动速度
    private bool canMoveUp = true;  // 控制是否可以继续向上移动

    void Update()
    {
        // 获取 WASD 按键的输入
        float horizontal = 0f;
        float forward = 0f;

        // A 和 D 控制左右移动 (X轴)
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }

        // W 和 S 控制前后移动 (Z轴)
        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }

        // 获取上下箭头键的输入，只影响Y轴
        float rise = 0f;
        if (canMoveUp)  // 只有在允许的情况下才能上升
        {
            if (Input.GetKey(KeyCode.UpArrow))  // 上箭头上升
            {
                rise = verticalSpeed;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))  // 下箭头下降
        {
            rise = -verticalSpeed;
        }

        // 计算移动向量
        Vector3 movement = new Vector3(horizontal, rise, forward) * moveSpeed * Time.deltaTime;

        // 应用移动
        transform.Translate(movement);
    }

    // 触发器检测，停止向上移动
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestCube"))
        {
            canMoveUp = false;  // 碰到 "testitem" 时停止向上移动
        }
    }

    // 当物体离开触发器时，允许再次向上移动
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestCube"))
        {
            canMoveUp = true;  // 离开 "testitem" 时恢复向上移动
        }
    }
}
