using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool isGameOver = false;
    private static Transform gameOverUI;


    public static int bombCount = 0;
    private static int health = 10;
    private static Transform healthUI;
    private static Transform buildingTypeSelectUI;
    private static Transform bombUI;
    private static Transform youWinUI;

    private Button restartButton;
    private Button mainMenuButton;

    private static Sprite heartSprite;
    private static Sprite emptyHeartSprite;

    private void Awake() {
        heartSprite = Resources.Load<Sprite>("Sprites/spHeart");
        emptyHeartSprite = Resources.Load<Sprite>("Sprites/spEmptyHeart");
    }

    private void Start() {
        //Cursor.visible = false;
        Time.timeScale = 1f;
        health = 10;
        bombCount = 0;
        isGameOver = false;
        healthUI = GameObject.Find("Health").transform;
        youWinUI = GameObject.Find("YouWinScreen").transform;
        restartButton = youWinUI.Find("Restart").GetComponent<Button>();
        mainMenuButton = youWinUI.Find("MainMenu").GetComponent<Button>();

        restartButton.onClick.AddListener(() => {
            Restart();
        });

        mainMenuButton.onClick.AddListener(() => {
            MainMenu();
        });


        buildingTypeSelectUI = GameObject.Find("BuildingTypeSelectUI").transform;
        bombUI = GameObject.Find("Bomb").transform;
        
        gameOverUI = GameObject.Find("GameOverUI").transform;
        gameOverUI.Find("highScoreText").GetComponent<Text>().text = "High score : " + PlayerPrefs.GetInt("HighScore");
        gameOverUI.gameObject.SetActive(false);
        youWinUI.gameObject.SetActive(false);

        PlayerPrefs.SetInt("Score", 0);

        youWinUI.Find("highScoreText").GetComponent<Text>().text = "High score : " + PlayerPrefs.GetInt("HighScore");
        RefreshUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            Win();
        }
    }

    public static void GainBomb() {
        bombCount += 1;
        RefreshUI();
    }

    public static void GainHeart() {
        if (health < 10) {
            health += 1;
            RefreshUI();
        }
    }

    public static void TakeDamage() {
        health -= 1;
        RefreshUI();

        if (health < 1) {
            Die();
        }
    }

    private static void Die() {
        isGameOver = true;
        gameOverUI.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")) {
            gameOverUI.Find("highScoreText").GetComponent<Text>().text = "High score : " + PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }
        gameOverUI.Find("yourScoreText").GetComponent<Text>().text = "Your score : " + PlayerPrefs.GetInt("Score");
    }

    public static void RefreshUI() {
        #region health refresh
        for (int i = 0; i < health; i++) {
            healthUI.transform.GetChild(i).GetComponent<Image>().sprite = heartSprite;
        }

        for (int i = health; i < 10; i++) {
            healthUI.transform.GetChild(i).GetComponent<Image>().sprite = emptyHeartSprite;
        }
        #endregion

        #region bomb refresh

        bombUI.Find("text").GetComponent<Text>().text = bombCount.ToString();

        #endregion

        #region material refresh

        buildingTypeSelectUI.Find("QuadshotButton").Find("materialText").GetComponent<Text>().text = BuildingManager.quadMaterialCount.ToString() + "/20";
        if (BuildingManager.quadMaterialCount < 20) {
            buildingTypeSelectUI.Find("QuadshotButton").Find("image").GetComponent<Image>().color = new Color32(255,255,255, 180);
            buildingTypeSelectUI.Find("QuadshotButton").Find("nameText").GetComponent<Text>().color = new Color32(255,255,255, 180);
            buildingTypeSelectUI.Find("QuadshotButton").Find("materialText").GetComponent<Text>().color = new Color32(255,255,255, 180);
            buildingTypeSelectUI.Find("QuadshotButton").Find("materialImage").GetComponent<Image>().color = new Color32(255,255,255, 180);
        }
        else {
            buildingTypeSelectUI.Find("QuadshotButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("QuadshotButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("QuadshotButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("QuadshotButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        buildingTypeSelectUI.Find("OctoshotButton").Find("materialText").GetComponent<Text>().text = BuildingManager.octoMaterialCount.ToString() + "/40";
        if (BuildingManager.octoMaterialCount < 40) {
            buildingTypeSelectUI.Find("OctoshotButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("OctoshotButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("OctoshotButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("OctoshotButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
        }
        else {
            buildingTypeSelectUI.Find("OctoshotButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("OctoshotButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("OctoshotButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("OctoshotButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        buildingTypeSelectUI.Find("Lock OnButton").Find("materialText").GetComponent<Text>().text = BuildingManager.lockOnMaterialCount.ToString() + "/60";
        if (BuildingManager.lockOnMaterialCount < 60) {
            buildingTypeSelectUI.Find("Lock OnButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("Lock OnButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("Lock OnButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("Lock OnButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
        }
        else {
            buildingTypeSelectUI.Find("Lock OnButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("Lock OnButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("Lock OnButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("Lock OnButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        buildingTypeSelectUI.Find("SpinnerButton").Find("materialText").GetComponent<Text>().text = BuildingManager.spinnerMaterialCount.ToString() + "/80";
        if (BuildingManager.spinnerMaterialCount < 80) {
            buildingTypeSelectUI.Find("SpinnerButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("SpinnerButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("SpinnerButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("SpinnerButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
        }
        else {
            buildingTypeSelectUI.Find("SpinnerButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("SpinnerButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("SpinnerButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("SpinnerButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        buildingTypeSelectUI.Find("BlockadeButton").Find("materialText").GetComponent<Text>().text = BuildingManager.blockadeMaterialCount.ToString() + "/20";
        if (BuildingManager.blockadeMaterialCount < 20) {
            buildingTypeSelectUI.Find("BlockadeButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("BlockadeButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("BlockadeButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 180);
            buildingTypeSelectUI.Find("BlockadeButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 180);
        }
        else {
            buildingTypeSelectUI.Find("BlockadeButton").Find("image").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("BlockadeButton").Find("nameText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("BlockadeButton").Find("materialText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            buildingTypeSelectUI.Find("BlockadeButton").Find("materialImage").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        #endregion
    }

    public static void Win() {
        youWinUI.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")) {
            youWinUI.Find("highScoreText").GetComponent<Text>().text = "High score : " + PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }
        youWinUI.Find("yourScoreText").GetComponent<Text>().text = "Your score : " + PlayerPrefs.GetInt("Score");
    }

    public void Restart() {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
