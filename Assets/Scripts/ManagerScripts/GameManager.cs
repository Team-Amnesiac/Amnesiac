using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Title,     // Game is in the Title Scene.
        Play,      // Game is in the play state (in a level).
        Pause,     // Game is in the paused state.
        Battle,    // Game is in the Battle Scene.
    }


    // Singleton instance of the GameManager class.
    public static GameManager Instance;

    // The current state of the game.
    [SerializeField] private GameState gameState;
    // The currently loaded scene of the game.
    private SceneLoader.Scene currentScene;
    // The previously loaded scene of the game.
    private SceneLoader.Scene previousScene;


    /* UNITY FUNCTIONS */

    void Awake()
    {
        if (Instance == null)
        {
            Instance     = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /* PRIVATE FUNCTIONS */

    private void handleGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Title:
                Time.timeScale = 1;
                previousScene = currentScene;
                currentScene  = SceneLoader.Scene.Title;
                // Load the title scene.
                SceneLoader.Instance.loadScene(SceneLoader.Scene.Title);

                break;

            case GameState.Play:
                Time.timeScale = 1;
                if (gameState == GameState.Title)  // Play button pressed.
                {
                    currentScene = SceneLoader.Scene.Hub;
                    // Load the Hub scene.
                    SceneLoader.Instance.loadScene(SceneLoader.Scene.Hub);
                }
                else if (gameState == GameState.Battle)
                {
                    SceneLoader.Instance.loadScene(previousScene);
                    // Swap values of previous scene and current scene.
                    (previousScene, currentScene) = (currentScene, previousScene);
                }

                break;

            case GameState.Pause:
                // Handle any game changes caused by pausing.
                Time.timeScale = 0;

                break;

            case GameState.Battle:
                Time.timeScale = 1;
                previousScene = currentScene;
                // Load the Battle scene.
                SceneLoader.Instance.loadScene(SceneLoader.Scene.Battle);

                break;

            default:
                // Handle invalid GameState.
                Debug.Log("ERROR: GameManager.HandleGameStateChange - invalid GameState");

                break;
        }
    }


    /* GET FUNCTIONS */

    public GameState getGameState()
    {
        return gameState;
    }


    /* SET FUNCTIONS */

    public void setGameState(GameState state)
    {
        handleGameStateChange(state);

        gameState = state;
    }
}

