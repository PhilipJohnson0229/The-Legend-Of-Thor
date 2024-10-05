using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() == false)
            stateMachine.ChangeState(player.AirState);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            stateMachine.ChangeState(player.WallJumpState);
            //this point of this return statement is to ignore the logic below to allow the wall jump to happen
            return;
        }

        if (yInput < 0)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        else
            rb.velocity = new Vector3(0, rb.velocity.y * .75f, 0);

        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.IdleState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
