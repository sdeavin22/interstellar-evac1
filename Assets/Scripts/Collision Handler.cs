using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem crash;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            startCrashSequence();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            startCrashSequence();
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadSceneAsync(3);
        }

    }

    void startCrashSequence()
    {
        crash.Play();
        GetComponent<BoxCollider>().enabled = false;
        SceneManager.LoadSceneAsync(2);
    }
}




