using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerGroundedState is known as a SuperState
public class PlayerMovementState : PlayerGroundedState
{
    public PlayerMovementState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
        player = _player;
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetFloat("Speed", 0.5f);
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetFloat("Speed", 0);
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

    

        if (xInput == 0/* || player.IsWallDetected()*/)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //Handle movement
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
    }
}