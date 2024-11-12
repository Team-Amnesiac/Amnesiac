using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Collectible", menuName = "Collectible/Create New Collectible")]
public class Collectible : Item
{
    public enum Set
    {
        Trophy,
    }

    public enum SetSize
    {
        Trophy = 3,
    }


    [SerializeField] private Set collectibleSet;


    public Set GetSet()
    {
        return collectibleSet;
    }
}
