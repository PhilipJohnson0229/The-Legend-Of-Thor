using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    //This is shorthand to grab the component
    [SerializeField]
    private Player player => GetComponentInParent<Player>();

    public MeleeWeaponTrail weaponTrail;
    public void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        /* //We will capture all colliders in one frame that are within the sphere
         Collider[] colliders = Physics.OverlapSphere(player.attackCheck.position, player.attackCheckRadius);

         foreach (var hit in colliders)
         {
             if ((hit.GetComponent<Enemy>() != null))
             {
                 EnemyStats _target = hit.GetComponent<EnemyStats>();

                 if (_target != null)
                 {
                     player.stats.DoDamage(_target);
                 }

                 ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                 if (weaponData != null)
                     weaponData.Effect(_target.transform);
             }
         }*/
    }

    public void EmitWeaponTrail()
    {
        weaponTrail.Emit = true;
    }

    public void MuteWeaponTrail()
    {
        weaponTrail.Emit = false;
    }

    #region Set Attack Movements
    public void AttackMovement()
    {

        int comboCounter = player.Anim.GetInteger("ComboCounter");
        Debug.Log($"ComboCounter read in anim trigger: {comboCounter}");
        //sets speed and direction
        player.SetAttackVelocity(
           player.attackDetails.swordLightGroundAttacks[comboCounter].attackDirection.x * player.facingDir,
           player.attackDetails.swordLightGroundAttacks[comboCounter].attackDirection.y,
           player.attackDetails.swordLightGroundAttacks[comboCounter].attackDirection.z,
           player.attackDetails.swordLightGroundAttacks[comboCounter].attackSpeed);

        //sets how long the movement lasts
        player.ActionMovement(player.attackDetails.swordLightGroundAttacks[comboCounter].attackSpeed);
    }

    private void ThrowHammer()
    {
        SkillManager.instance.mjolnir_skill.CreateHammer();
        //player.swordModel.SetActive(false);
    }
    #endregion
}