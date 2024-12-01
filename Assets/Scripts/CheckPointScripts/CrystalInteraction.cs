using UnityEngine;

public class CrystalInteraction : MonoBehaviour
{
    public GameObject checkpointUI;
    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            checkpointUI.SetActive(true);
            Time.timeScale = 0f;
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
            checkpointUI.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Player left the world crystal.");
        }
    }
}
