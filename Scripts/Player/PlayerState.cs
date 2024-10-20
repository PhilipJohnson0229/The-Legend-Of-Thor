using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected float xInput;
    protected float yInput;

    protected Rigidbody rb;

    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;
    protected AnimatorStateInfo stateInfo;
    protected AnimatorClipInfo[] clipInfo;

    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _playerStateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //This is where we play the animation of the entered state
        player.Anim.SetBool(animBoolName, true);
        rb = player.RB;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void FixedUpdate()
    {
       
    }

    public virtual void Exit()
    {
        //When exit is called we set the condition to end the animation
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    public float GetStateTimer()
    {
        return stateTimer;
    }

    public float GetXInput()
    {
        return xInput;
    }

    public void SetXInput(float value)
    {
        xInput = value;
    }

    public virtual void ActionMovement(float time)
    {
        stateTimer = time;
    }

    public bool HasNoHammer()
    {

        if (!player.hammer)
        {
            return true;
        }

        player.hammer.GetComponent<Mjolnir_Controller>().ReturnHammer();

        return false;
    }
}