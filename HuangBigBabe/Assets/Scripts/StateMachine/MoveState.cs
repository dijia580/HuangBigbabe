using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{

    private Player player;
    

    private string ISWALK = "IsWalk";
    public MoveState(StateMachine stateMachine,Player player) : base(stateMachine)
    {
        this.player = player;
    }
    public override void On_Enter()
    {

        GameInput.Instance.OnJumpWasClick += Instance_OnJumpWasClick
            ;
        player.animator.SetBool(ISWALK, true);
    }

    private void Instance_OnJumpWasClick(object sender, System.EventArgs e)
    {
        if(player.IsGround)
        stateMachine.SwitchState<JumpState>();
    }

    public override void Update()
    {
     if(player.movedir==Vector2.zero)
        {
            stateMachine.SwitchState<IdleState>();
        }
    }
    public override void FixedUpdate()
    {
        player.HandleMove();
    }
    public override void Exit()
    {
        GameInput.Instance.OnJumpWasClick -= Instance_OnJumpWasClick;
    }
}
