using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//结算阶段
public class Phase_Settle : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        //结算逻辑
        controller.Settle();

        stateMachine.SwitchState(typeof(Phase_MonsterAction));
    }
}
