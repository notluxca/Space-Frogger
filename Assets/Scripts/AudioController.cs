using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
   // salve Luquinhas, apaga isso aq depois, vapo vapo, tudo pelo streak , sexoooo
    public Transform target;
    public float maxDistance = 10f; // Distância máxima para o som ser audível
    public float minDistance = 1f;  // Distância mínima para o volume máximo e pitch mais alto
    public float maxHeightDifference = 5f; // Diferença máxima de altura para o som ser audível
    private AudioSource audioSource;

    private float minPitch = 1.0f;
    private float maxPitch = 1.5f;

    private bool active = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        try
        {
            target = GameObject.Find("Player").transform;
        }
        catch (System.Exception)
        {
            return;
        }

    }

    void Update()
    {
        if (target == null) return;
        float distance = Vector2.Distance(transform.position, target.position); // Distância horizontal
        float heightDifference = target.position.y - transform.position.y; // Diferença de altura

        if (distance <= minDistance && heightDifference <= 0) // Se o alvo está perto e não acima
        {
            audioSource.volume = 1f;
            audioSource.pitch = maxPitch;
        }
        else if (heightDifference > maxHeightDifference && active) // Se fora da distância ou acima da altura limite
        {
            active = false;
            StartCoroutine(FadeOutAudio());
        }
        else
        {
            float distanceFactor = (distance - minDistance) / (maxDistance - minDistance);
            float heightFactor = Mathf.Clamp01(heightDifference / maxHeightDifference); // Fator de altura entre 0 e 1

            audioSource.volume = (1f - distanceFactor) * (1f - heightFactor);
            audioSource.pitch = Mathf.Lerp(maxPitch, minPitch, distanceFactor);
        }
    }

    IEnumerator FadeOutAudio()
    {

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / 2f; // Suavizar o fade
            yield return null;
        }
        audioSource.Stop();
    }
}
