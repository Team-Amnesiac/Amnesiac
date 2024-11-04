using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerBattleScene()
    {
        Debug.Log("Transitioning to battle scene...");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

