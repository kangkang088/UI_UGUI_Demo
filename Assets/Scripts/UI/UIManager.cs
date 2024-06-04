using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;
    private UIManager()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        //Canvas过场景不移除
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }
    //存储面板的容器
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    //开始时就获取Canvas对象
    private Transform canvasTrans;

    //显示(动态创建)面板
    public T ShowPanel<T>() where T : BasePanel
    {
        //定义一个规则，保证T的类型名和面板名一样，方便使用
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //动态创建面板预设体，设置父对象(Canvas)
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);

        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);

        panel.ShowMe();

        return panel;
    }

    //隐藏(动态删除)面板
    //参数一：希望淡出，传true。希望直接删除，传false
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {

                panelDic[panelName].Hide(() =>
                {
                    //面板淡出成功后删除面板
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
                
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }

    //获得面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //没有直接返回空
        return null;
    }
}