using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] ParticleSystem crash;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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
        audioManager.PlaySFX(audioManager.death);
        crash.Play();
        GetComponent<BoxCollider>().enabled = false;
        Wait(5f);
        SceneManager.LoadSceneAsync(2);
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}




