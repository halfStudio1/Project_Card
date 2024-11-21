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

    private UnityAction _onCompelete;

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
            BattleController.Instance.FoldCard(foldCard.id);
        }

        foldCardSelectList.Clear();
        _onCompelete?.Invoke();
        _onCompelete = null;
        UIMgr.Instance.HidePanel<FoldCardPanel>();
    }

    //传入手牌然后创建出来，传入需要丢弃的牌的数量，传入UI结束过后执行的
    public void Init(List<Card> handCards, int needFoldNum, UnityAction onCompelete)
    {
        //如果没有手牌了
        if (handCards.Count <= 0)
        {
            Debugger.LogPink("没有手牌，不用弃置");
            onCompelete?.Invoke();
            UIMgr.Instance.HidePanel<FoldCardPanel>();
            return;
        }

        //如果手牌数量小于应该弃置的牌数量
        if (handCards.Count < needFoldNum)
        {
            needFoldNum = handCards.Count;
        }

        _needFoldNum = needFoldNum;
        _onCompelete = onCompelete;

        for (int i = 0; i < handCards.Count; i++)
        {
            CreateCard(i, handCards[i]);
        }
    }

    //创建卡牌到UI
    private void CreateCard(int id, Card card)
    {
        ResMgr.Instance.LoadAssetAsync<GameObject>("FoldCard", (obj) =>
        {
            obj = Instantiate(obj, foldCardGroup);
            obj.GetComponent<FoldCard>().OnLoad(id, card);
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
