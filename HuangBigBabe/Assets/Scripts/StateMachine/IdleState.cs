using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        GameInput.Instance.OnJumpWasClick += Instance_OnJumpWasClick;
        player.animator.SetBool(ISWALK, false);
        
        player.rb.velocity = Vector2.zero;
        
    }

    private void Instance_OnJumpWasClick(object sender, EventArgs e)
    {
        if (player.IsGround)
            stateMachine.SwitchState<JumpState>();
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
    public override void Exit()
    {
        GameInput.Instance.OnJumpWasClick -= Instance_OnJumpWasClick;
    }
}
