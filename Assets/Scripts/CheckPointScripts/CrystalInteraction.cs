using UnityEngine;

public class CrystalInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.showUI(UIManager.UI.Checkpoint);
            GameManager.Instance.setGameState(GameManager.GameState.Pause);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the world crystal. Press 'E' to interact.");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the world crystal.");
        }
    }
}
