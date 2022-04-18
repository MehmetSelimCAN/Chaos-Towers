using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour {

    private Button pauseButton;
    private Button resumeButton;
    private Button restartButton;
    private Button mainMenuButton;
    private Transform pauseScreen;

    private void Awake() {
        pauseScreen = GameObject.Find("PauseScreen").transform;
        resumeButton = pauseScreen.Find("ResumeButton").GetComponent<Button>();
        restartButton = pauseScreen.Find("RestartButton").GetComponent<Button>();
        mainMenuButton = pauseScreen.Find("MainMenuButton").GetComponent<Button>();
        pauseScreen.gameObject.SetActive(false);

        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

        pauseButton.onClick.AddListener(() => {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        });

        resumeButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        });

        restartButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
    }
}
