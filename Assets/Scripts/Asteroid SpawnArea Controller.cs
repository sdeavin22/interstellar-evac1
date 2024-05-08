using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnAreaController : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;
    public float timeInBetweenAsteroids;
    public Transform player;
    public float asteroidSpeed;

    private BoxCollider spawnCollider;
    private bool playerInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            playerInside = true;
            StartCoroutine(SpawnAndMoveAsteroids());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            playerInside = false;
            StopCoroutine(SpawnAndMoveAsteroids());

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    void Start()
    {
        spawnCollider = GetComponent<BoxCollider>();
    }

    private IEnumerator SpawnAndMoveAsteroids()
    {
        Vector3 boundsMin = spawnCollider.bounds.min;
        Vector3 boundsMax = spawnCollider.bounds.max;

        while (playerInside)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(boundsMin.x, boundsMax.x), // Random x within bounds
                Random.Range(boundsMin.y, boundsMax.y), // Random y within bounds
                boundsMin.z // Spawn at the min z side of the collider
            );
            GameObject asteroid = Instantiate(
                asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)],
                spawnPosition,
                Quaternion.identity,
                transform
            );

            Vector3 targetPosition = new Vector3(
                spawnPosition.x, // Same x
                spawnPosition.y, // Same y
                boundsMax.z // Target at the max z side of the collider
            );
            asteroid.AddComponent<AsteroidMover>().Initialize(targetPosition, asteroidSpeed, boundsMax.z);

            yield return new WaitForSeconds(timeInBetweenAsteroids);
        }
    }
}
