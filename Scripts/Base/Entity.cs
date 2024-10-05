using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Entity : MonoBehaviour
{
    //Property
    [field: SerializeField] public Rigidbody RB { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }

    [Header("Collision Checks")]
    //public Transform attackCheck;
    //public float attackCheckRadius;
    public bool isGrounded;
    public bool isWallDetected;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform wallCheck;

    [Header("Rotation")]
    [SerializeField] protected bool facingLeft = false;
    [SerializeField] protected Transform playerModel;
    //[SerializeField] protected Vector3 direction; //This value is meant for AI entities
    [SerializeField] protected Vector3 facing; //This value is used by the player
    public int facingDir = 1; //This value is meant for AI entities

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        //gravity = GetComponent<CustomGravity>();
    }


    void Update()
    {
        
    }

    protected virtual void HandleRotation(float x)
    {

        //Track the current rotation so we can update it
        Vector3 facing = playerModel.localEulerAngles;

        //Rotate the player model according to the input
        if (x != 0)
        {
            facing.y = x > 0 ? 90 : -90;
            playerModel.localEulerAngles = facing;
        }

        if (facing.y == 90)
        {
            facingLeft = false;
            facingDir = 1;
        }
        else if (facing.y == -90)
        {
            facingLeft = true;
            facingDir = -1;
        }
    }

    public void FlipController(float x)
    {
        HandleRotation(x);
    }

    //this will be for AI only
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingLeft = !facingLeft;
        HandleRotation(facingDir);
    }

    public virtual void SetupDefailtFacingDir(int _direction)
    {
        facingDir = _direction;

        if (facingDir == -1)
            facingLeft = true;
    }

    public bool IsGroundDetected() => Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics.Raycast(wallCheck.position, wallCheck.forward, wallCheckDistance, whatIsGround);

    #region Velocity
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        RB.velocity = new Vector3(xVelocity, yVelocity, 0);
        FlipController(xVelocity);
    }
    public void SetZeroVelocity()
    {
        RB.velocity = new Vector3(0f, 0f, 0f);
    }

    public void SetAttackVelocity(float xVelocity, float yVelocity, float zVelocity, float speed)
    {
        Debug.Log($"xVelocity: {xVelocity}, zVelocity: {zVelocity}");
        /*if (isKnocked)
            return;*/
        // Get the dash direction based on player's forward vector
        Vector3 offset = new Vector3(xVelocity, yVelocity, zVelocity);

        // Transform the local direction to world space
        Vector3 worldDirection = transform.TransformDirection(offset);

        // Set velocity based on dash direction
        // Apply velocity using AddForce
        RB.AddForce(worldDirection.normalized * speed, ForceMode.VelocityChange);
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        Vector3 Pos = groundCheck.position + (-transform.up * groundCheckDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, Pos + (-transform.up * groundCheckDistance));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + wallCheck.forward * wallCheckDistance);

       /* Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);*/
    }

    public Transform GetPlayerModel()
    {
        return playerModel;
    }

    public virtual void Die()
    {

    }
}
