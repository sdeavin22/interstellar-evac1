using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEnemyMissile : MonoBehaviour
{
    // Public method to destroy the whole missile object
    public void DestroyMissile()
    {
        Destroy(gameObject);  // Destroys the parent and all children
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
