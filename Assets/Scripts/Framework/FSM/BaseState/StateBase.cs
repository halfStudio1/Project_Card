using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public class StateBase : IState
{
    //记录状态开始时间
    private float _stateStartTime;
    //记录状态已持续时间
    protected float StateDuration => Time.time - _stateStartTime;
    //持有该状态的状态机
    protected StateMachine stateMachine;

    //初始化状态，只在Base状态中使用
    public virtual void Init(StateMachine stateMachine, params object[] param) { this.stateMachine = stateMachine; }
    //状态进入
    public virtual void Enter() { _stateStartTime = Time.time; }
    //状态退出
    public virtual void Exit() { }
    //状态Update
    public virtual void Update() { }
    //状态FixedUpdate
    public virtual void FixedUpdate() { }
}
