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
            Set collectibleSetType = collectibleSet.getSetType();
            if (collectibleSetType == collectible.getSetType())  // Matching set found.
            {
                int collectibleCount = -1;
                int totalCount = -2;
                if (collectibleSetType == Set.Trophy)
                {
                    trophyCount++;
                    collectibleCount = trophyCount;
                    totalCount = (int)SetSize.Trophy;
                }

                if (collectibleCount == totalCount)  // Set completed.
                {

                    UIManager.Instance.newNotification($"{collectibleSetType.ToString()} set complete! See the keeper.");
                    activeSets.Remove(collectibleSet);
                    completedSets.Add(collectibleSet);
                }
                else
                {
                    UIManager.Instance.newNotification(
                        $"{collectibleSetType.ToString()} collected!: {collectibleCount} / {totalCount}");
                }

                return;
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
