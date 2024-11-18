using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//出牌阶段
public class Phase_Play : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        //xxxAction += EndRound();
    }
    public override void Exit()
    {
        base.Exit();

        //xxxAction -= EndRound();
    }



    //结束出牌阶段
    private void EndRound()
    {
        stateMachine.SwitchState(typeof(Phase_Settle));
    }
}
