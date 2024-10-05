using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector3(rb.velocity.x, player.jumpForce, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //This is how we transition from jumping to falling
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.AirState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }
    }
}