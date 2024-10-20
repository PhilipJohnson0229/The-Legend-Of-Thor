using System;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class Mjolnir_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject hammerPrefab;
    [SerializeField] private Vector3 launchForce;
    [SerializeField] private float hammerGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float hitCooldown = .35f;
    [SerializeField] private float maxTravelDistance = 7;
    

    private Vector3 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void CheckUnlock()
    {
        base.CheckUnlock();
    }

    void Start()
    {
        player = PlayerManager.instance.player;
        GenereateDots();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            finalDir = new Vector3(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y, 0);


        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }

    public void CreateHammer()
    {
        Debug.Log("Creating a hammer to throw");
        GameObject newHammer = Instantiate(hammerPrefab, player.transform.localPosition, transform.rotation, null);

        Mjolnir_Controller newHammerScript = newHammer.GetComponent<Mjolnir_Controller>();

        /* if (swordType == SwordType.Bounce)
             newHammerScript.SetupBounce(true, bounceAmount, bounceSpeed, maxTravelDistance * 2.2f);
         else if (swordType == SwordType.Pierce)
             newHammerScript.SetupPierce(pierceAmount, maxTravelDistance * 3);
         else if (swordType == SwordType.Spin)
             newHammerScript.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);*/


        newHammerScript.SetupHammer(finalDir, hammerGravity, player, freezeTimeDuration, returnSpeed, maxTravelDistance);
        player.AssignNewHammer(newHammer);
        DotsActive(false);
    }

    #region Aim region
    public Vector3 AimDirection()
    {
        Vector3 playerPosition = player.transform.position;

        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -Camera.main.transform.position.z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 direction = new Vector3(mousePosition.x, mousePosition.y, 0) - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenereateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector3 DotsPosition(float t)
    {
        Vector3 position = player.transform.position + new Vector3(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics.gravity * hammerGravity) * (t * t);
        return position;
    }

    #endregion
}
