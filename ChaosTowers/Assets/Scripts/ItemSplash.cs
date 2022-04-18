using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSplash : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 forceVector = Vector2.up;
    private Vector3 startPosition;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        forceVector *= Random.Range(1.5f, 2.5f);
        forceVector += new Vector2(Random.Range(-1f, 1f), Random.Range(-1.5f, 1.5f));
    }

    private void Update() {
        rb.position += forceVector * Time.deltaTime;
        forceVector -= Vector2.up * 5 * Time.deltaTime;

        if (Mathf.Abs(rb.position.y - (startPosition.y - Random.Range(-0.5f, 0.5f))) < 0.5f && forceVector.y < 0f) {
            this.enabled = false;
        }
    }

}
