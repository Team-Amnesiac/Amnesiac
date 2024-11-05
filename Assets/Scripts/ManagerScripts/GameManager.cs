using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Title,     // Game is in the Title Scene.
        Play,      // Game is in the play state (in a level).
        Pause,     // Game is in the paused state.
        Battle,    // Game is in the Battle Scene.
        Shop,      // Game is in a shop menu.
        Dialogue,  // Game is playing dialogue (text conversation).
        Quest,     // Game is in dialogue quest menu (accept / reject).
    }


    // Singleton instance of the GameManager class.
    public static GameManager Instance;

    // The current state of the game.
    private GameState         gameState;
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
            gameState    = GameState.Title;
            currentScene = SceneLoader.Scene.Title;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /* PRIVATE FUNCTIONS */

    private void HandleGameStateChange(GameState state)
    {
        switch (state)
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
                // Load the Battle scene.
                SceneLoader.Instance.LoadScene(SceneLoader.Scene.Battle);

                break;

            default:
                // Handle invalid GameState.
                Debug.Log("ERROR: GameManager.HandleGameStateChange - invalid GameState");

                break;
        }
    }


    /* SET FUNCTIONS */

    public void SetGameState(GameState state)
    {
        HandleGameStateChange(state);

        gameState = state;
    }
}

