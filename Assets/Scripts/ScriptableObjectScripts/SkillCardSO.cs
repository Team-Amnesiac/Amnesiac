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
    
    private bool equipped = false;


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
        if (equipped)
        {
            PlayerManager.Instance.unequip(this);
        }
        else
        {
            PlayerManager.Instance.equipSkillCard(this);
        }
        equipped = !equipped;
    }


    public bool isEquipped()
    {
        return equipped;
    }
}
