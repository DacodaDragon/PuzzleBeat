using UnityEngine;
public static class Vector2DMath
{
    public static float GetAngleBetween(Vector2 from, Vector2 to)
    {
        Vector2 AngleVector = from - to;
        float angle = Mathf.Atan2(AngleVector.y, AngleVector.x) * Mathf.Rad2Deg;
        // Make top 0;
        angle += 90;
        if (angle < 0) angle += 360;
        return angle;
    }
}