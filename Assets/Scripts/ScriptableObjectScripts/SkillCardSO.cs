using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillCardSO", menuName = "SkillCardSO/Create New SkillCardSO")]
public class SkillCardSO : ItemSO
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
    [SerializeField] private int   staminaCost;
    [SerializeField] private AttackType attackType;
    
    private bool equipped = false;


    public float getDamage()
    {
        return damage;
    }


    public int getStaminaCost()
    {
        return staminaCost;
    }


    public AttackType getAttackType()
    {
        return attackType;
    }


    public bool isEquipped()
    {
        return equipped;
    }


    public void setEquipped(bool equipped)
    {
        this.equipped = equipped;
    }
}
