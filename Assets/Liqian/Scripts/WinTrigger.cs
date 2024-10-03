using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Canvas winCanvas; // 胜利界面的Canvas引用
    public string targetTag = "Item"; // 触发胜利的物体标签，默认为"Player"

    void Start()
    {
        // 确保有Canvas引用
        if (winCanvas == null)
        {
            Debug.LogError("Win Canvas is not assigned to the WinTrigger script!");
        }
        else
        {
            // 开始时隐藏Canvas
            winCanvas.gameObject.SetActive(false);
        }

        // 确保物体有碰撞器且设置为触发器
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No Collider found on the destination plane!");
        }
        else
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的物体是否具有目标标签
        if (other.CompareTag(targetTag))
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        if (winCanvas != null)
        {
            winCanvas.gameObject.SetActive(true);
            
            // 可选：暂停游戏
            Time.timeScale = 0f;
        }
    }
}