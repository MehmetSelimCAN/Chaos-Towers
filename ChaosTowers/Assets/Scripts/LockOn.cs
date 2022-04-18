using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour {

    private float lockTime = 2f;
    private float waitTime = 3f;
    private float bulletSpeed = 4.5f;
    private Transform bulletPrefab;

    private Transform nearestObject;
    private int integerValueForLayerMask;

    private LineRenderer lineRenderer;
    float colorProgress = 0;

    private void Awake() {
        bulletPrefab = Resources.Load<Transform>("Prefabs/pfBulletLockOn");
    }

    private void Start() {
        integerValueForLayerMask = LayerMask.GetMask("Enemy", "Player");
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;
        colorProgress = 0;
    }

    private void Update() {
        if (GameManager.isGameOver) {
            this.enabled = false;
        }

        if (WaveManager.waveStopped) {
            lockTime = 2f;
            waitTime = 3f;
            colorProgress = 0f;
        }
        else {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0) {
                //lock animation

                lockTime -= Time.deltaTime;

                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 100f, integerValueForLayerMask);
                nearestObject = GetNearestObject(objects);

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, nearestObject.position);
                colorProgress += Time.deltaTime / 2.0f;
                Color currentColor = Color.Lerp(Color.yellow, Color.red, colorProgress);
                SetSingleColor(lineRenderer, currentColor);

                //get locked
                if (lockTime <= 0) {
                    //Shoot
                    colorProgress = 0f;
                    lineRenderer.enabled = false;
                    Vector2 direction = nearestObject.position - transform.position;
                    StartCoroutine(Shot(direction));

                    waitTime = 3f;
                    lockTime = 2f;
                }
            }
        }
    }

    private IEnumerator Shot(Vector2 direction) {

        for (int i = 0; i < 5; i++) {
            Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
            yield return new WaitForSeconds(0.075f);
        }
    }

    private Transform GetNearestObject(Collider2D[] objects) {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider2D t in objects) {
            float dist = Vector3.Distance(t.GetComponent<Transform>().position, currentPos);
            if (dist < minDist) {
                tMin = t.GetComponent<Transform>();
                minDist = dist;
            }
        }
        return tMin;
    }

    private void SetSingleColor(LineRenderer lineRendererToChange, Color newColor) {
        lineRendererToChange.startColor = newColor;
        lineRendererToChange.endColor = newColor;
    }
}
