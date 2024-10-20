using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimMjolnirState : PlayerState
{
    public PlayerAimMjolnirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.mjolnir_skill.DotsActive(true);

        Debug.Log("Trying to aim sword");
    }

    public override void Exit()
    {
        base.Exit();

        //player.StartCoroutine("BusyFor", .2f);
        SkillManager.instance.mjolnir_skill.DotsActive(false);

        Debug.Log("Throwing mjolnir now!");
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        if (Input.GetMouseButtonUp(0))
        {
            stateMachine.ChangeState(player.IdleState);
        }

        //capture the direction we throw the hammer
        //and turn the player towards that direction to make it look like hes aiming
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -Camera.main.transform.position.z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenPos);

        Debug.Log(mousePosition);


        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
            player.Flip();
    }
}
