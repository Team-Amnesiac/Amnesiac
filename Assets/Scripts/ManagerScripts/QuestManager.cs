using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] List<QuestSO> activeQuests;
    [SerializeField] List<QuestSO> completedQuests;

    [Header("Quest Assets")]
    [SerializeField] private QuestSO firstQuest;
    [SerializeField] private QuestSO secondQuest;
    [SerializeField] private QuestSO thirdQuest;
    [SerializeField] private QuestSO sideQuestTemplate;
    private int currentSideQuestStage = 1;


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

    void Start()
    {
        firstQuest.isCompleted = false;
        secondQuest.isCompleted = false;
        thirdQuest.isCompleted = false;
        sideQuestTemplate.isCompleted = false;
        sideQuestTemplate.requiredAmount = 3;
        StartOrUpdateSideQuest();
    }


    public void reset()
    {
        activeQuests.Clear();
        completedQuests.Clear();
        currentSideQuestStage = 1;
    }


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


    public void addRelic(RelicSO relic)
    {
        QuestSO relicQuest = relic.getRelatedQuest();
        if (activeQuests.Contains(relicQuest))
        {
            updateQuestProgress(relicQuest);
        }
    }


    public void addCollectible(CollectibleSO collectible)
    {
        QuestSO collectibleQuest = collectible.getRelatedQuest();
        if (activeQuests.Contains(collectibleQuest) && !collectibleQuest.isCompleted)
        {
            updateQuestProgress(collectibleQuest);
        }
    }


    private void addQuest(QuestSO newQuestSo)
    {
        activeQuests.Add(newQuestSo);
        UIManager.Instance.newNotification($"New Quest(s) in quest log!");
    }


    private void updateQuestProgress(QuestSO questSo)
    {
        if (activeQuests.Contains(questSo) && !questSo.isCompleted)
        {
            Debug.Log($"[QuestManager] Updating progress for quest: {questSo.questName}");
            questSo.requiredAmount--;
            Debug.Log($"[QuestManager] Remaining items for quest: {questSo.requiredAmount}");

            if (questSo.requiredAmount <= 0)
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

    public void StartOrUpdateSideQuest()
    {
        if (!activeQuests.Contains(sideQuestTemplate))
        {
            // Start the side quest if not already active
            sideQuestTemplate.questName = $"Defeat {sideQuestTemplate.requiredAmount} Enemies";
            activeQuests.Add(sideQuestTemplate);
            Debug.Log($"New Side Quest Started: {sideQuestTemplate.questName}");
        }
    }

    public void UpdateSideQuestProgress()
    {
        if (activeQuests.Contains(sideQuestTemplate) && !sideQuestTemplate.isCompleted)
        {
            sideQuestTemplate.requiredAmount--;
            Debug.Log($"[QuestManager] Side Quest Progress: {sideQuestTemplate.requiredAmount} enemies remaining");
            if (sideQuestTemplate.requiredAmount <= 0)
            {
                // Create a snapshot of the completed quest for UI purposes
                QuestSO completedQuestSnapshot = ScriptableObject.CreateInstance<QuestSO>();
                completedQuestSnapshot.questName = sideQuestTemplate.questName;
                completedQuestSnapshot.description = sideQuestTemplate.description;
                completedQuestSnapshot.requiredAmount = sideQuestTemplate.requiredAmount;
                completedQuestSnapshot.isCompleted = true;
                completedQuestSnapshot.questType = sideQuestTemplate.questType;

                // Add the completed quest snapshot to the completedQuests list
                completedQuests.Add(completedQuestSnapshot);
                Debug.Log($"[QuestManager] Side Quest Completed: {completedQuestSnapshot.questName}");

                // Reward the player
                int experienceReward = 20 + (currentSideQuestStage * 25);
                PlayerManager.Instance.increaseExperience(experienceReward);
                Debug.Log($"Player rewarded with {experienceReward} experience points.");

                // Increment the stage and prepare the next side quest
                currentSideQuestStage++;
                sideQuestTemplate.isCompleted = false;
                int baseEnemiesCount = 3; // Starting point for enemies in the first side quest
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

    public QuestData SaveState()
    {
        return new QuestData
        {
            activeQuests = activeQuests.ConvertAll(q => q.questName),
            completedQuests = completedQuests.ConvertAll(q => q.questName)
        };
    }

    public void LoadState(QuestData data)
    {
        activeQuests = data.activeQuests.ConvertAll(name => FindQuestByName(name));
        completedQuests = data.completedQuests.ConvertAll(name => FindQuestByName(name));
    }

    private QuestSO FindQuestByName(string name)
    {
        return Resources.Load<QuestSO>($"Quests/{name}");
    }

    private bool playerReadyForFirstQuest()
    {
        return !activeQuests.Contains(firstQuest) &&
               !completedQuests.Contains(firstQuest);
    }

    private bool playerReadyForSecondQuest()
    {
        return completedQuests.Contains(firstQuest) &&
               !activeQuests.Contains(secondQuest) &&
               !completedQuests.Contains(secondQuest);
    }

    private bool playerReadyForThirdQuest()
    {
        return completedQuests.Contains(secondQuest) &&
               !activeQuests.Contains(thirdQuest) &&
               !completedQuests.Contains(thirdQuest);
    }
}
