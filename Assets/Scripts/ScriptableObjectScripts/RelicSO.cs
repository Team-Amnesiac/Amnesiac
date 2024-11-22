using UnityEngine;


[CreateAssetMenu(fileName = "New RelicSO", menuName = "RelicSO/Create New RelicSO")]
public class RelicSO : ItemSO
{
    [SerializeField] private QuestSO relatedQuest;


    public QuestSO getRelatedQuest()
    {
        return relatedQuest;
    }


    public void SetRelatedQuest(QuestSO relatedQuest)
    {
        this.relatedQuest = relatedQuest;
    }
}
