using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private ScoreBoard scoreBoard;

    [SerializeField] ParticleSystem crash;
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    void OnTriggerEnter(Collider other)
    {
        startCrashSequence();
    }

    void startCrashSequence()
    {
        crash.Play();
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", 0.7f);
    }

    void ReloadLevel()
    {
        // Destroy(gameObject);
        //int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currSceneIndex); // this is where you would change the scene level for menus
        PlayerPrefs.SetInt("Score", scoreBoard.getScore());
        SceneManager.LoadScene(2);
    }
}




