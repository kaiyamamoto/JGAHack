
using UnityEngine;

public enum Direction
{
    Front,
    Back,
    Left,
    Right
}

public static class DirectionEx
{
    public static Vector3 Vector(this Direction dir)
    {
        float z = 0.0f;
        switch (dir)
        {
            case Direction.Back:
                z = 180.0f;
                break;
            case Direction.Left:
                z = 90.0f;
                break;
            case Direction.Right:
                z = 270.0f;
                break;
        }
        return new Vector3(0.0f, 0.0f, z);
    }
}