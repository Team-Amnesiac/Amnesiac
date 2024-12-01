using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    // Singleton instance of the SceneLoader class.
    public static SceneLoader Instance;


    public enum Scene
    {
        Title,
        Hub,
        Noryx,
        Loikart,
        Ending
    }
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void loadScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
