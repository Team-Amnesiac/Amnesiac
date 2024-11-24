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


    [SerializeField] private QuestSO relatedQuest;
    [SerializeField] private Set collectibleSet;


    public Set getSet()
    {
        return collectibleSet;
    }


    public QuestSO getRelatedQuest()
    {
        return relatedQuest;
    }
}
