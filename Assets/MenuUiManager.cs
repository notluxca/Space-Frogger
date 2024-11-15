using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiManager : MonoBehaviour
{
    [SerializeField] GameObject creditsScreen;
    [SerializeField] Animator creditsText;

    void Start()
    {
        creditsScreen.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
        creditsText.SetTrigger("StartCredits");
    }

    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
        creditsText.StopPlayback();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
