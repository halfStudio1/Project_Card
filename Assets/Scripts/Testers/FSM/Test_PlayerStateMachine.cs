/// <summary>
/// 玩家状态机
/// </summary>
public class Test_PlayerStateMachine : StateMachine
{
    private Test_PlayerStateBase[] states = new Test_PlayerStateBase[]
    {
        //TODO 填入具体状态
        new Test_PlayerState_1(),
        new Test_PlayerState_2(),
    };

    public Test_PlayerController player;

    private void Awake()
    {
        //初始化状态
        Init(states, this, player);
        //切换状态
        SwitchState(typeof(Test_PlayerState_1));
    }
}
