using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSettings() {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
