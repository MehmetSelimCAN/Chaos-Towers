using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave {
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
}

public class WaveManager : MonoBehaviour {

    public static WaveManager Instance;

    private Button startButton;
    private Text waveText;
    public static bool waveStopped = true;
    private Transform waveCompletedScreen;

    public Transform spawnPoint;
    public Wave[] waves;
    private Wave currentWave;
    private int waveNumber = 0;


    private void Awake() {
        Instance = this;
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        waveText = GameObject.Find("waveNumber").GetComponent<Text>();

        startButton.onClick.AddListener(() => {
            if (waveNumber < waves.Length) {
                WaveStart();
            }
        });
    }

    private void Start() {
        waveStopped = true;
        currentWave = waves[waveNumber];
        waveCompletedScreen = GameObject.Find("WaveCompleted").transform;
        waveCompletedScreen.gameObject.SetActive(false);
    }

    private void Update() {
        if (!waveStopped) {
            CheckEnemy();
        }
    }

    private void WaveStart() {
        waveText.text = "Wave : " + (waveNumber + 1);
        currentWave = waves[waveNumber];
        waveStopped = false;
        StartCoroutine(SpawnWave());
    }

    public void CheckEnemy() {
        if (!GameObject.FindGameObjectWithTag("Enemy")) {
            WaveEnd();
        }
    }

    private void WaveEnd() {
        waveStopped = true;
        waveNumber++;
        if (waveNumber < waves.Length) {
            startButton.interactable = true;
            StartCoroutine(WaveCompleteAnimation());
        }
        else if (waveNumber == waves.Length) {
            GameManager.Win();
        }
        
    }

    private IEnumerator SpawnWave() {
        startButton.interactable = false;
        while (currentWave.noOfEnemies != 0) {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoint;
            GameObject enemy = Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            yield return new WaitForSeconds(1f / enemy.GetComponent<Enemy>().speed);
        }
    }

    private IEnumerator WaveCompleteAnimation() {
        waveCompletedScreen.gameObject.SetActive(true);
        waveCompletedScreen.GetComponent<Animator>().Play("WaveComplete");
        yield return new WaitForSeconds(2.5f);
        waveCompletedScreen.gameObject.SetActive(false);
    }
}
