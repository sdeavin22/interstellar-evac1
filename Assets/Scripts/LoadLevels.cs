using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
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
