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

    public void reset()
    {
        activeSets.Clear();
        completedSets.Clear();
        trophyCount = 0;
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

   public void LoadState(CollectiblesData data)
    {
        trophyCount = data.trophyCount;

        activeSets = data.activeSets.ConvertAll(name => FindCollectibleSetByName(name));
        
        completedSets = data.completedSets.ConvertAll(name => FindCollectibleSetByName(name));
    }

    private CollectibleSetSO FindCollectibleSetByName(string name)
    {
        return Resources.Load<CollectibleSetSO>($"Collectibles/{name}");
    }

    public CollectiblesData SaveState()
    {
        return new CollectiblesData
        {
            trophyCount = trophyCount,
            activeSets = activeSets.ConvertAll(set => set.name),
            completedSets = completedSets.ConvertAll(set => set.name)
        };
    }
}
