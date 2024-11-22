using UnityEngine;

[CreateAssetMenu(fileName = "New QuestSO", menuName = "QuestSO/QuestSO")]
public class QuestSO : ScriptableObject
{
    [Header("Basic Info")]
    public string questName;
    public string description;
    public bool isCompleted;


    [Header("Quest Type")]
    public QuestType questType;


    [Header("Quest Requirements")]
    public string targetItem;  // For collection quests (e.g., collecting relics)
    public int requiredAmount;


    public enum QuestType
    {
        Main,
        Side
    }
}
