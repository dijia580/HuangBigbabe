using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DashState : State
{

    private Player player;

    private float Dashspeed = 7f;
    private float DashDUration = .2f;
    private float Dashcooldown = .5f;

    private float DashTimeLeft;
    private Vector2 DashDir;

    private string ISDASH = "IsDash";
    public DashState(StateMachine stateMachine,Player player) : base(stateMachine)
    {
        this.player = player;
    }
    public override void On_Enter()
    {
        player.animator.SetBool(ISDASH, true);


        if (player.movedir.sqrMagnitude > 0.01f)
        {
            DashDir = player.movedir.normalized;
        }
        else
        {
            DashDir = new Vector2(player.FacingDirection, 0);
        }

        DashTimeLeft = DashDUration;

        player.DashcooldownTimer = Dashcooldown;
    }
    public override void Update()
    {
        DashTimeLeft -= Time.deltaTime;

        if(DashTimeLeft<=0)
        {
            if(player.IsGround)
            {
                if(player.movedir!=Vector2.zero)
                {
                    stateMachine.SwitchState<MoveState>();
                }else
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
    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector2(DashDir.x * Dashspeed, player.rb.velocity.y);
    }
    public override void Exit()
    {
        player.animator.SetBool(ISDASH,false);
    }
}
