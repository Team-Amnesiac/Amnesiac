using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public bool isMainQuest;
    public bool isCompleted;

    public QuestType questType;
    public int requiredAmount; // For kill quests (e.g- killing certain amount of certain enemies)
    public string targetItem;  // For collection quests (e.g- collectibles, main quest involving collecting relics)

    public enum QuestType
    {
        Collect,
        Kill
    }
}
