using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public LayerMask enemyLayer;

    private void Start() {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f, enemyLayer);

        GetComponent<Animator>().Play("BombExplode");
        //explosion animation

        foreach (Collider2D collider in colliders) {
            collider.GetComponent<Enemy>().TakeDamage(10);
        }


        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
