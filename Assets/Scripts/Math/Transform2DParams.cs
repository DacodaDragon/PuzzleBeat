using UnityEngine;
public struct Transform2DParams
{
    public Vector2 position;
    public Quaternion rotation;

    public Transform2DParams(Vector2 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}