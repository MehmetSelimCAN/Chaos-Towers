using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blockade : MonoBehaviour {

    private float health = 500;
    private Sprite blockadeHalfBroken;
    private Sprite blockadeFullBroken;

    private Tilemap plaidBackgroundTilemap;

    private void Awake() {
        blockadeHalfBroken = Resources.Load<Sprite>("Sprites/spBlockadeHalfBroken");
        blockadeFullBroken = Resources.Load<Sprite>("Sprites/spBlockadeFullBroken");

    }

    private void Start() {
        plaidBackgroundTilemap = GameObject.Find("BackgroundGrid").transform.Find("plaidBackground").GetComponent<Tilemap>();
    }

    private void TakeDamage(float damage) {
        health -= damage;

        if (health <= 250) {
            //change sprite
            transform.GetComponentInParent<SpriteRenderer>().sprite = blockadeHalfBroken;
            
            if (health <= 50) {
                //change sprite
                transform.GetComponentInParent<SpriteRenderer>().sprite = blockadeFullBroken;
            }
        }

        if (health <= 0) {
            #region unmark destroyed area

            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 4; j++) {
                    Vector3 firstTilePosition = transform.position + new Vector3(-0.375f + j * 0.25f, -0.125f - i * 0.25f, 0f);
                    Vector3Int cellCoordinates = plaidBackgroundTilemap.WorldToCell(firstTilePosition);
                    plaidBackgroundTilemap.SetTileFlags(cellCoordinates, TileFlags.None);
                    plaidBackgroundTilemap.SetColor(cellCoordinates, Color.white);
                }
            }

            #endregion
            //add breaking animation
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Bullet") {
            TakeDamage(10f);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            TakeDamage(2.5f);
        }
    }

}
