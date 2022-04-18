using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    private Button playButton;

    private void Awake() {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();

        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene");
        });
    }
}
