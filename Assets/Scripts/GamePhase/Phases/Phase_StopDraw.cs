using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//停止摸牌
public class Phase_StopDraw : PhaseBase
{
    public override void Enter()
    {
        base.Enter();



        //切换到出牌阶段
        stateMachine.SwitchState(typeof(Phase_Play));
    }
}
