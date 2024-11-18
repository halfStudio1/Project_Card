using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBase : StateBase
{
    protected BattleController controller;
    protected BattlePanel battlePanel;
    public override void Init(StateMachine stateMachine, params object[] param)
    {
        base.Init(stateMachine, param);

        controller = param[0] as BattleController;
    }

    public override void Enter()
    {
        base.Enter();

        Debugger.LogCyan("进入阶段" + this.ToString());
    }
}
