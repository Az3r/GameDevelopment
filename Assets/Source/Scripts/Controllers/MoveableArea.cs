
using UnityEngine;

[System.Serializable]
public class MovableArea
{
    public Vector2 horizontal = new Vector2(-8f, 8f);
    public Vector2 vertical = new Vector2(-12f, 12f);
    public float xMin => horizontal.x;
    public float xMax => horizontal.y;
    public float yMin => vertical.x;
    public float yMax => vertical.y;
}