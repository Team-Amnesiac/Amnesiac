using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillCard", menuName = "SkillCard/Create New SkillCard")]
public class SkillCard : Item
{
    public enum AttackType
    {
        Earth,
        Fire,
        Water,
        Wind,
        None,
    }


    [SerializeField] private float damage;
    [SerializeField] private AttackType attackType;


    public SkillCard(float damage, AttackType attackType)
    {
        this.damage     = damage;
        this.attackType = attackType;
    }


    public float GetDamage()
    {
        return damage;
    }


    public AttackType GetAttackType()
    {
        return attackType;
    }
}
