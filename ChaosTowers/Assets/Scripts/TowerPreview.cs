using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Transform towerPreview;

    private void Start() {
        towerPreview = transform.parent.Find("towerPreview");
    }


    public void OnPointerEnter(PointerEventData eventData) {
        towerPreview.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        towerPreview.gameObject.SetActive(false);
    }
}
