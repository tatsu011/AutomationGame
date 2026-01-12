using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public Camera mainCamera;
    public Building minerPrefab;
    public Building conveyorPrefab;
    public Building chestPrefab;

    public Direction currentFacing = Direction.Right;
    public enum BuildMode { Miner, Conveyor, Chest }
    public BuildMode buildMode = BuildMode.Miner;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) buildMode = BuildMode.Miner;
        if (Input.GetKeyDown(KeyCode.Alpha2)) buildMode = BuildMode.Conveyor;
        if (Input.GetKeyDown(KeyCode.Alpha3)) buildMode = BuildMode.Chest;

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Rotate clockwise
            currentFacing = (Direction)(((int)currentFacing + 1) % 4);
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceCurrent();
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
            _ => minerPrefab
        };

        GridManager.Instance.PlaceBuilding(gridPos, prefab, currentFacing);
    }
}