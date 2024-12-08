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

        controller.round++;
        controller.roundStart?.Invoke();

        stateMachine.SwitchState(typeof(Phase_Draw));
    }
}
