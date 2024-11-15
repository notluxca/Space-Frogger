using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pointSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
            GetComponent<SpriteRenderer>().enabled = false;
            audioSource.clip = pointSound;
            audioSource.Play();
            Destroy(this.gameObject, 0.3f);
        }
    }
}
