using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ATKState : State
{

    private Player player;

    private float AtkCoolDown=.05f;

    private float AtkDuration = .2f;
    private float AtkTimerLeft;

    private string ATK="ATK";
    public ATKState(StateMachine stateMachine,Player player) : base(stateMachine)
    {
        this.player = player;
    }
    public override void On_Enter()
    {
        player.animator.SetTrigger(ATK);

        player.AtkcooldownTimer = AtkCoolDown;
        AtkTimerLeft = AtkDuration;
        
    }

    public override void Update()
    {
        AtkTimerLeft -= Time.deltaTime;

        if(AtkTimerLeft<=0)
        {
            if(player.IsGround)
            {
                if (player.movedir != Vector2.zero)
                {
                    stateMachine.SwitchState<MoveState>();
                }
                else
                {
                    stateMachine.SwitchState<IdleState>();
                }

            }
            else
            {
                stateMachine.SwitchState<JumpState>();
            }
        }
    }
    public override void Exit()
    {
        
    }
}
