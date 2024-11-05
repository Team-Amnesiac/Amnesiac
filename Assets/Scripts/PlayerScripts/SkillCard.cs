using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCard : MonoBehaviour
{
    public enum SkillCardType
    {
        Fire,
        Water,
        Electric,
        Ground,
        None
    }

    public SkillCard(float damage, SkillCardType type)
    {
        this.damage = damage;
        this.type = type;
    }

    public float         damage = 35.0f;
    public SkillCardType type   = SkillCardType.Fire;


    public float GetDamage()
    {
        return damage;
    }


    public SkillCardType GetType()
    {
        return type;
    }
}
