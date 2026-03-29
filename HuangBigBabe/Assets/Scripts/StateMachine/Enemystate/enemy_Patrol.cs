using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class enemy_Patrol : State
{

    private float TransTimer;
    private float TransTimeMAX = 2.5f;
    private Vector2 movedir;
    private float PatrolSpeed = 2f;

    private  Enemy enemy;
    public enemy_Patrol(StateMachine stateMachine,Enemy enemy) : base(stateMachine)
    {
        this.enemy = enemy;
    }
    public override void On_Enter()
    {
        TransTimer = TransTimeMAX;
        float randomX = Random.Range(-1f, 1f);
        // ±‹√‚¡„œÚ¡ø
        if (Mathf.Approximately(randomX, 0)) randomX = 0.1f;
        movedir = new Vector2(randomX, 0).normalized;
    }
    public override void Update()
    {
        TransTimer -= Time.deltaTime;
        if(TransTimer<=0)
        {
            TransTimer = TransTimeMAX;
            movedir = new Vector2(-movedir.x, movedir.y);
        }

        if(enemy.PlayerInRange)
        {
            stateMachine.SwitchState<ChaseState>();
        }
    }
    public override void FixedUpdate()
    {
        enemy.rb.velocity = movedir * PatrolSpeed;
    }
    public override void Exit()
    {
        enemy.rb.velocity = Vector2.zero;
    }

}
