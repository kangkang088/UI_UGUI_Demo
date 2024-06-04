using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnStart;
    public Button btnCancel;
    public Button btnChange;
    public Text textServerName;
    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            //进入游戏
            //canvas过场景不会被移除,所以下面的面板也要都隐藏掉
            UIManager.Instance.HidePanel<ServerPanel>();
            LoginMgr.Instance.SaveLoginData();
            UIManager.Instance.HidePanel<LoginBKPanel>();
            //切换场景
            SceneManager.LoadScene("GameScene");

        });
        btnCancel.onClick.AddListener(() => 
        {
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;
            UIManager.Instance.ShowPanel<LoginPanel>();
            UIManager.Instance.HidePanel<ServerPanel>(); 
        });
        btnChange.onClick.AddListener(() => {
            //显示服务器列表面板，隐藏自己
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己的时候更新之前选择的服务器名字和区号
        //根据ID得出服务器数据
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
        {
            textServerName.text = "无";
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            textServerName.text = info.id + "区  " + info.name;
        }
        
        
    }
}
