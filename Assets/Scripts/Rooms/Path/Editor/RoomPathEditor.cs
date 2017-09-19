using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomPath))]
public class RoomPathEditor : Editor {

    private bool EditMode = false;
    private float SnapAngle = 45;
    private float SnapDistance = 4;

    private bool MouseEditing = false;

    public override void OnInspectorGUI()
    {
        RoomPath roomPath = target as RoomPath;
        if (GUILayout.Button("Toggle Edit")) EditMode = !EditMode;

        if (!EditMode)
            return;

        GUILayout.Label("Relative Angle Snap");
        SnapAngle = EditorGUILayout.DelayedFloatField(SnapAngle);

        GUILayout.Label("Relative Distance Snap");
        SnapDistance = EditorGUILayout.FloatField(SnapDistance);

        GUILayout.Space(5);
        
        GUILayout.Label("Node Control");
        if (GUILayout.Button("Add")) { RoomPathEditorUtility.AddNode(roomPath); }
        if (GUILayout.Button("Remove")) { RoomPathEditorUtility.RemoveNode(roomPath); }

        base.OnInspectorGUI();
    }

    public void OnSceneGUI()
    {
        RoomPath roomPath = target as RoomPath;
        RoomPathEditorUtility.DrawPathLine(roomPath);

        if (!EditMode)
            return;

        if (EditorEventUtility.OnKeyUp(KeyCode.LeftControl))
        {
            MouseEditing = true;
            ActiveEditorTracker.sharedTracker.isLocked = true;
        }

        if (EditorEventUtility.OnKeyDown(KeyCode.LeftControl))
        {
            MouseEditing = false;
            ActiveEditorTracker.sharedTracker.isLocked = false;
        }

        if (MouseEditing)
        {
            if (EditorEventUtility.OnMouseDown(0))
            {
                Vector2 mousePosition = EditorEventUtility.GetMousePosition();
                RoomPathEditorUtility.AddNode(roomPath, mousePosition);
            }

            if (EditorEventUtility.OnMouseDown(1))
                RoomPathEditorUtility.AddNode(roomPath);
        }

        RoomPathEditorUtility.MovePathNodes(roomPath);
    }
}
