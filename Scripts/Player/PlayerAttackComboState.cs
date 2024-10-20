using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttackComboState : PlayerGroundedState
{
    public int ComboCounter { get; private set; }
    private float comboWindow = .5f;
    private float lastTimeAttackedLight, lastTimeAttackedHeavy;
    public int HeavyComboCounter { get; private set; }

    private int attackLevelCopy, debugComboInt;
    public PlayerAttackComboState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //combo logic

        ComboCounter = player.Anim.GetInteger("ComboCounter");
        HeavyComboCounter = player.Anim.GetInteger("HeavyComboCounter");

        player.Anim.SetBool("Attack", false);
        player.Anim.SetBool("HeavyAttack", false);

        stateTimer = .8f;

        switch (player.attackDetails.attackLevel)
        {
            case 1:
                lastTimeAttackedLight = Time.time;
                break;
            case 2:
                lastTimeAttackedHeavy = Time.time;
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        switch (player.attackDetails.attackLevel)
        {
            case 1:
                ComboCounter++;
                LightComboCheck();
         
                break;
            case 2:
                HeavyComboCounter++;
                HeavyComboCheck();
                
                break;
        }

        player.Anim.SetInteger("ComboCounter", ComboCounter);
        player.Anim.SetInteger("HeavyComboCounter", HeavyComboCounter);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.K))
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //stateMachine.ChangeState(player.HeavyAttackState);
        }

        if (Input.GetKeyUp(KeyCode.P) && player.attackDetails.weaponArtCooldown <= 0)
        {
            //stateMachine.ChangeState(player.WeaponArtState);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    private void HeavyComboCheck()
    {
        if (HeavyComboCounter > player.attackDetails.heavyAttackComboCeiling ||
            Time.time >= lastTimeAttackedHeavy + comboWindow)
        {
            HeavyComboCounter = 0;
        }
        ComboCounter = 0;
    }

    private void LightComboCheck()
    {
        if (ComboCounter > player.attackDetails.primaryAttackComboCeiling ||
            Time.time >= lastTimeAttackedLight + comboWindow)
        {
            ComboCounter = 0;
        }
        HeavyComboCounter = 0;
    }
}