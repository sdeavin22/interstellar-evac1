using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    private Transform player;
    public float speed = 10f;

    public float rotationSpeed = 3f;
    private bool shouldRotate = true;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            UnityEngine.Debug.LogError("Missile script could not find an object with tag 'Player'.");
        }

        // Destroy(gameObject, 5f);
    }

    void Update()
    {
        
        if (player == null) return; // Ensure there is a player to target

        // Calculate the direction to the player
        Vector3 targetDirection = player.position - transform.position;
        targetDirection.Normalize(); // Normalize the direction vector

        // Calculate the angle between the missile's forward direction and the direction to the player
        float angleToPlayer = Vector3.Angle(transform.forward, targetDirection);

        // Check if the missile has gone past the player
        if (angleToPlayer > 45) // 90 degrees is directly to the side
        {
            shouldRotate = false; // Stop rotating if the missile has gone past the player
        }

        // Rotate the missile to face the player smoothly if it should still rotate
        if (shouldRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        

        // Move the missile forward in its current facing direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Implement damage logic here
            // Example: other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);

            ParentEnemyMissile parentController = GetComponentInParent<ParentEnemyMissile>();
            if (parentController != null)
            {
                parentController.DestroyMissile();
            }
            else
            {
                UnityEngine.Debug.LogError("Missile has no ParentEnemyMissile attached to its parent.");
            }
        }
    }
}
