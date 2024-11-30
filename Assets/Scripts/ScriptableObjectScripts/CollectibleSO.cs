using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New CollectibleSO", menuName = "Collectible/Create New CollectibleSO")]
public class CollectibleSO : ItemSO
{
    [SerializeField] private QuestSO relatedQuest;
    [SerializeField] private CollectibleManager.Set collectibleSet;


    public CollectibleManager.Set getSetType()
    {
        return collectibleSet;
    }


    public QuestSO getRelatedQuest()
    {
        return relatedQuest;
    }
}
