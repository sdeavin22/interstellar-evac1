using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit by" + other.gameObject.name);
        Destroy(gameObject);
    }
}
