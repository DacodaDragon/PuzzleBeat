using UnityEngine;
using System;
using System.Collections.Generic;

public class RoomPathUtility
{
    static public float GetLatestNodeAngle(RoomPath roomPath)
    {
        if (roomPath.Path.Count < 2)
            throw new Exception("Room does not have enough PathNodes");

        // We use worldpositions as we want the angle in worldspace.
        Vector2 vecFrom = roomPath.Path[roomPath.Path.Count - 2].Position;
        Vector2 vecTo = roomPath.Path[roomPath.Path.Count - 1].Position;

        return Vector2DMath.GetAngleBetween(vecFrom, vecTo);
    }

    static public float GetFirstNodeAngle(RoomPath roomPath)
    {
        if (roomPath.Path.Count < 2)
            throw new Exception("Room does not have enough PathNodes");

        // We use worldpositions as we want the angle in worldspace.
        Vector2 vecFrom = roomPath.Path[0].Position;
        Vector2 vecTo = roomPath.Path[1].Position;

        return Vector2DMath.GetAngleBetween(vecFrom, vecTo);
    }

    static public Vector2 GetLatestNodePosition(RoomPath roomPath)
    {
        if (roomPath.Path.Count == 0)
            throw new Exception("Room does not have any PathNodes");
        return roomPath.Path[roomPath.Path.Count - 1].Position;
    }

    static public Vector2[] ConvertToVectors(RoomPath roomPath)
    {
        List<Vector2> s = new List<Vector2>();
        for (int i = 0; i < roomPath.Path.Count; i++)
        {
            s.Add(roomPath.Path[i].Position);
        }
        return s.ToArray();

    }
}