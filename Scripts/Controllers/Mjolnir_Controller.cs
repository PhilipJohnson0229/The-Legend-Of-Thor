using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjolnir_Controller : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider cd;
    [SerializeField]
    private Player player;
    public CustomGravity cg { get; private set; }

    [SerializeField]
    private Transform modelDirection;
    private bool canRotate = true;
    private bool isReturning;
    private float maxTravelDistance;
    private float freezeTimeDuration;
    private float returnSpeed = 12;
    private float hitTimer;
    private float hitCooldown;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<SphereCollider>();
        cg = GetComponent<CustomGravity>();
    }

   
    void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;


        if (isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, player.transform.position) < 1)
            {
                player.CatchTheHammer();
            }

        }
    }

    private void FixedUpdate()
    {
        cg.Tick();
    }

    public void SetupHammer(Vector3 _dir, float _gravityScale, Player _player, float _freezeTimeDuration, float _returnSpeed, float _maxTravelDistance)
    {
        player = _player;
        freezeTimeDuration = _freezeTimeDuration;
        maxTravelDistance = _maxTravelDistance;
        returnSpeed = _returnSpeed;

        rb.velocity = _dir;
        
        Debug.Log($"the dirction were trying to throw the hammer is {_dir}");

        cg.gravityScale = _gravityScale;


        Invoke("DestroyMe", 5);

        if (player.facingDir == -1)
        {
            modelDirection.rotation = new Quaternion(0, 180f, 0, 0);
        }
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isReturning)
            return;

       /* if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            SwordSkillDamage(enemy);
        }*/

        //SetupTargetsForBounce(collision);

        StuckInto(collision);
    }

    private void StuckInto(Collider collision)
    {

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //GetComponentInChildren<ParticleSystem>().Play();

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }


    public void ReturnHammer()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;


        //sword.skill.setcooldown;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
