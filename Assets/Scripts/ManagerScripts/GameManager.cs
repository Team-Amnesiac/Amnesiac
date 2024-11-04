using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Title,
        Play,
        Pause,
        Battle
    }


    public static GameManager Instance;

    // The current state of the game.
    private GameState         gameState;
    // The currently loaded scene of the game.
    private SceneLoader.Scene currentScene;
    // The previously loaded scene of the game.
    private SceneLoader.Scene previousScene;


    void Awake()
    {
        if (Instance == null)
        {
            Instance     = this;
            gameState    = GameState.Title;
            currentScene = SceneLoader.Scene.Title;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void HandleGameStateChange(GameState state)
    {
        switch (gameState)
        {
            case GameState.Title:
                // Load the title scene.
                SceneLoader.Instance.LoadScene(SceneLoader.Scene.Title);

                break;

            case GameState.Play:
                if (gameState == GameState.Title)  // Play button pressed.
                {
                    // Load the Hub scene.
                    SceneLoader.Instance.LoadScene(SceneLoader.Scene.Hub);
                }
                else if (gameState == GameState.Battle)
                {
                    SceneLoader.Instance.LoadScene(previousScene);
                }

                break;

            case GameState.Pause:
                // Handle any game changes caused by pausing.
                break;

            case GameState.Battle:
                SceneLoader.Instance.LoadScene(SceneLoader.Scene.Battle);

                break;

            default:
                // Handle invalid GameState.
                Debug.Log("ERROR: GameManager.HandleGameStateChange - invalid GameState");

                break;
        }
    }


    public GameState GetGameState()
    {
        return gameState;
    }


    public void SetGameState(GameState state)
    {
        HandleGameStateChange(state);

        gameState = state;
    }
}

