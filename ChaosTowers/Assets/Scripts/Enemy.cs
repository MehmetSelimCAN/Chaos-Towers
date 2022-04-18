using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform path;
    private int wayPoint;
    private Transform currentTarget;
    private Vector2 movementDirection;
    private Rigidbody2D rb;

    [HideInInspector] public float speed;
    private int health;

    private Drop drop;

    [SerializeField] private EnemyType enemyType;

    public enum EnemyType {
        BlueEnemy,
        RedEnemy,
        PurpleEnemy,
        GreenEnemy,
        YellowEnemy
    }

    private void Awake() {
        if (enemyType == EnemyType.BlueEnemy) {
            speed = 1f;
            health = 10;
        }
        else if (enemyType == EnemyType.RedEnemy) {
            speed = 1f;
            health = 20;
        }
        else if (enemyType == EnemyType.PurpleEnemy) {
            speed = 2f;
            health = 20;
        }
        else if (enemyType == EnemyType.GreenEnemy) {
            speed = 0.5f;
            health = 50;
        }
        else if (enemyType == EnemyType.YellowEnemy) {
            speed = 3f;
            health = 10;
        }

        path = Resources.Load<Transform>("Prefabs/Path");
    }

    private void Start() {
        currentTarget = path.GetChild(0).transform;
        wayPoint = 0;
        movementDirection = currentTarget.position - transform.position;
        drop = GetComponent<Drop>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (GameManager.isGameOver) {
            rb.velocity = Vector2.zero;
            this.enabled = false;
        }

        movementDirection = currentTarget.position - transform.position;

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.11f) {
            wayPoint++;
            if (wayPoint < path.childCount) {
                currentTarget = path.GetChild(wayPoint).transform;
                movementDirection = currentTarget.position - transform.position;
            }
            else {
                GameManager.TakeDamage();
                Destroy(gameObject);
                //Damage player
            }

            if (movementDirection.normalized.x > 0) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void FixedUpdate() {
        rb.velocity = movementDirection.normalized * speed;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health < 1) {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() {
        drop.DropItem();
        yield return new WaitForSeconds(0f);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 10);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Bullet") {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }
}
