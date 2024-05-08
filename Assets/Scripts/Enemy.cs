using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {

        prcoessHit();
        if (hitPoints < 1)
        {
            killEnemy();
        }
    }

    void killEnemy()
    {
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
        PlayerPrefs.SetInt("Score", scoreBoard.getScore());
        Debug.Log(PlayerPrefs.GetInt("Score"));
        SceneManager.LoadScene(2);
    }

    void prcoessHit()
    {
        GameObject vfx = Instantiate(hitFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        hitPoints--;
        scoreBoard.increaseScore(scorePerHit);
    }
}
