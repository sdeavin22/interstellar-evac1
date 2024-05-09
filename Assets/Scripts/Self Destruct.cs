using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeUntilDestroy = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeUntilDestroy);
    }
}
