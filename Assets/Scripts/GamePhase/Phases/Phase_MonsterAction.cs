using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_MonsterAction : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        controller.MonsterAction();

        stateMachine.SwitchState(typeof(Phase_Start));
    }
}
