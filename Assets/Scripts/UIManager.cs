using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Animator blackTransitionAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SetBlackTransitionReference();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetBlackTransitionReference();
    }

    private void SetBlackTransitionReference()
    {
        var blackTransitionObject = GameObject.Find("BlackTransition");

        if (blackTransitionObject != null)
        {
            blackTransitionAnimator = blackTransitionObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("BlackTransition object not found in the scene.");
        }
    }

    public void FadeToBlack()
    {
        if (blackTransitionAnimator == null)
        {
            Debug.LogWarning("BlackTransition Animator is not set.");
            return;
        }

        blackTransitionAnimator.SetTrigger("FadeIn");
    }

    public void FadeFromBlack()
    {
        if (blackTransitionAnimator == null)
        {
            Debug.LogWarning("BlackTransition Animator is not set.");
            return;
        }

        blackTransitionAnimator.SetTrigger("FadeOut");
    }
}
