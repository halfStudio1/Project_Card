/// <summary>
/// 玩家状态基类
/// </summary>
public class Test_PlayerStateBase : StateBase
{
    protected Test_PlayerController player;

    public override void Init(StateMachine stateMachine, params object[] param)
    {
        base.Init(stateMachine, param);

        player = param[0] as Test_PlayerController;
    }
}
