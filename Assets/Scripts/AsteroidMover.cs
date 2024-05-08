using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed;
    private float maxBound;

    public void Initialize(Vector3 target, float moveSpeed, float maxZ)
    {
        targetPosition = target;
        speed = moveSpeed;
        maxBound = maxZ;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition || transform.position.z == maxBound)
        {
            Destroy(gameObject);
        }
    }
}
