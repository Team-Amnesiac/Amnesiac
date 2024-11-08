using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCard
{
    public enum SkillCardType
    {
        Fire,
        Water,
        Electric,
        Ground,
        None
    }


    private float damage;
    private SkillCardType type;


    public SkillCard(float damage, SkillCardType type)
    {
        this.damage = damage;
        this.type = type;
    }


    public float GetDamage()
    {
        return damage;
    }


    public SkillCardType GetType()
    {
        return type;
    }
}
