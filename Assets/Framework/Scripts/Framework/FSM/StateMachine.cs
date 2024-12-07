using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //当前处于的状态类型，不参与逻辑，只给外部看的
    public Type currentStateType = null;

    private IState _currentState = null;

    protected Dictionary<Type, IState> stateTable;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="states">状态数组</param>
    /// <param name="stateMachine">状态机</param>
    /// <param name="param">传入的数据</param>
    protected void Init<T, U>(T[] states, U stateMachine, params object[] param) where T : StateBase where U : StateMachine
    {
        stateTable = new Dictionary<Type, IState>(states.Length);

        foreach (var state in states)
        {
            stateTable.Add(state.GetType(), state);
            state.Init(stateMachine, param);
        }
    }
    private void Update()
    {
        _currentState.Update();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }
    protected void SwitchOn(IState newState)
    {
        _currentState = newState;
        _currentState.Enter();
    }
    protected void SwitchState(IState newState)
    {
        _currentState?.Exit();
        SwitchOn(newState);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newStateType"></param>
    public void SwitchState(Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
}
