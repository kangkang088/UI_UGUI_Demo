using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance => instance;
    private LoginData loginData;
    public LoginData LoginData => loginData;

    private RegisterData registerData;
    public RegisterData RegisterData => registerData;
    //所有服务器数据
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData => serverData;
    private LoginMgr()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }
    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
    //注册成功后清理注册数据
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.autoLogin = false;
        loginData.rememberPassword = false;
    }
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }
    //注册方法
    public bool RegisterUser(string userName, string password)
    {
        if (registerData.registerInfo.ContainsKey(userName))
        {
            return false;
        }
        else
        {
            registerData.registerInfo.Add(userName, password);
            SaveRegisterData();
            return true;
        }
    }
    //验证用户名和密码是否正确
    public bool CheckInfo(string userName, string password)
    {
        if (registerData.registerInfo.ContainsKey(userName))
        {
            if (registerData.registerInfo[userName] == password)
            {
                return true;
            }
        }
        return false;
    }
}
