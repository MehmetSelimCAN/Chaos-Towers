using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

    [SerializeField]
    private ObjectType objectType;

    public enum ObjectType {
        Player,
        Mammoth,
        Yeti,
        Wendigo
    }

    public ObjectType GetKeyType() {
        return objectType;
    }
}
