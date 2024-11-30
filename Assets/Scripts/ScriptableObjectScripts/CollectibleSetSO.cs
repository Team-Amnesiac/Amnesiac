using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New CollectibleSetSO", menuName = "CollectibleSet/Create New CollectibleSetSO")]
public class CollectibleSetSO : ScriptableObject
{
    [SerializeField]private CollectibleManager.Set setType;
    [SerializeField] private CollectibleSO[]       collectiblsInSet;


    public CollectibleManager.Set getSetType()
    {
        return setType;
    }
}
