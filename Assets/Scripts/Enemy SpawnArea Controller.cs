using System.Collections;
using UnityEngine;

public class EnemySpawnAreaController : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public float timeBetweenEnemies;
    public float spawnDistance;
    public GameObject enemyPrefab;

    private bool playerInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            playerInside = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            playerInside = false;
            StopCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {

        while (playerInside)
        {
            Instantiate(
                enemyPrefab,
                new Vector3(0,0,0),
                Quaternion.identity
            );

            //Wait a set time till next spawn
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }
}
