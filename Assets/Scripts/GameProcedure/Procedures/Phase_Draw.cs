using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//摸牌阶段
public class Phase_Draw : PhaseBase
{
    public override void Enter()
    {
        base.Enter();

        //摸完牌判断点数

        //如果大于21，玩家扣血，如果玩家血量<=0，进入玩家失败
        //stateMachine.SwitchState(typeof(Procedure_Lose));

        //如果小于21，让玩家选择
    }
}
