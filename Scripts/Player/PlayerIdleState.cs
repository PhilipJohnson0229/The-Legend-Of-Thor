using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public bool attack, combo;
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
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
        attack = player.Anim.GetBool("Attack");
        combo = player.Anim.GetBool("Combo");

        //Handle movement
        if (xInput != 0 && !attack && !combo)
        {
            stateMachine.ChangeState(player.MovementState);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
    }
}