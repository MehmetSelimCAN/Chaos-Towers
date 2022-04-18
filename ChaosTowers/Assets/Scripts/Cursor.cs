using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour {

    public static Cursor Instance { get; private set; }

    public static bool canDestroy;
    private Transform tower;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        tower = null;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && tower != null) {
            BuildingManager.Instance.DestroyBuilding(tower);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Tower" && canDestroy) {
            collision.GetComponent<SpriteRenderer>().color = Color.red;
            tower = collision.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Tower" && canDestroy) {
            collision.GetComponent<SpriteRenderer>().color = Color.red;
            tower = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Tower" && canDestroy) {
            collision.GetComponent<SpriteRenderer>().color = Color.white;
            tower = null;
        }
    }
}
