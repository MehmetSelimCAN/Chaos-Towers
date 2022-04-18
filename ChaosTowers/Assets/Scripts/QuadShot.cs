using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadShot : MonoBehaviour {

    private float timerMax = 5f;
    private float timer;
    private float bulletSpeed = 3f;
    private Transform bulletPrefab;

    private void Awake() {
        bulletPrefab = Resources.Load<Transform>("Prefabs/pfBulletQuad");
    }

    private void Start() {
        timer = timerMax;
    }

    private void Update() {
        if (GameManager.isGameOver) {
            this.enabled = false;
        }

        if (WaveManager.waveStopped) {
            timer = timerMax;
        }
        else {
            timer -= Time.deltaTime;

            if (timer < 0f) {
                Shot();
                timer = timerMax;
            }
        }
    }

    private void Shot() {
        Vector2[] directions = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };
        for (int i = 0; i < 4; i++) {
            Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = directions[i] * bulletSpeed;
        }
    }
}
