using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New CollectibleSO", menuName = "Collectible/Create New CollectibleSO")]
public class CollectibleSO : ItemSO
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
