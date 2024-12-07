using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//出牌阶段
public class Phase_Play : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        controller.nextPhaseAction += EndPhase;
    }
    public override void Exit()
    {
        base.Exit();

        controller.nextPhaseAction -= EndPhase;
    }



    //结束出牌阶段
    private void EndPhase()
    {
        stateMachine.SwitchState(typeof(Phase_Settle));
    }
}
