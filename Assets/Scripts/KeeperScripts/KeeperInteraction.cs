using UnityEngine;

public class KeeperInteraction : MonoBehaviour
{
    private bool playerInRange = false;


    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.showUI(UIManager.UI.Keeper);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range of The Keeper.");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left the range of The Keeper.");
        }
    }


     public void talkTo()
    {
        QuestManager.Instance.talkToKeeper();
        Debug.Log("Player interacted with Keeper and received a quest.");
    }
}
