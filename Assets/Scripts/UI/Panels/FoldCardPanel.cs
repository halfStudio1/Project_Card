using cfg.card;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FoldCardPanel : BasePanel
{
    public Button Btn_Fold;

    public Transform foldCardGroup;

    private List<FoldCard> foldCardSelectList = new List<FoldCard>();

    //需要丢弃的数量
    private int _needFoldNum;

    public override void HideMe()
    {
        gameObject.SetActive(false);
    }
    public override void ShowMe()
    {
        gameObject.SetActive(true);
    }
    private void Awake()
    {
        Btn_Fold.onClick.AddListener(OnBtn_FoldClick);
    }

    private void OnBtn_FoldClick()
    {
        if (foldCardSelectList.Count != _needFoldNum)
        {
            Debugger.LogPink($"你必须要弃置{_needFoldNum}张牌");
            return;
        }

        foreach (FoldCard foldCard in foldCardSelectList)
        {
            BattleController.Instance.FoldCard(foldCard.cardView);
        }

        foldCardSelectList.Clear();
        RemoveAllChildren(foldCardGroup);
        UIMgr.Instance.HidePanel<FoldCardPanel>();
    }
    public void RemoveAllChildren(Transform parent)
    {
        Transform transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            transform = parent.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }


    public void Init(BattlePanel battlePanel, int needFoldNum)
    {
        int handCardsNum = battlePanel.cardGroup.cards.Count;
        if (handCardsNum <= 0)
        {
            Debugger.LogPink("没有手牌，不用弃置");
            UIMgr.Instance.HidePanel<FoldCardPanel>();
            return;
        }

        //如果手牌数量小于应该弃置的牌数量
        if (handCardsNum < needFoldNum)
        {
            needFoldNum = handCardsNum;
        }

        _needFoldNum = needFoldNum;

        for (int i = 0; i < handCardsNum; i++)
        {
            CreateCard(battlePanel.cardGroup.cards[i]);
        }
    }

    //创建卡牌到UI
    private void CreateCard(CardView cardView)
    {
        ResMgr.Instance.LoadAssetAsync<GameObject>("FoldCard", (obj) =>
        {
            obj = Instantiate(obj, foldCardGroup);
            obj.GetComponent<FoldCard>().Init(cardView);
        });
    }

    public void SelectFold(FoldCard foldCard)
    {
        //如果没有选择该卡片
        if (!foldCardSelectList.Contains(foldCard))
        {
            //如果选择的卡片大于要丢弃的卡片
            if (foldCardSelectList.Count >= _needFoldNum)
            {
                //取消选择第一张
                foldCardSelectList[0].OnCancel();
            }

            foldCardSelectList.Add(foldCard);
        }
        else
        {
            foldCard.OnCancel();
        }
    }

    public void CancelFold(FoldCard foldCard)
    {
        foldCardSelectList.Remove(foldCard);
    }

}
