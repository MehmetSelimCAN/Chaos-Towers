using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombThrow : MonoBehaviour {

    private Transform pfBomb;

    private void Awake() {
        pfBomb = Resources.Load<Transform>("Prefabs/pfBomb");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            ThrowBomb();
        }
    }

    private void ThrowBomb() {
        if (GameManager.bombCount > 0) {
            Instantiate(pfBomb, transform.position, Quaternion.identity);
            GameManager.bombCount--;
            GameManager.RefreshUI();
        }
    }

}
