using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Data/Attack Detail")]
public class AttackDetail : ScriptableObject
{
    [Header("Attack details")]
    public Vector3 attackDirection;
    public float attackSpeed;
    public float attackTime;
}
