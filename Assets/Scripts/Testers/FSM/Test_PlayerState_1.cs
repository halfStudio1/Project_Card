/// <summary>
/// 玩家状态1
/// </summary>
public class Test_PlayerState_1 : Test_PlayerStateBase
{
    public override void Update()
    {
        base.Update();

        if(StateDuration >= 4f)
        {
            player.TestDebug();
            stateMachine.SwitchState(typeof(Test_PlayerState_2));
        }
    }
}