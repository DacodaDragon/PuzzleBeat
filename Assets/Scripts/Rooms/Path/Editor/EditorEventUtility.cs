using UnityEngine;
using UnityEditor;

static class EditorEventUtility
{
    public static Vector2 GetMousePosition()
    {
        Vector2 mousePosition = Event.current.mousePosition;
        mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
        mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);
        return mousePosition;
    }

    public static bool OnMouseDown(int mouseButton)
    {
        return (Event.current.type == EventType.MouseDown
            && Event.current.button == mouseButton);
    }

    public static bool OnKeyDown(KeyCode key)
    {
        return (Event.current.type == EventType.KeyUp
            && Event.current.keyCode == key);
    }

    public static bool OnKeyUp(KeyCode key)
    {
        return (Event.current.type == EventType.KeyDown 
            && Event.current.keyCode == key);
    }
}