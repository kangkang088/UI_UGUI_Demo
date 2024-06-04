using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 10.0f;
    private bool isShow = false;
    private UnityAction hideCallback;
    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }
    protected virtual void Start()
    {
        Init();
    }
    /// <summary>
    /// 初始化按钮事件监听等内容
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 显示自己以及要处理的逻辑
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    /// <summary>
    /// 隐藏自己以及要处理的逻辑
    /// </summary>
    public virtual void Hide(UnityAction callback)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        hideCallback = callback;
    }
    protected virtual void Update()
    {
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //面板管理器删除自己
                hideCallback?.Invoke();
            }
        }
    }
}
