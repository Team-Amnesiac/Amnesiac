using UnityEngine;

public class RelicPickup : MonoBehaviour
{
    public Quest associatedQuest;
    private bool isPlayerNearby = false;
    private Animator playerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerAnimator = other.GetComponent<Animator>();
            Debug.Log($"Player is near the relic: {associatedQuest?.questName ?? "No associated quest"}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerAnimator = null;
            Debug.Log($"player left the relic: {associatedQuest?.questName ?? "No associated quest????"}");
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickupRelic();
        }
    }

    //Issues: Relic's associated quest and QuestManager's active quests element references are not matching properly 
    private void PickupRelic()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Gather");
        }

        if (associatedQuest != null)
        {
            Debug.Log($"[RelicPickup] Attempting to collect relic for quest: {associatedQuest.questName}");

            if (QuestManager.Instance.activeQuests.Contains(associatedQuest) && !associatedQuest.isCompleted)
            {
                Debug.Log($"[RelicPickup] Relic's collected for active quest: {associatedQuest.questName}");
                QuestManager.Instance.UpdateQuestProgress(associatedQuest);
            }
            else
            {
                Debug.Log($"[RelicPickup] Relic's collected but no active quest found: {associatedQuest.questName}");
            }
        }
        else
        {
            Debug.LogWarning("[RelicPickup] Associated quest is null. Relic's collection has been skipped.");
        }

        Destroy(gameObject); //could add it to inventory but unnecessary
    }
}
