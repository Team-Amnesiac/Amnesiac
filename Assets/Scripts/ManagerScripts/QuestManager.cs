// This class manages all aspects of the game's quest system, including active and completed quests, quest progression, 
// handling relics and collectibles, and managing side quests with dynamic updates.

using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Singleton instance for global access.
    public static QuestManager Instance;

    // Lists of active and completed quests.
    [SerializeField] List<QuestSO> activeQuests;
    [SerializeField] List<QuestSO> completedQuests;

    // Main quest references.
    [Header("Quest Assets")]
    [SerializeField] private QuestSO firstQuest;       // First main quest.
    [SerializeField] private QuestSO secondQuest;      // Second main quest.
    [SerializeField] private QuestSO thirdQuest;       // Third main quest.
    [SerializeField] private QuestSO sideQuestTemplate; // Template for generating side quests.

    private int currentSideQuestStage = 1; // Tracks the current stage of the side quest.

    // Ensures only one instance of the QuestManager exists and persists across scenes.
    void Awake()
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

    // Initializes quests and starts the first side quest.
    void Start()
    {
        firstQuest.isCompleted = false;
        secondQuest.isCompleted = false;
        thirdQuest.isCompleted = false;
        sideQuestTemplate.isCompleted = false;
        sideQuestTemplate.requiredAmount = 3;
        StartOrUpdateSideQuest();
    }

    // Resets the quest system by clearing all data and resetting stages.
    public void reset()
    {
        activeQuests.Clear();
        completedQuests.Clear();
        currentSideQuestStage = 1;
    }

    // Handles interactions with the Keeper to start or progress main quests.
    public void talkToKeeper()
    {
        if (playerReadyForFirstQuest())
        {
            addQuest(firstQuest);
        }
        else if (playerReadyForSecondQuest())
        {
            addQuest(secondQuest);
        }
        else if (playerReadyForThirdQuest())
        {
            addQuest(thirdQuest);
        }
    }

    // Adds a relic to its associated quest and updates its progress.
    public void addRelic(RelicSO relic)
    {
        QuestSO relicQuest = relic.getRelatedQuest();
        if (activeQuests.Contains(relicQuest))
        {
            updateQuestProgress(relicQuest);
        }
    }

    // Adds a collectible to its associated quest and updates its progress.
    public void addCollectible(CollectibleSO collectible)
    {
        QuestSO collectibleQuest = collectible.getRelatedQuest();
        if (activeQuests.Contains(collectibleQuest) && !collectibleQuest.isCompleted)
        {
            updateQuestProgress(collectibleQuest);
        }
    }

    // Adds a new quest to the active quests list and notifies the player.
    private void addQuest(QuestSO newQuestSo)
    {
        activeQuests.Add(newQuestSo);
        UIManager.Instance.newNotification($"New Quest(s) in quest log!");
    }

    // Updates the progress of an active quest and checks for completion.
    private void updateQuestProgress(QuestSO questSo)
    {
        if (activeQuests.Contains(questSo) && !questSo.isCompleted)
        {
            Debug.Log($"[QuestManager] Updating progress for quest: {questSo.questName}");
            questSo.requiredAmount--;
            Debug.Log($"[QuestManager] Remaining items for quest: {questSo.requiredAmount}");

            if (questSo.requiredAmount <= 0) // Quest is complete.
            {
                Debug.Log($"[QuestManager] Quest completed: {questSo.questName}");
                questSo.isCompleted = true;
                completedQuests.Add(questSo);
                activeQuests.Remove(questSo);
                UIManager.Instance.newNotification("Quest complete, Return to Keeper!");
            }
        }
        else
        {
            Debug.Log($"[QuestManager] Quest progress update skipped. Quest not active or already completed: {questSo.questName}");
        }
    }

    // Starts or updates the current side quest.
    public void StartOrUpdateSideQuest()
    {
        if (!activeQuests.Contains(sideQuestTemplate)) // If the side quest isn't active, start it.
        {
            sideQuestTemplate.questName = $"Defeat {sideQuestTemplate.requiredAmount} Enemies";
            activeQuests.Add(sideQuestTemplate);
            Debug.Log($"New Side Quest Started: {sideQuestTemplate.questName}");
        }
    }

    // Updates the progress of the side quest and prepares the next stage when completed.
    public void UpdateSideQuestProgress()
    {
        if (activeQuests.Contains(sideQuestTemplate) && !sideQuestTemplate.isCompleted)
        {
            sideQuestTemplate.requiredAmount--;
            Debug.Log($"[QuestManager] Side Quest Progress: {sideQuestTemplate.requiredAmount} enemies remaining");

            if (sideQuestTemplate.requiredAmount <= 0) // Side quest is complete.
            {
                QuestSO completedQuestSnapshot = ScriptableObject.CreateInstance<QuestSO>();
                completedQuestSnapshot.questName = sideQuestTemplate.questName;
                completedQuestSnapshot.description = sideQuestTemplate.description;
                completedQuestSnapshot.requiredAmount = sideQuestTemplate.requiredAmount;
                completedQuestSnapshot.isCompleted = true;
                completedQuestSnapshot.questType = sideQuestTemplate.questType;

                completedQuests.Add(completedQuestSnapshot);
                Debug.Log($"[QuestManager] Side Quest Completed: {completedQuestSnapshot.questName}");

                int experienceReward = 20 + (currentSideQuestStage * 25);
                PlayerManager.Instance.increaseExperience(experienceReward);
                Debug.Log($"Player rewarded with {experienceReward} experience points.");

                currentSideQuestStage++;
                sideQuestTemplate.isCompleted = false;
                int baseEnemiesCount = 3;
                sideQuestTemplate.requiredAmount = baseEnemiesCount + (currentSideQuestStage * 2);
                sideQuestTemplate.questName = $"Defeat {sideQuestTemplate.requiredAmount} Enemies";
                StartOrUpdateSideQuest();
            }
        }
        else
        {
            Debug.LogWarning($"[QuestManager] Side quest is either not active or already completed.");
        }
    }

    /* GET FUNCTIONS */

    public QuestSO FirstQuest => firstQuest;
    public QuestSO SecondQuest => secondQuest;
    public QuestSO ThirdQuest => thirdQuest;

    public List<QuestSO> getActiveQuests()
    {
        return activeQuests;
    }

    public List<QuestSO> getCompletedQuests()
    {
        return completedQuests;
    }

    // Saves the current quest state into a data object.
    public QuestData SaveState()
    {
        return new QuestData
        {
            activeQuests = activeQuests.ConvertAll(q => q.questName),
            completedQuests = completedQuests.ConvertAll(q => q.questName)
        };
    }

    // Loads the quest state from saved data.
    public void LoadState(QuestData data)
    {
        activeQuests = data.activeQuests.ConvertAll(name => FindQuestByName(name));
        completedQuests = data.completedQuests.ConvertAll(name => FindQuestByName(name));
    }

    // Finds a quest by its name in the Resources folder.
    private QuestSO FindQuestByName(string name)
    {
        return Resources.Load<QuestSO>($"Quests/{name}");
    }

    // Checks if the player is ready for the first quest.
    private bool playerReadyForFirstQuest()
    {
        return !activeQuests.Contains(firstQuest) &&
               !completedQuests.Contains(firstQuest);
    }

    // Checks if the player is ready for the second quest.
    private bool playerReadyForSecondQuest()
    {
        return completedQuests.Contains(firstQuest) &&
               !activeQuests.Contains(secondQuest) &&
               !completedQuests.Contains(secondQuest);
    }

    // Checks if the player is ready for the third quest.
    private bool playerReadyForThirdQuest()
    {
        return completedQuests.Contains(secondQuest) &&
               !activeQuests.Contains(thirdQuest) &&
               !completedQuests.Contains(thirdQuest);
    }
}
