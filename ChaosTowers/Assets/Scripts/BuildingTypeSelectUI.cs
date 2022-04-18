using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    private Camera mainCamera;
    private Transform cursor;
    private Sprite defaultCursorSprite;
    private Sprite wrenchSprite;
    private Sprite destroyCursorSprite;

    private bool canBuy = false;

    private void Awake() {
        wrenchSprite = Resources.Load<Sprite>("Sprites/spWrench");
        destroyCursorSprite = Resources.Load<Sprite>("Sprites/spDestroy");
        defaultCursorSprite = Resources.Load<Sprite>("Sprites/spDefaultCursor");

        cursor = GameObject.Find("Cursor").GetComponent<Transform>();
        cursor.GetComponent<SpriteRenderer>().sprite = defaultCursorSprite;

        Transform buttonTemplate = transform.Find("buttonTemplate");
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>("BuildingTypeListSO");
        int index = 0;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            float offsetAmount = -100f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetAmount * index);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            buttonTransform.Find("nameText").GetComponent<Text>().text = buildingType.nameString;
            buttonTransform.name = buildingType.nameString + "Button";
            buttonTransform.Find("materialImage").GetComponent<Image>().sprite = buildingType.materialSprite;
            buttonTransform.Find("materialImage").GetComponent<RectTransform>().sizeDelta = new Vector2(buildingType.materialSprite.texture.width, buildingType.materialSprite.texture.height);
            
            buttonTransform.Find("towerPreview").Find("materialImage").GetComponent<Image>().sprite = buildingType.materialSprite;
            buttonTransform.Find("towerPreview").Find("towerImage").GetComponent<Image>().sprite = buildingType.sprite;
            buttonTransform.Find("towerPreview").Find("towerNameText").GetComponent<Text>().text = buildingType.nameString;
            buttonTransform.Find("towerPreview").Find("costText").GetComponent<Text>().text = "COST : " + buildingType.neededMaterialCount.ToString();
            buttonTransform.Find("towerPreview").Find("instructionText").GetComponent<Text>().text = buildingType.instruction;

            buttonTransform.Find("towerPreview").gameObject.SetActive(false);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                if ((buildingType.materialName == "QuadMaterial" && buildingType.neededMaterialCount <= BuildingManager.quadMaterialCount) ||
                    (buildingType.materialName == "OctoMaterial" && buildingType.neededMaterialCount <= BuildingManager.octoMaterialCount) ||
                    (buildingType.materialName == "LockOnMaterial" && buildingType.neededMaterialCount <= BuildingManager.lockOnMaterialCount) ||
                    (buildingType.materialName == "SpinnerMaterial" && buildingType.neededMaterialCount <= BuildingManager.spinnerMaterialCount) ||
                    (buildingType.materialName == "BlockadeMaterial" && buildingType.neededMaterialCount <= BuildingManager.blockadeMaterialCount)) { 
                        canBuy = true;
                }
                else {
                    canBuy = false;
                }

                if (canBuy) {
                    BuildingManager.Instance.getBuildingGhost().gameObject.SetActive(true);
                    BuildingManager.Instance.setActiveBuilingType(buildingType);
                    BuildingManager.Instance.getBuildingGhost().GetComponent<SpriteRenderer>().sprite = buildingType.sprite;
                    cursor.GetComponent<SpriteRenderer>().sprite = defaultCursorSprite;
                    Cursor.canDestroy = false;
                    GameObject.Find("Wrench").GetComponent<SpriteRenderer>().sprite = wrenchSprite;
                    BuildingManager.Instance.getPlaidBackgroundTilemap().gameObject.SetActive(true);
                    }
            });

            index++;
        }


        Transform buildingDestroyerButton = transform.Find("BuildingDestroyer");
        Transform cancelSelectButton = transform.Find("CancelSelect");

        buildingDestroyerButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.setActiveBuilingType(null);
            BuildingManager.Instance.getBuildingGhost().gameObject.SetActive(false);
            cursor.GetComponent<SpriteRenderer>().sprite = destroyCursorSprite;
            Cursor.canDestroy = true;
            GameObject.Find("Wrench").GetComponent<SpriteRenderer>().sprite = wrenchSprite;
            BuildingManager.Instance.getPlaidBackgroundTilemap().gameObject.SetActive(false);
        });

        cancelSelectButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.setActiveBuilingType(null);
            BuildingManager.Instance.getBuildingGhost().gameObject.SetActive(false);
            cursor.GetComponent<SpriteRenderer>().sprite = defaultCursorSprite;
            Cursor.canDestroy = false;
            GameObject.Find("Wrench").GetComponent<SpriteRenderer>().sprite = null;
            BuildingManager.Instance.getPlaidBackgroundTilemap().gameObject.SetActive(false);
        });
    }

    private void Start() {
        mainCamera = Camera.main;
        BuildingManager.Instance.getBuildingGhost().gameObject.SetActive(false);
    }

    private void Update() {
        cursor.position = getMousePosition();
    }

    private Vector3 getMousePosition() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
