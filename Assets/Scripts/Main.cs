using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
        //tipPanel.ChangeInfo("测试提示内容改变");
        UIManager.Instance.ShowPanel<LoginBKPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
