using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private Transform wrench;
    private Animator animator;
    private Camera mainCamera;
    private string currentAnimationState;

    //Animations
    private const string PLAYER_IDLE_W = "PlayerIdleW";
    private const string PLAYER_IDLE_A = "PlayerIdleA";
    private const string PLAYER_IDLE_S = "PlayerIdleS";
    private const string PLAYER_IDLE_D = "PlayerIdleD";
    private const string PLAYER_RUN_W = "PlayerRunW";
    private const string PLAYER_RUN_A = "PlayerRunA";
    private const string PLAYER_RUN_S = "PlayerRunS";
    private const string PLAYER_RUN_D = "PlayerRunD";

    //Movement variables
    [Range(0,10)]
    [SerializeField] private float movementSpeed;
    private Vector2 movement;
    private Vector2 previousMovement;


    private void Start() {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wrench = transform.Find("Wrench");
    }

    private void Update() {
        if (GameManager.isGameOver) {
            rb.velocity = Vector2.zero;
            this.enabled = false;
        }
        WrenchMovement();
    }

    private void FixedUpdate() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = Vector2.ClampMagnitude(movement, 1f);
        rb.velocity = movement * movementSpeed;

        #region Animation
        if (rb.velocity.x > 0) {
            ChangeAnimationState(PLAYER_RUN_D);
            previousMovement = rb.velocity;
        }
        else if (rb.velocity.x < 0) {
            ChangeAnimationState(PLAYER_RUN_A);
            previousMovement = rb.velocity;
        }
        else if (rb.velocity.y > 0) {
            ChangeAnimationState(PLAYER_RUN_W);
            previousMovement = rb.velocity;
        }
        else if (rb.velocity.y < 0) {
            ChangeAnimationState(PLAYER_RUN_S);
            previousMovement = rb.velocity;
        }
        else {
            if (previousMovement.x > 0) {
                ChangeAnimationState(PLAYER_IDLE_D);
            }
            else if (previousMovement.x < 0) {
                ChangeAnimationState(PLAYER_IDLE_A);
            }
            else if (previousMovement.y > 0) {
                ChangeAnimationState(PLAYER_IDLE_W);
            }
            else if (previousMovement.y < 0) {
                ChangeAnimationState(PLAYER_IDLE_S);
            }
        }
        #endregion

    }

    private void ChangeAnimationState(string newState) {
        if (currentAnimationState == newState) 
            return;

        animator.Play(newState);
        currentAnimationState = newState;
    }

    private void WrenchMovement() {
        if (wrench.GetComponent<SpriteRenderer>().sprite != null) {
            Vector2 boundedArea = getMousePosition() - transform.position;
            boundedArea.Normalize();
            boundedArea = Vector2.ClampMagnitude(boundedArea, 0.3f);
            float angle = AngleBetweenTwoPoints(wrench.position, getMousePosition());
        
            wrench.position = new Vector3(transform.position.x + boundedArea.x,transform.position.y + boundedArea.y, 0f);
            wrench.rotation = Quaternion.Euler(new Vector3(0f,0f, angle));

            if (wrench.localPosition.x < 0f) {
                if (wrench.GetComponent<SpriteRenderer>().sprite.name == "Wrench") {
                    wrench.GetComponent<SpriteRenderer>().flipX = true;
                    wrench.GetComponent<SpriteRenderer>().flipY = false;

                }
                else {
                    wrench.GetComponent<SpriteRenderer>().flipX = true;
                    wrench.GetComponent<SpriteRenderer>().flipY = false;
                }
            }
            else {
                if (wrench.GetComponent<SpriteRenderer>().sprite.name == "Wrench") {
                    wrench.GetComponent<SpriteRenderer>().flipX = false;
                    wrench.GetComponent<SpriteRenderer>().flipY = false;
                }
                else {
                    wrench.GetComponent<SpriteRenderer>().flipX = true;
                    wrench.GetComponent<SpriteRenderer>().flipY = true;
                }
            }
        }

    }

    private Vector3 getMousePosition() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Bullet") {
            GameManager.TakeDamage();
            Destroy(collision.gameObject);
        }
    }

}
