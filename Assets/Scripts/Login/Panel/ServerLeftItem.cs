using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem :MonoBehaviour
{
    public Button btnSelf;
    public Text textInfo;
    private int beginIndex;
    private int endIndex;
    private void Start()
    {
        btnSelf.onClick.AddListener(() => 
        {
            //通知选服面板右侧改变
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdataPanel(beginIndex, endIndex);
        });
    }
    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;
        textInfo.text = beginIndex + "—" + endIndex + "区";
    }
}
