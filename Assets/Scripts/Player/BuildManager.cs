using UnityEngine;

public class BuildManager : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] Building minerPrefab;
    [SerializeField] Building conveyorPrefab;
    [SerializeField] Building chestPrefab;

    [SerializeField] Direction currentFacing = Direction.Right;
    public enum BuildMode { Miner, Conveyor, Chest, None }
    [SerializeField] BuildMode buildMode = BuildMode.None;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) buildMode = BuildMode.Miner;
        if (Input.GetKeyDown(KeyCode.Alpha2)) buildMode = BuildMode.Conveyor;
        if (Input.GetKeyDown(KeyCode.Alpha3)) buildMode = BuildMode.Chest;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                // Rotate counter-clockwise
                currentFacing = (Direction)(((int)currentFacing + 3) % 4);
                return;
            }
            // Rotate clockwise
            currentFacing = (Direction)(((int)currentFacing + 1) % 4);
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceCurrent();
        }

        if(Input.GetMouseButtonDown(1)) //disable building.
        {
            buildMode = BuildMode.None;
        }
    }

    void PlaceCurrent()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        var mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = GridManager.Instance.WorldToGrid(mouseWorld);

        Building prefab = buildMode switch
        {
            BuildMode.Miner => minerPrefab,
            BuildMode.Conveyor => conveyorPrefab,
            BuildMode.Chest => chestPrefab,
            BuildMode.None => null,
            _ => null
        };
            if (prefab == null) return;
        GridManager.Instance.PlaceBuilding(gridPos, prefab, currentFacing);
    }
}