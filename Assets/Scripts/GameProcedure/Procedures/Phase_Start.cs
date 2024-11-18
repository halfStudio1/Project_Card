using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回合开始阶段
public class Phase_Start : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        //初始化卡组
        controller.InitDeck();
        controller.Shuffle();
        //加载战斗UI，加载完成后进入摸牌阶段
        UIMgr.Instance.ShowPanel<BattlePanel>(E_CanvasType.Top, LoadComplete);

    }

    private void LoadComplete(BattlePanel panel)
    {
        //进入到摸牌阶段
        stateMachine.SwitchState(typeof(Phase_Draw));
        battlePanel = panel;
        controller.battlePanel = panel;
    }
}
