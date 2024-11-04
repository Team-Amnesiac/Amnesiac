using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum Scene
    {
        Title,
        Hub,
        World1,
        World2,
        Battle
    }


    public static SceneLoader Instance;
    

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

    public void LoadScene(Scene scene)
    {
        if (scene == Scene.Battle)
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}
