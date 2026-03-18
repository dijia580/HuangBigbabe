using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    private string ISWALK = "IsWalk";
   
      private Player player;
    public IdleState(StateMachine stateMachine,Player player) : base(stateMachine)
    {
        this.player = player;
    }
    public override void On_Enter()
    {
        player.animator.SetBool(ISWALK, false);
        
        player.rb.velocity = Vector2.zero;
        
    }
    public override void Update()
    {
        if (player.movedir != Vector2.zero)
        {
            stateMachine.SwitchState<MoveState>();
        }
    }
    public override void FixedUpdate()
    {
        
    }
}
