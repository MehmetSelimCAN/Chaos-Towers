using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject {

    public string nameString;
    public Transform prefab;
    public Sprite sprite;
    public Sprite materialSprite;
    public string materialName;
    public int neededMaterialCount;
    public string instruction;

}
