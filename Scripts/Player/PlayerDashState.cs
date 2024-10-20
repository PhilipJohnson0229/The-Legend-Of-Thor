using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        //player.stats.MakeInvincible(true);

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
        //player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("Dash State" + stateTimer);
        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }

        player.SetVelocity(player.facingDir * player.dashSpeed, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        //the after image controller you we just made
        //player.aiController.SpawnAfterimage();
    }
}
