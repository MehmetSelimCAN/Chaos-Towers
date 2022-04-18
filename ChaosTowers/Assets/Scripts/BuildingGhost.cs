using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private Sprite spBlockade;

    private void Awake() {
        spBlockade = Resources.Load<Sprite>("Sprites/spBlockade");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.CompareTag("Road") || collision.CompareTag("Tree")) && GetComponentInParent<SpriteRenderer>().sprite != spBlockade) {
            BuildingManager.isBuildingGhostOnRoad = true;
        }

        if ((collision.CompareTag("Road") || collision.CompareTag("Tree")) && GetComponentInParent<SpriteRenderer>().sprite == spBlockade) {
            BuildingManager.isBuildingGhostOnRoad = false;
        }

        if (collision.tag == "towerBottom") {
            BuildingManager.isBuildingGhostOnTower = true;
        }
        }

    private void OnTriggerStay2D(Collider2D collision) {
        if ((collision.CompareTag("Road") || collision.CompareTag("Tree")) && GetComponentInParent<SpriteRenderer>().sprite != spBlockade) {
            BuildingManager.isBuildingGhostOnRoad = true;
        }

        if ((collision.CompareTag("Road") || collision.CompareTag("Tree")) && GetComponentInParent<SpriteRenderer>().sprite == spBlockade) {
            BuildingManager.isBuildingGhostOnRoad = false;
        }

        if (collision.tag == "towerBottom") {
            BuildingManager.isBuildingGhostOnTower = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Road") || collision.CompareTag("Tree")) {
            BuildingManager.isBuildingGhostOnRoad = false;
        }

        if (collision.tag == "towerBottom") {
            BuildingManager.isBuildingGhostOnTower = false;
        }
    }
}
