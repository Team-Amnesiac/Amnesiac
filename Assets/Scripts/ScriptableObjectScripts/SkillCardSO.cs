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
    [SerializeField] private AttackType attackType;
    
    private bool isEquiped = false;


    public SkillCardSO(float damage, AttackType attackType)
    {
        this.damage     = damage;
        this.attackType = attackType;
    }


    public float getDamage()
    {
        return damage;
    }


    public AttackType getAttackType()
    {
        return attackType;
    }


    public void toggleEquip()
    {
        if (isEquiped)
        {
            PlayerManager.Instance.unequip(this);
        }
        else
        {
            PlayerManager.Instance.equipSkillCard(this);
        }
        isEquiped = !isEquiped;
    }
}
