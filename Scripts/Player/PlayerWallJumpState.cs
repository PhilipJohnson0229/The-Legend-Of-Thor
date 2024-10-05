using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        SetXInput(0);
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.AirState);
        }

        if (player.IsGroundDetected())
        {
            player.SetZeroVelocity();
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
