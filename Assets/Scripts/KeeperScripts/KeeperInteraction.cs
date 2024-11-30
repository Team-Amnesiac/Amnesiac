using System;
using UnityEngine;

public class KeeperInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    [SerializeField] private string[] dialogueArray;
    private int startDialogueIndex = 0;
    private int endDialogueIndex = 2;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.showUI(UIManager.UI.Keeper); // Show Keeper UI
            GameManager.Instance.setGameState(GameManager.GameState.Pause);
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
        UIManager.Instance.getDialogueUI().onNextDialogue += dialogueUI_onNextDialogue;
        dialogueUI_onNextDialogue(this, EventArgs.Empty);
        Debug.Log("Player interacted with Keeper and received a quest.");
    }


    private void dialogueUI_onNextDialogue(object sender, EventArgs e)
    {
        if (startDialogueIndex > endDialogueIndex)  // Dialogue over.
        {
            // Reset the starting dialogue index.
            startDialogueIndex = 0;
            // Unsubscribe from the dialogue event.
            UIManager.Instance.getDialogueUI().onNextDialogue -= dialogueUI_onNextDialogue;
            // Close the dialogue UI.
            UIManager.Instance.hideUI(UIManager.UI.Dialogue);
            GameManager.Instance.setGameState(GameManager.GameState.Play);
        }
        else                                        // Dialogue continuing.
        {
            // Display the next dialogue message.
            UIManager.Instance.newDialogue(dialogueArray[startDialogueIndex]);
            startDialogueIndex++;
        }
    }
}
