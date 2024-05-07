    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Implement damage logic here
            // Example: other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);

            Destroy(gameObject); // Destroy the missile on hit
        }
    }
}
