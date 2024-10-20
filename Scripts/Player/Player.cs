using TMPro;
using UnityEngine;

public class Player : Entity
{
    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMovementState MovementState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackComboState AttackComboState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAimMjolnirState AimMjolnirState { get; private set; }
    public PlayerCatchMjolnirState CatchMjolnirState { get; private set; }
    #endregion

    #region Components
    public SkillManager skill { get; private set; }
    public GameObject hammer { get; private set; }
    #endregion

    #region Settings
    public float moveSpeed = 7f;
    public float jumpForce = 10f;
    public float dashSpeed = 8f;
    public float dashDuration = 1f;

    [Header("Attack details")]
    public PlayerAttackDetails attackDetails;
    public TMP_Text debug;
    #endregion

    protected override void Start() 
    {
        base.Start();

        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MovementState = new PlayerMovementState(this, StateMachine, "Move");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        AttackComboState = new PlayerAttackComboState(this, StateMachine, "Combo");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        AimMjolnirState = new PlayerAimMjolnirState(this, StateMachine, "Aim");
        CatchMjolnirState = new PlayerCatchMjolnirState();
        StateMachine.Initialize(IdleState);
        skill = SkillManager.instance;
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        debug.text = $"State: {StateMachine.currentState}";
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.FixedUpdate();
    }


    public void AssignNewHammer(GameObject _newHammer)
    {
        hammer = _newHammer;
    }

    public void AnimationTrigger() => StateMachine.currentState.AnimationFinishTrigger();

    public void ActionMovement(float speed) => StateMachine.currentState.ActionMovement(speed);

    public void CatchTheHammer()
    {
        Debug.Log("Caught the hammer");
    }
}
