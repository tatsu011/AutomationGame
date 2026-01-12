using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public float cellSize = 1f;

    private Dictionary<Vector2Int, Building> _buildings = new();

    void Awake() => Instance = this;

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector2 p = worldPos;
        return Vector2Int.RoundToInt(p / cellSize);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            (gridPos.x + 0.5f) * cellSize,
            (gridPos.y + 0.5f) * cellSize,
            0f
        );
    }

    public bool IsOccupied(Vector2Int pos) => _buildings.ContainsKey(pos);

    public bool PlaceBuilding(Vector2Int pos, Building prefab, Direction facing)
    {
        if (IsOccupied(pos)) return false;

        var worldPos = GridToWorld(pos);
        var rot = Quaternion.Euler(0, 0, facing.ToRotationZ());
        var bld = Instantiate(prefab, worldPos, rot);
        bld.GridPosition = pos;
        bld.Facing = facing;

        _buildings[pos] = bld;
        return true;
    }

    public Building GetBuilding(Vector2Int pos)
    {
        _buildings.TryGetValue(pos, out var b);
        return b;
    }

    public void RemoveBuilding(Vector2Int pos)
    {
        if (_buildings.TryGetValue(pos, out Building b))
        {
            Destroy(b.gameObject);
            _buildings.Remove(pos);
        }
    }
}