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
    
    [SerializeField] private bool equipped = false;


    void Start()
    {
        equipped = false;
    }


    public float getDamage()
    {
        return damage;
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
