using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacle;
    public float waveInterval;

    public Transform target;
    public float maxHeightDifference;

    public bool active;
    public enum SpawnerType { Fast, Medium, Slow }
    public SpawnerType spawnerType;

    void Start()
    {
        active = true;
        target = GameObject.Find("Player").transform;
        StartCoroutine(SpawnCoroutine());
    }

    void Update()
    {
        if (target == null) return;
        float distance = Vector2.Distance(transform.position, target.position); // Distância horizontal
        float heightDifference = target.position.y - transform.position.y; // Diferença de altura

        if (heightDifference > maxHeightDifference && active) // Se fora da distância ou acima da altura limite
        {
            active = false;
        }
    }

    IEnumerator SpawnCoroutine()
    {
        while (active)
        {
            float randomCooldown;
            switch (spawnerType)
            {
                case SpawnerType.Fast:
                    randomCooldown = UnityEngine.Random.Range(2f, 2.5f);
                    break;
                case SpawnerType.Medium:
                    randomCooldown = UnityEngine.Random.Range(3f, 4f);
                    break;
                case SpawnerType.Slow:
                    randomCooldown = UnityEngine.Random.Range(4f, 5f);
                    break;
                default:
                    randomCooldown = 2f; // Default cooldown if no type is selected
                    break;
            }

            Destroy(Instantiate(obstacle, transform.position, quaternion.identity), 20);
            yield return new WaitForSeconds(randomCooldown);
        }
    }
}
