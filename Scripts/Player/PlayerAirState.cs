using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (rb.velocity.y < 0)
        {
            player.SetVelocity(rb.velocity.x, -3);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
        {
            Debug.Log("Found a wall");
            stateMachine.ChangeState(player.WallSlideState);
        }

        if (player.IsGroundDetected())
        {
            Debug.Log("Found the ground");
            stateMachine.ChangeState(player.IdleState);
        }

        if (Input.GetKeyUp(KeyCode.H) && !player.IsWallDetected())
        {
            stateMachine.ChangeState(player.DashState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }
    }
}