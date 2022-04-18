using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    private float disappearTimer = 7f;
    private float dissolveTime = 2f;

    private float itemSpeed = 3.5f;
    private Transform player;
    private Vector3 direction;

    private void Start() {
        player = GameObject.Find("Player").transform;
    }

    private void Update() {
        direction = player.position - transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f) {
            transform.position += direction.normalized * itemSpeed * Time.deltaTime;
        }


        disappearTimer -= Time.deltaTime;

        if (disappearTimer < dissolveTime) {
            GetComponent<Animator>().Play("Dissolve");
        }

        if (disappearTimer < 0f) {
            Destroy(transform.gameObject);
        }
    }

    public enum ItemType {
        Quad,
        Octo,
        LockOn,
        Spinner,
        Blockade,
        Heart,
        Bomb
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (itemType == ItemType.Quad) {
                BuildingManager.quadMaterialCount++;
                //increase quad material count
            }
            if (itemType == ItemType.Octo) {
                BuildingManager.octoMaterialCount++;
                //increase octo material count
            }
            if (itemType == ItemType.LockOn) {
                BuildingManager.lockOnMaterialCount++;
                //increase octo material count
            }
            if (itemType == ItemType.Spinner) {
                BuildingManager.spinnerMaterialCount++;
                //increase octo material count
            }
            if (itemType == ItemType.Blockade) {
                BuildingManager.blockadeMaterialCount++;
                //increase octo material count
            }
            if (itemType == ItemType.Heart) {
                GameManager.GainHeart();
                //increase heart count
            }
            if (itemType == ItemType.Bomb) {
                GameManager.GainBomb();
                //increase bomb count
            }

            Destroy(gameObject);
            GameManager.RefreshUI();
        }
    }

}
