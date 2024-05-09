using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        processHit();

        if (hitPoints < 1)
        {
            killEnemy();
        }
    }
    void killEnemy()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        PlayerPrefs.SetInt("Score", scoreBoard.getScore());
        Debug.Log(PlayerPrefs.GetInt("Score"));
    }

    void processHit()
    {
        Instantiate(hitFX, transform.position, Quaternion.identity);
        hitPoints--;
        scoreBoard.increaseScore(scorePerHit);
    }
}
