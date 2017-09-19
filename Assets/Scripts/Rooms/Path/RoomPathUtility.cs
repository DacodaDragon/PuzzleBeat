using UnityEngine;
using System;

public class RoomPathUtility
{
    static public float GeLatestNodeAngle(RoomPath roomPath)
    {
        if (roomPath.Path.Count < 2)
            throw new Exception("Room does not have enough PathNodes");

        // We use worldpositions as we want the angle in worldspace.
        Vector2 vecFrom = roomPath.Path[roomPath.Path.Count - 2].Position;
        Vector2 vecTo = roomPath.Path[roomPath.Path.Count - 1].Position;

        return Vector2DMath.GetAngleBetween(vecFrom, vecTo);
    }

    static public Vector2 GetLatestNodePosition(RoomPath roomPath)
    {
        if (roomPath.Path.Count == 0)
            throw new Exception("Room does not have any PathNodes");
        return roomPath.Path[roomPath.Path.Count - 1].Position;
    }
}