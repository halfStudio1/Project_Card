using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_Init : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        //初始化数据
        controller.InitData();
        //加载战斗UI，加载完成后进入摸牌阶段
        UIMgr.Instance.ShowPanel<BattlePanel>(E_CanvasType.Top, LoadComplete);

    }

    private void LoadComplete(BattlePanel panel)
    {
        battlePanel = panel;
        controller.battlePanel = panel;
        panel.battleController = controller;
        panel.enemyView.Init(controller.enemyobj);
        //进入到摸牌阶段
        stateMachine.SwitchState(typeof(Phase_Start));
    }
}
