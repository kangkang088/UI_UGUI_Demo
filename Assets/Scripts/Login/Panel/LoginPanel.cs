using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnRegister;
    public Button btnSure;
    public InputField inputUserName;
    public InputField inputPassword;
    public Toggle remerberPassword;
    public Toggle autoLogin;
    public override void Init()
    {
        btnRegister.onClick.AddListener(() => 
        {
            //显示注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        });
        btnSure.onClick.AddListener(() => 
        {
            //点击登录，验证用户名和密码。

            //判断账号密码是否合理
            if (inputUserName.text.Length <= 6 || inputPassword.text.Length <= 6)
            {
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("账号和密码必须大于6位");
                return;
            }
            //合理，验证用户名和密码是否正确
            if (LoginMgr.Instance.CheckInfo(inputUserName.text, inputPassword.text))
            {
                //记录数据
                LoginMgr.Instance.LoginData.userName = inputUserName.text;
                LoginMgr.Instance.LoginData.password = inputPassword.text;
                LoginMgr.Instance.LoginData.rememberPassword = remerberPassword.isOn;
                LoginMgr.Instance.LoginData.autoLogin = autoLogin.isOn;
                LoginMgr.Instance.SaveLoginData();

                //根据服务器信息判断显示哪个面板
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //没选过服务器，第一次登录，那么直接打开选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //之前登录过，直接打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
            }//不合理
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误！");   
            }
        });
        remerberPassword.onValueChanged.AddListener((isOn) => 
        {
            //取消记住密码时，自动登录也要取消
            if (!isOn)
            {
                autoLogin.isOn = false;
            }
        });
        autoLogin.onValueChanged.AddListener((isOn) => 
        {
            //自动登录选中时，记住密码也要被选中
            if (isOn)
            {
                remerberPassword.isOn = true;
            }
        });
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己时，根据数据更新初始面板的内容
        LoginData loginData = LoginMgr.Instance.LoginData;
        //初始化面板显示
        remerberPassword.isOn = loginData.rememberPassword;
        autoLogin.isOn = loginData.autoLogin;
        inputUserName.text = loginData.userName;
        if (remerberPassword.isOn)
            inputPassword.text = loginData.password;
        if (autoLogin.isOn)
        {
            //自动去验证账号密码相关
            if (LoginMgr.Instance.CheckInfo(inputUserName.text, inputPassword.text))
            {
                //根据服务器信息判断显示哪个面板
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //没选过服务器，第一次登录，那么直接打开选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //之前登录过，直接打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误!");
            }
        }
    }
    //提供给外部设置用户名密码
    public void SetInfo(string userName, string password)
    {
        inputUserName.text = userName;
        inputPassword.text = password;
    }
}
