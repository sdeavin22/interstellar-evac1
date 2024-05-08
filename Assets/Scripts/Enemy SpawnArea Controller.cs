using System.Collections;
using System.Collections.Generic;
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

            float marginX = Screen.width * 0.3f;
            float marginY = Screen.height * 0.3f;

            // Calculate the viewport bounds at the distance in front of the player
            float playerDistance = (mainCamera.transform.position - player.position).magnitude;

            // Get the corners of the viewport at this distance, adjusted with margin
            Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(marginX, marginY, playerDistance));
            Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - marginX, Screen.height - marginY, playerDistance));

            // Generate a random position within these adjusted bounds
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);

            // Use the player's forward direction to find a point in front of the player at the calculated random position
            Vector3 forwardPoint = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;
            forwardPoint.x = randomX;
            forwardPoint.y = randomY;

            //Create the enemy at the calculated point
            Instantiate(
                enemyPrefab,
                forwardPoint,
                mainCamera.transform.rotation
            );
            Debug.Log($"Spawned Enemy at {forwardPoint}");

            //Wait a set time till next spawn
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }
}
