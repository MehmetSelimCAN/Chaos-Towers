using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }

    private BuildingTypeSO activeBuildingType;

    private Camera mainCamera;
    private Transform player;

    private Transform buildingGhost;
    private Tilemap plaidBackgroundTilemap;

    private Vector3 boundedPosition;

    public static bool isBuildingGhostOnRoad;
    public static bool isBuildingGhostOnTower;

    public static int quadMaterialCount = 40;
    public static int octoMaterialCount = 40;
    public static int lockOnMaterialCount = 30;
    public static int spinnerMaterialCount = 0;
    public static int blockadeMaterialCount = 0;

    private Transform pfDropQuad;
    private Transform pfDropOcto;
    private Transform pfDropLockOn;
    private Transform pfDropSpinner;

    private void Start() {
        Instance = this;

        mainCamera = Camera.main;
        player = GameObject.Find("Player").GetComponent<Transform>();

        pfDropQuad = Resources.Load<Transform>("Prefabs/Drops/pfDropQuad");
        pfDropOcto = Resources.Load<Transform>("Prefabs/Drops/pfDropOcto");
        pfDropLockOn = Resources.Load<Transform>("Prefabs/Drops/pfDropLockOn");
        pfDropSpinner = Resources.Load<Transform>("Prefabs/Drops/pfDropSpinner");

        buildingGhost = GameObject.Find("buildingGhost").transform;
        plaidBackgroundTilemap = GameObject.Find("BackgroundGrid").transform.Find("plaidBackground").GetComponent<Tilemap>();

        quadMaterialCount = 40;
        octoMaterialCount = 40;
        lockOnMaterialCount = 30;
        spinnerMaterialCount = 0;
        blockadeMaterialCount = 0;
}

    private void Update() {
        boundedPosition = getMousePosition();
        boundedPosition.x = Mathf.Clamp(boundedPosition.x, 0.25f * Mathf.RoundToInt(player.position.x / 0.25f) - 1f, 0.25f * Mathf.RoundToInt(player.position.x / 0.25f) + 1f);
        boundedPosition.y = Mathf.Clamp(boundedPosition.y, 0.25f * Mathf.RoundToInt(player.position.y / 0.25f) - 1f, 0.25f * Mathf.RoundToInt(player.position.y / 0.25f) + 1f);
        buildingGhost.position = boundedPosition;

        if (isBuildingGhostOnTower || isBuildingGhostOnRoad) {
            buildingGhost.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 100);
        }
        else {
            buildingGhost.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);

            if (activeBuildingType != null) {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                    if (!isBuildingGhostOnTower && !isBuildingGhostOnRoad) {
                            if (activeBuildingType.materialName == "QuadMaterial" && activeBuildingType.neededMaterialCount <= quadMaterialCount) {
                                quadMaterialCount -= activeBuildingType.neededMaterialCount;
                                
                            }
                            else if (activeBuildingType.materialName == "OctoMaterial" && activeBuildingType.neededMaterialCount <= octoMaterialCount) {
                                octoMaterialCount -= activeBuildingType.neededMaterialCount;
                            }
                            else if (activeBuildingType.materialName == "LockOnMaterial" && activeBuildingType.neededMaterialCount <= lockOnMaterialCount) {
                                lockOnMaterialCount -= activeBuildingType.neededMaterialCount;
                            }
                            else if (activeBuildingType.materialName == "SpinnerMaterial" && activeBuildingType.neededMaterialCount <= spinnerMaterialCount) {
                                spinnerMaterialCount -= activeBuildingType.neededMaterialCount;
                            }
                            else if (activeBuildingType.materialName == "BlockadeMaterial" && activeBuildingType.neededMaterialCount <= blockadeMaterialCount) {
                                blockadeMaterialCount -= activeBuildingType.neededMaterialCount;
                            }


                            #region mark builded area
                            for (int i = 0; i < 2; i++) {
                                for (int j = 0; j < 4; j++) {
                                    Vector3 firstTilePosition = buildingGhost.position + new Vector3(-0.375f + j * 0.25f, -0.125f - i * 0.25f, 0f);
                                    Vector3Int cellCoordinates = plaidBackgroundTilemap.WorldToCell(firstTilePosition);
                                    plaidBackgroundTilemap.SetTileFlags(cellCoordinates, TileFlags.None);
                                    plaidBackgroundTilemap.SetColor(cellCoordinates, Color.red);
                                }
                            }
                            #endregion


                            //refresh ui
                            GameManager.RefreshUI();
                            Instantiate(activeBuildingType.prefab, buildingGhost.position, Quaternion.identity);
                            setActiveBuilingType(null);
                            buildingGhost.gameObject.SetActive(false);
                            plaidBackgroundTilemap.gameObject.SetActive(false);
                    }
                }

            }
        }
    }

    public Tilemap getPlaidBackgroundTilemap() {
        return plaidBackgroundTilemap;
    }

    public Transform getBuildingGhost() {
        return buildingGhost;
    }

    private Vector3 getMousePosition() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.x = 0.25f * Mathf.RoundToInt(mouseWorldPosition.x / 0.25f);
        mouseWorldPosition.y = 0.25f * Mathf.RoundToInt(mouseWorldPosition.y / 0.25f);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    /*private Vector3 getTileMapCenter() {
        Vector3Int cellCoordinates = backgroundTilemap.WorldToCell(getMousePosition());
        Vector3 cursorPosition = backgroundTilemap.GetCellCenterWorld(cellCoordinates);
        return cursorPosition;
    }*/

    public void setActiveBuilingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
    }

    private BuildingTypeSO getActiveBuildingType() {
        return activeBuildingType;
    }

    public void DestroyBuilding(Transform building) {

        if (building.GetComponent<QuadShot>() != null) {
            for (int i = 0; i < 10; i++) {
                Instantiate(pfDropQuad, building.transform.position, Quaternion.identity);
            }
        }

        else if (building.GetComponent<OctoShot>() != null) {
            for (int i = 0; i < 20; i++) {
                Instantiate(pfDropOcto, building.transform.position, Quaternion.identity);
            }
        }

        else if (building.GetComponent<LockOn>() != null) {
            for (int i = 0; i < 30; i++) {
                Instantiate(pfDropLockOn, building.transform.position, Quaternion.identity);
            }
        }

        else if (building.GetComponent<Spinner>() != null) {
            for (int i = 0; i < 40; i++) {
                Instantiate(pfDropSpinner, building.transform.position, Quaternion.identity);
            }
        }

        #region unmark destroyed area

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 4; j++) {
                Vector3 firstTilePosition = building.position + new Vector3(-0.375f + j * 0.25f, -0.125f - i * 0.25f, 0f);
                Vector3Int cellCoordinates = plaidBackgroundTilemap.WorldToCell(firstTilePosition);
                plaidBackgroundTilemap.SetTileFlags(cellCoordinates, TileFlags.None);
                plaidBackgroundTilemap.SetColor(cellCoordinates, Color.white);
            }
        }

        #endregion

        Destroy(building.gameObject);
    }
}
