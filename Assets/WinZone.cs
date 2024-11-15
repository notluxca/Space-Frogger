using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    [SerializeField] float timeCooldown;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        UIManager.Instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextLevelName);
    }
}
