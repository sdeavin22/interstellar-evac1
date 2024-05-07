using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Starting variables for basic ai aim and fire
    public Transform player;
    public GameObject missilePrefab;
    public float firingInterval = 2.0f;
    private float nextFireTime = 0f;

    // Added variables for realistic aim
    private Vector3 lastPlayerPosition;
    private Vector3 playerVelocity;

    // State machine
    enum State { Idle, Attack }
    State currentState = State.Idle;

    void Update()
    {
        // Update player velocity
        if (player != null)
        {
            playerVelocity = (player.position - lastPlayerPosition) / Time.deltaTime;
            lastPlayerPosition = player.position;
        }

        LookAtPlayer();

        switch (currentState)
        {
            case State.Idle:
                // Checks if player is near to swap to attack state
                if (Vector3.Distance(transform.position, player.position) < 50f)
                {
                    currentState = State.Attack;
                }
                break;
            case State.Attack:
                if (Time.time >= nextFireTime)
                {
                    FireMissile();
                    nextFireTime = Time.time + firingInterval;
                }
                break;
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    void FireMissile()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        // Manually using 20 as missile speed, if missile speed gets changed this must follow
        float reachTime = distanceToPlayer.magnitude / 30;
        Vector3 predictedPosition = player.position + playerVelocity * reachTime;

        // Add inaccuracy
        float inaccuracy = 1.5f; // Adjust this value to increase/decrease inaccuracy
        Vector3 inaccuracyOffset = new Vector3(UnityEngine.Random.Range(-inaccuracy, inaccuracy), 0, UnityEngine.Random.Range(-inaccuracy, inaccuracy));
        predictedPosition += inaccuracyOffset;

        Quaternion firingRotation = Quaternion.LookRotation(predictedPosition - transform.position);
        Instantiate(missilePrefab, transform.position, firingRotation);
    }
}
