using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StateMachine
{
    Dictionary<Type, State> states = new Dictionary<Type, State>();

    public  State currentState;


    /// <summary>
    /// 添加状态方法
    /// </summary>
    /// <param name="stateMachine"></param>
    /// <param name="state"></param>
    public void AddState(State state)
    {
        Type type = state.GetType();
        if(!states.ContainsKey(type))
        {
            states.Add(type, state);
        }
    }
    public void SwitchState<T>() where T:State
    {
        if(states.TryGetValue(typeof(T),out State newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.On_Enter();
        }
        else
        {
            UnityEngine.Debug.LogWarning($"状态 {typeof(T)} 不存在");
        }
           
    }
    public void Update() => currentState.Update();

    public void FixedUpdate() => currentState.FixedUpdate();
}
