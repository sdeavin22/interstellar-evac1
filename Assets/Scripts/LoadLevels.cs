using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame() {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMenu() {
        SceneManager.LoadSceneAsync(0);
    }
}
