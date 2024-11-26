using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;


    public enum Set
    {
        Trophy,
    }

    public enum SetSize
    {
        Trophy = 3,
    }

    private int trophyCount = 0;


    [SerializeField] private List<CollectibleSetSO> activeSets;
    [SerializeField] private List<CollectibleSetSO> completedSets;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addCollectible(CollectibleSO collectible)
    {
        foreach (CollectibleSetSO collectibleSet in activeSets)
        {
            if (collectibleSet.getSetType() == collectible.getSetType())  // Matching set found.
            {
                if (collectibleSet.getSetType() == Set.Trophy)
                {
                    trophyCount++;
                    if (trophyCount == (int)SetSize.Trophy)  // Set completed.
                    {
                        activeSets.Remove(collectibleSet);
                        completedSets.Add(collectibleSet);
                    }
                }

                break;
            }
        }
    }


    /* GET FUNCTIONS */

    public List<CollectibleSetSO> getActiveSets()
    {
        return activeSets;
    }


    public List<CollectibleSetSO> getCompletedSets()
    {
        return completedSets;
    }


    public int getCollectedCount(Set setType)
    {
        if (setType == Set.Trophy)
        {
            return trophyCount;
        }

        return -1;
    }


    public int getSetSize(Set setType)
    {
        if (setType == Set.Trophy)
        {
            return (int)SetSize.Trophy;
        }

        return -1;
    }
}
