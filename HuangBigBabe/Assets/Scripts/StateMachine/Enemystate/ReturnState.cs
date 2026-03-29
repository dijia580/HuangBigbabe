using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : State
{
    Enemy enemy;
    private float ReturnSpeed=2.5f;
    private const float ARRIVE_THRESHOLD = 0.1f;
    public ReturnState(StateMachine stateMachine,Enemy enemy) : base(stateMachine)
    {
        this.enemy = enemy;
    }
    public override void On_Enter()
    {
       
    }
    public override void Update()
    {

        CheckState();
        
    }
    public override void FixedUpdate()
    {
        Vector2 direction = (enemy.IniPosition - (Vector2)enemy.transform.position).normalized;
        enemy.rb.velocity=new Vector2( direction.x * ReturnSpeed,0);
        

    }
    private void CheckState()
    {
        if (enemy.PlayerInRange)
        {
            stateMachine.SwitchState<ChaseState>();
            return;
        }
        if (Vector2.Distance(enemy.transform.position, enemy.IniPosition) < ARRIVE_THRESHOLD)
        {
            stateMachine.SwitchState<enemy_Patrol>();
        }
    }
    public override void Exit()
    {
        enemy.rb.velocity = Vector2.zero;
    }
}
