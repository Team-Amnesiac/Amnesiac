using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private GameObject questEntryPrefab;
    [SerializeField] private GameObject activeQuestContent;
    [SerializeField] private GameObject completedQuestContent;
    [SerializeField] private Button closeQuestLogButton;


    void Start()
    {
        closeQuestLogButton.onClick.AddListener(closeQuestLogButtonOnClick);
        UIManager.Instance.setUI(UIManager.UI.QuestLog, this);
        UIManager.Instance.hideUI(UIManager.UI.QuestLog);
    }


    public void prepareQuestLogShow()
    {
        prepareActiveQuestLogShow();
        prepareCompletedQuestLogShow();
    }


    public void prepareQuestLogHide()
    {
        prepareActiveQuestLogHide();
        prepareCompletedQuestLogHide();
    }


    private void closeQuestLogButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.QuestLog);
    }


    private void prepareActiveQuestLogShow()
    {
        foreach (QuestSO activeQuest in QuestManager.Instance.getActiveQuests())
        {
            GameObject obj = Instantiate(questEntryPrefab, activeQuestContent.transform);
            QuestEntryController controller = obj.GetComponent<QuestEntryController>();

            controller.setQuest(activeQuest);
        }
    }


    private void prepareCompletedQuestLogShow()
    {
        foreach (QuestSO completedQuest in QuestManager.Instance.getCompletedQuests())
        {
            GameObject obj = Instantiate(questEntryPrefab, completedQuestContent.transform);
            QuestEntryController controller = obj.GetComponent<QuestEntryController>();

            controller.setQuest(completedQuest);
        }
    }


    private void prepareActiveQuestLogHide()
    {
        foreach (Transform child in activeQuestContent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void prepareCompletedQuestLogHide()
    {
        foreach (Transform child in completedQuestContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}