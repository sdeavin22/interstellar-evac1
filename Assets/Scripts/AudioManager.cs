using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource jetsSource;
    [SerializeField] AudioSource gunSource;
    [SerializeField] AudioSource enemySource;



    public AudioClip shoot;
    public AudioClip jets;
    public AudioClip death;
    public AudioClip asteroid_explosion_1;
    public AudioClip asteroid_explosion_2;
    public AudioClip enemy_shoot;
    public AudioClip enemy_death;
    public AudioClip enemy_spawn;

    public void Start()
    {
        jetsSource.clip = jets;
        jetsSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXEnemy(AudioClip clip)
    {
        enemySource.PlayOneShot(clip);
    }
    public void PlaySound()
    {
        if (!gunSource.isPlaying)
        {
            gunSource.Play();
        }
    }
    public void StopSound()
    {
        gunSource.Stop();
    }

}
