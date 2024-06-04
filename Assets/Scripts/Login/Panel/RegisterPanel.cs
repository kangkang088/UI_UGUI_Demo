using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnCancel;
    public Button btnSure;
    public InputField inputUserName;
    public InputField inputPassword;
    public override void Init()
    {
        btnCancel.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<RegisterPanel>();
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        btnSure.onClick.AddListener(() =>
        {
            //判断账号密码是否合理
            if (inputUserName.text.Length <= 6 || inputPassword.text.Length <= 6)
            {
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("账号和密码必须大于6位");
                return;
            }
            //合理，判断注册是否成功
            if (LoginMgr.Instance.RegisterUser(inputUserName.text, inputPassword.text))
            {
                LoginMgr.Instance.ClearLoginData();
                //注册成功
                LoginPanel panel = UIManager.Instance.ShowPanel<LoginPanel>();
                //更新登录面板的内容，避免重新输入一次
                panel.SetInfo(inputUserName.text, inputPassword.text);
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //注册失败
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用户名已经存在");
                inputUserName.text = "";
                inputPassword.text = "";
            }

        });
    }
}
