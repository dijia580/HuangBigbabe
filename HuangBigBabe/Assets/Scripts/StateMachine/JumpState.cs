using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class JumpState : State
{

    private Player player;
    public JumpState(StateMachine stateMachine, Player player) : base(stateMachine)
    {
        this.player = player;
    }

    public override void On_Enter()
    {
        player.rb.AddForce(player.JumpFprce * Vector2.up, ForceMode2D.Impulse);
    }
    public override void FixedUpdate()
    {

    }
    public override void Update()
    {
        if (player.GetIsGround() && player.rb.velocity.y <= 0)
        {
            // 根据是否有移动输入决定切换到 Idle 还是 Move
            if (player.movedir != Vector2.zero)
                stateMachine.SwitchState<MoveState>();
            else
                stateMachine.SwitchState<IdleState>();
        }
    }
}
