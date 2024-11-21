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
        World1,
        World2,
        Battle
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
        if (scene == Scene.Title)
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if (scene == Scene.Battle)
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}
