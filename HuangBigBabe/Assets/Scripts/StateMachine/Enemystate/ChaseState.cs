using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class ChaseState : State
{
    Enemy enemy;
    private float ChaseSpeed = 3f;

    public ChaseState(StateMachine stateMachine,Enemy enemy) : base(stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Update()
    {

        CheckState();
    }
    public override void FixedUpdate()
    {
        enemy.rb.velocity =new Vector2( enemy.movedir.normalized.x * ChaseSpeed,0);
    }
    private void CheckState()
    {
        if (!enemy.PlayerInRange)
        {

            stateMachine.SwitchState<ReturnState>();
        }

        if (enemy.movedir.magnitude <= enemy.atkDistance)
        {
            stateMachine.SwitchState<E_AtkState>();
        }
    }
    public override void Exit()
    {
        enemy.rb.velocity = Vector2.zero;
    }
}
