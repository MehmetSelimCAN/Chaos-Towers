using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypeSO")]
public class EnemyTypeSO : ScriptableObject {

    public string nameString;
    public Transform prefab;
    public Sprite sprite;

}
