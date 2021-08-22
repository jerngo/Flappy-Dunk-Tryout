using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void quitGame() {
        Debug.Log("Application Quitting");
        Application.Quit();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame() {
        SceneManager.LoadScene("PlayScene");
    }
}
