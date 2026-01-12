using UnityEngine;

public enum Direction { Up, Right, Down, Left }

public static class DirectionExtensions
{
    public static Vector2Int ToVector2Int(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => Vector2Int.up,
            Direction.Right => Vector2Int.right,
            Direction.Down => Vector2Int.down,
            Direction.Left => Vector2Int.left,
            _ => Vector2Int.right
        };
    }

    public static float ToRotationZ(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => 90f,
            Direction.Right => 0f,
            Direction.Down => 270f,
            Direction.Left => 180f,
            _ => 0f
        };
    }
}