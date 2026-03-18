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
        player.animator.SetBool(ISWALK, true);
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
}
