using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    // property
    public int comboCounter { get; private set; }
    private float comboWindow = .5f;
    private float lastTimeAttacked;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //AudioManager.instance.PlaySFX(2, null); // attack sound effect

        xInput = 0;  // we need this to fix bug on attack direction
        player.SetZeroVelocity();

        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;


        player.attackDetails.attackLevel = 1;

        player.Anim.SetInteger("AttackLevel", player.attackDetails.attackLevel);
    }


    public override void Exit()
    {
        base.Exit();
        //player.StartCoroutine("BusyFor", .15f);
        //comboCounter++;
        //lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            Debug.Log($"the trigger was called during attack {comboCounter}");
            stateMachine.ChangeState(player.AttackComboState);
        }

        //verify the animation is complete then transition if hang up
        if ((CheckAnimName() && stateInfo.normalizedTime > 1))
        {
            Debug.Log($"the attack timed out during attack {comboCounter}");
            stateMachine.ChangeState(player.AttackComboState);
        }
    }

    public bool CheckAnimName()
    {
        if (stateInfo.IsName("Primary Attack 1"))
        {
            return true;
        }
        else if (stateInfo.IsName("Primary Attack 2"))
        {
            return true;
        }
        else if (stateInfo.IsName("Primary Attack 3"))
        {
            return true;
        }
        else if (stateInfo.IsName("Primary Attack 4"))
        {
            return true;
        }
        return false;
    }
}