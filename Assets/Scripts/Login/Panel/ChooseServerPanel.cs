using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    public ScrollRect scrollRectLeft;
    public ScrollRect scrollRectRight;
    public Text textFrontName;
    public Image imgFrontState;
    public Text textServerArea;

    //用于存储右侧按钮们
    private List<GameObject> itemList = new List<GameObject>();
    public override void Init()
    {
        //动态创建左侧区间按钮
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        int num = Mathf.CeilToInt(infoList.Count / 5.0f);
        for (int i = 0; i < num; i++)
        {
            //动态创建预设体对象
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));
            obj.transform.SetParent(scrollRectLeft.content, false);
            //初始化
            ServerLeftItem serverLeftItem = obj.GetComponent<ServerLeftItem>();
            int beiginIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            serverLeftItem.InitInfo(beiginIndex, endIndex);
        }
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //初始化上一次登录信息和右侧信息
        if (LoginMgr.Instance.LoginData.frontServerID <= 0)
        {
            textFrontName.text = "无";
            imgFrontState.gameObject.SetActive(false);
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[LoginMgr.Instance.LoginData.frontServerID - 1];
            textFrontName.text = info.id + "区  " + info.name;
            imgFrontState.gameObject.SetActive(true);
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Login");
            switch (info.state)
            {
                case 0:
                    imgFrontState.gameObject.SetActive(false);
                    break;
                case 1:
                    //流畅
                    imgFrontState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                    break;
                case 2:
                    //繁忙
                    imgFrontState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                    break;
                case 3:
                    //火爆
                    imgFrontState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                    break;
                case 4:
                    //维护
                    imgFrontState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        //更新当前选择的区间
        UpdataPanel(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
    }
    //提供给其他地方 用于更新 当前选择区间的右侧按钮
    public void UpdataPanel(int beginIndex, int endIndex)
    {
        //更新服务器区间显示
        textServerArea.text = "服务器" + beginIndex + "—" + endIndex;
        //删除之前的单个按钮
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i].gameObject);
        }
        itemList.Clear();
        //创建新的按钮
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //获取服务器信息
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i - 1];
            //动态创建预设体
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(scrollRectRight.content,false);
            ServerRightItem info = serverItem.GetComponent<ServerRightItem>();
            info.InitInfo(nowInfo);
            itemList.Add(serverItem);
        }

    }
}
