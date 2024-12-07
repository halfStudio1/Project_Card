using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseStateMachine : StateMachine
{
    private PhaseBase[] states = new PhaseBase[]
    {
        //TODO 填入具体状态
        new Phase_Init(),
        new Phase_Start(),
        new Phase_Draw(),
        new Phase_StopDraw(),
        new Phase_Play(),
        new Phase_Settle(),
        new Phase_MonsterAction(),
        new Phase_Win(),
        new Phase_Lose()
    };

    public void Init(BattleController controller)
    {
        //初始化状态
        Init(states, this, controller);
        //切换状态
        SwitchState(typeof(Phase_Init));
    }
}
