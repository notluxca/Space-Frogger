using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static instance accessible globally
    public static GameManager Instance { get; private set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagers
        }
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }

    public int playerScore = 0;
    public bool isGamePaused = false;


    public void AddScore(int value)
    {
        playerScore += value;
        Debug.Log("Score: " + playerScore);
    }

    private void OnNewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        playerScore = 0;
    }

}
