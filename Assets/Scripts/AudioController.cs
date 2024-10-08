using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Transform target;
    public float maxDistance = 10f; // Distância máxima para o som ser audível
    public float minDistance = 1f;  // Distância mínima para o volume máximo e pitch mais alto
    private AudioSource audioSource;

    private float minPitch = 1.0f;
    private float maxPitch = 1.5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position); //* Distancia pro player

        if (distance <= minDistance) //* Distancia minima
        {
            audioSource.volume = 1f;
            audioSource.pitch = maxPitch;
        }
        else if (distance >= maxDistance) //* Distancia maxima
        {
            audioSource.volume = 0f;
            audioSource.pitch = minPitch;
        }
        else
        {
            float distanceFactor = (distance - minDistance) / (maxDistance - minDistance);
            audioSource.volume = 1f - distanceFactor;
            audioSource.pitch = Mathf.Lerp(maxPitch, minPitch, distanceFactor);
        }
    }
}
