using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    public Button btnSelf;
    public Image imageNew;
    public Image imgState;
    public Text textName;
    private ServerInfo nowServerInfo;
    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;

            //隐藏选服面板
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            //显示服务器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }
    //初始化方法，用于更新按钮显示相关
    public void InitInfo(ServerInfo info)
    {
        nowServerInfo = info;
        //更新按钮上的信息
        textName.text = info.id + "区    " + info.name;
        imageNew.gameObject.SetActive(info.isNew);
        imgState.gameObject.SetActive(true);
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Login");
        switch (info.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1:
                //流畅
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                break;
            case 2:
                //繁忙
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                break;
            case 3:
                //火爆
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                break;
            case 4:
                //维护
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}
