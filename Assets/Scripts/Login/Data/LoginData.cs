using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 登录界面需要的一些数据
/// </summary>
public class LoginData 
{
    public string userName;
    public string password;
    public bool rememberPassword;
    public bool autoLogin;
    //-1代表没有选择过服务器
    public int frontServerID = -1;
}
