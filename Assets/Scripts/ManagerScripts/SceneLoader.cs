// This class manages scene loading and ensures a singleton instance for global access.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Singleton instance of the SceneLoader class.
    public static SceneLoader Instance;

    // Enumeration representing the different scenes in the game.
    public enum Scene
    {
        Title,   // Title screen.
        Hub,     // Hub world.
        Noryx,   // Noryx world.
        Loikart, // Loikart world.
        Ending   // Ending screen.
    }

    // Ensures only one instance of SceneLoader exists and persists across scenes.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign the singleton instance.
            DontDestroyOnLoad(gameObject); // Preserve the SceneLoader across scene loads.
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances.
        }
    }

    // Loads the specified scene based on the Scene enumeration.
    public void loadScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene); // Load the scene by its index in the enumeration.
    }
}
