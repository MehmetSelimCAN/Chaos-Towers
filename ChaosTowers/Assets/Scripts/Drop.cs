using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

    private Transform quadMaterial;
    private Transform octoMaterial;
    private Transform lockOnMaterial;
    private Transform spinnerMaterial;
    private Transform blockadeMaterial;
    private Transform heart;
    private Transform bomb;

    private float quadDropProbability = 0.85f;
    private float octoDropProbability = 0.65f;
    private float lockOnDropProbability = 0.75f;
    private float spinnerDropProbability = 0.65f;
    private float blockadeDropProbability = 0.25f;
    private float heartDropProbability = 0.10f;
    private float bombDropProbability = 0.05f;

    private float randomQuadNumber;
    private float randomOctoNumber;
    private float randomLockOnNumber;
    private float randomSpinnerNumber;
    private float randomBlockadeNumber;
    private float randomHeartNumber;
    private float randomBombNumber;

    private void Awake() {
        quadMaterial = Resources.Load<Transform>("Prefabs/Drops/pfDropQuad");
        octoMaterial = Resources.Load<Transform>("Prefabs/Drops/pfDropOcto");
        lockOnMaterial = Resources.Load<Transform>("Prefabs/Drops/pfDropLockOn");
        spinnerMaterial = Resources.Load<Transform>("Prefabs/Drops/pfDropSpinner");
        blockadeMaterial = Resources.Load<Transform>("Prefabs/Drops/pfDropBlockade");
        heart = Resources.Load<Transform>("Prefabs/Drops/pfDropHeart");
        bomb = Resources.Load<Transform>("Prefabs/Drops/pfDropBomb");
    }

    public void DropItem() {
        randomQuadNumber = Random.Range(0f, 1f);
        randomOctoNumber = Random.Range(0f, 1f);
        randomLockOnNumber = Random.Range(0f, 1f);
        randomSpinnerNumber = Random.Range(0f, 1f);
        randomBlockadeNumber = Random.Range(0f, 1f);
        randomHeartNumber = Random.Range(0f, 1f);
        randomBombNumber = Random.Range(0f, 1f);

        if (randomQuadNumber <= quadDropProbability) {
            Instantiate(quadMaterial, transform.position, Quaternion.identity);
            //drop quad material
        }
        if (randomOctoNumber <= octoDropProbability) {
            Instantiate(octoMaterial, transform.position, Quaternion.identity);
            //drop octo material
        }
        if (randomLockOnNumber <= lockOnDropProbability) {
            Instantiate(lockOnMaterial, transform.position, Quaternion.identity);
            //drop octo material
        }
        if (randomSpinnerNumber <= spinnerDropProbability) {
            Instantiate(spinnerMaterial, transform.position, Quaternion.identity);
            //drop octo material
        }
        if (randomBlockadeNumber <= blockadeDropProbability) {
            Instantiate(blockadeMaterial, transform.position, Quaternion.identity);
            //drop octo material
        }
        if (randomHeartNumber <= heartDropProbability) {
            Instantiate(heart, transform.position, Quaternion.identity);
            //drop heart
        }
        if (randomBombNumber <= bombDropProbability) {
            Instantiate(bomb, transform.position, Quaternion.identity);
            //drop bomb
        }
    }
}
