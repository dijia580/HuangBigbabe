using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkState : State
{
    private Enemy enemy;
    public AtkState(StateMachine stateMachine) : base(stateMachine)
    {
    }
}
