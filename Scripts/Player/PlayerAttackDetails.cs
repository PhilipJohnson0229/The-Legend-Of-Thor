using System.Text;
using UnityEngine;


[CreateAssetMenu(fileName = "New Attack Data", menuName = "Data/Attack Details")]
public class PlayerAttackDetails : ScriptableObject
{
    [Header("Attack details")]
    public int attackLevel;
    public float counterAttackDuration = .2f;
    public float weaponArtCooldown = 2f;
    public float heavyAttackCooldown = 0.5f;
    public int primaryAttackComboCeiling;
    public int heavyAttackComboCeiling;
    public int primaryAirAttackComboCeiling;
    public int heavyAirAttackComboCeiling;
    public int comboCounter;

    [Header("Hammer")]
    public AttackDetail[] swordLightAirAttacks;
    public AttackDetail[] swordHeavyAirAttacks;
    public AttackDetail[] swordLightGroundAttacks;
    public AttackDetail[] swordHeavyGroundAttacks;
    public AttackDetail swordFallingLight;
    public AttackDetail swordFallingHeavy;
    public AttackDetail swordRunningLight;
    public AttackDetail swordRunningHeavy;
    public AttackDetail swordWeaponArtAir;
    public AttackDetail swordWeaponArtGrounded;

}