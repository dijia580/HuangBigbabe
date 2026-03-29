using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DashState : State
{

    private Player player;
    [Header("dash信息")]
    private float Dashspeed = 7f;
    private float DashDUration = .2f;
    private float Dashcooldown = .5f;
    private float DashTimeLeft;
    private Vector2 DashDir;

    [Header("跳跃信息")]
    private bool JumpRequested ;

    private string ISDASH = "IsDash";
    public DashState(StateMachine stateMachine,Player player) : base(stateMachine)
    {
        this.player = player;
    }
    public override void On_Enter()
    {
        JumpRequested = false;
        GameInput.Instance.OnJumpWasClick += Instance_OnJumpWasClick;
        player.animator.SetBool(ISDASH, true);


        if (player.movedir.sqrMagnitude > 0.01f)
        {
            DashDir = player.movedir.normalized;
        }
        else
        {
            DashDir = new Vector2(player.facingdir, 0);
        }

        DashTimeLeft = DashDUration;

        player.DashcooldownTimer = Dashcooldown;
    }

    private void Instance_OnJumpWasClick(object sender, System.EventArgs e)
    {
        JumpRequested = true;
    }

    public override void Update()
    {
        DashTimeLeft -= Time.deltaTime;
        
            CheckState();
        
        
    }
    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector2(DashDir.x * Dashspeed, player.rb.velocity.y);
    }
    private void CheckState()
    {
        if (JumpRequested)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.JumpFprce);
            stateMachine.SwitchState<JumpState>();
            return;
        }
        if (DashTimeLeft <= 0)
        {
            if (player.GetIsGround())
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
        else
        {

        }
    }
    public override void Exit()
    {
        player.animator.SetBool(ISDASH,false);
        GameInput.Instance.OnJumpWasClick -= Instance_OnJumpWasClick;
    }
}
