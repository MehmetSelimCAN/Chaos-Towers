using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    private float timerMax = 0.2f;
    private float timer;
    private float bulletSpeed = 3f;
    private Transform bulletPrefab;
    private float angle = 0;

    private void Awake() {
        bulletPrefab = Resources.Load<Transform>("Prefabs/pfBulletSpinner");
        timer = timerMax;
    }

    private void Update() {
        if (GameManager.isGameOver) {
            this.enabled = false;
        }

        if (WaveManager.waveStopped) {
            angle = 0;
            timer = timerMax;
        }
        else {
            angle++;
            if (angle >= 360)
                angle = 0;


            timer -= Time.deltaTime;
            if (timer < 0f) {
                Shot();
                timer = timerMax;
            }
        }
    }

    private void Shot() {
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2FromAngle(angle) * bulletSpeed;
    }

    public Vector2 Vector2FromAngle(float angle) {
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
