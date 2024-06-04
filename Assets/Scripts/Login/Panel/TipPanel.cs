using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnSure;
    public Text textInfo;
    public override void Init()
    {
        btnSure.onClick.AddListener(() => 
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    public void ChangeInfo(string info)
    {
        textInfo.text = info;
    }
}
