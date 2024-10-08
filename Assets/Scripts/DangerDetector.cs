using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerDetector : MonoBehaviour
{
    public float raycastDistance = 1.5f;
    public LayerMask collisionLayers;
    public AudioClip collisionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, collisionLayers);
        Debug.DrawRay(transform.position, Vector2.up * raycastDistance, Color.green);

        if (hit.collider != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}
