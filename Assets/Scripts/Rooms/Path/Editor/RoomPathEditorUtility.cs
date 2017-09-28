using UnityEditor;
using UnityEngine;
using System;

static class RoomPathEditorUtility
{
    static public void MovePathNodes(RoomPath roomPath)
    {
        for (int i = 1; i < roomPath.Path.Count; i++)
        {
            Undo.RecordObject(roomPath.Path[i], "Moved Pathnode");
            roomPath.Path[i].Position = Handles.PositionHandle(roomPath.Path[i].Position, Quaternion.identity);
        }
    }

    static public void AddNode(RoomPath roomPath)
    {
        
        Undo.SetCurrentGroupName("Add Node");
        int UndoGroup = Undo.GetCurrentGroup();
        GameObject newNode = new GameObject();
        Undo.RegisterCreatedObjectUndo(newNode, "Node Creation");
        Undo.RecordObject(roomPath, "Node List Addition");
        newNode.transform.SetParent(roomPath.gameObject.transform);
        newNode.transform.localPosition = Vector2.zero;
        newNode.name = "[" + roomPath.Path.Count + "] Path Node";
        //newNode.hideFlags = HideFlags.HideInHierarchy;
        if (roomPath.Path.Count > 0)
        {
            newNode.transform.localPosition = roomPath.Path[roomPath.Path.Count - 1].LocalPosition;
        }

        roomPath.Path.Add(newNode.AddComponent<PathNode>());
        Undo.CollapseUndoOperations(UndoGroup);
    }

    static public void AddNode(RoomPath roomPath, Vector2 position)
    {
        Undo.SetCurrentGroupName("Add Node");
        int UndoGroup = Undo.GetCurrentGroup();
        
        GameObject newNode = new GameObject();
        Undo.RegisterCreatedObjectUndo(newNode, "Node Creation");
        Undo.RecordObject(roomPath, "Node List Addition");
        newNode.transform.SetParent(roomPath.gameObject.transform);
        newNode.transform.position = position;
        newNode.name = "[" + roomPath.Path.Count + "] Path Node";
        newNode.hideFlags = HideFlags.HideInHierarchy;
        roomPath.Path.Add(newNode.AddComponent<PathNode>());

        Undo.CollapseUndoOperations(UndoGroup);
    }

    static public void RemoveNode(RoomPath roomPath)
    {
        Undo.SetCurrentGroupName("Remove Node");
        int UndoGroup = Undo.GetCurrentGroup();

        GameObject objectToDestroy = roomPath.Path[roomPath.Path.Count - 1].gameObject;

        Undo.RecordObject(roomPath, "Node List Subtraction");
        roomPath.Path.RemoveAt(roomPath.Path.Count - 1);

        Undo.DestroyObjectImmediate(objectToDestroy);
        Undo.CollapseUndoOperations(UndoGroup);
    }

    static public PathNode GetClosestNode(Vector2 position)
    {
        throw new NotImplementedException();
    }

    static public void DrawPathLine(RoomPath roomPath)
    {
        if (roomPath.Path.Count > 1)
        {
            Handles.color = Color.green;
            for (int i = 0; i < roomPath.Path.Count - 1; i++)
            {
                Handles.DrawDottedLine(
                    roomPath.Path[i].Position,
                    roomPath.Path[i + 1].Position,
                    2);
            }
        }
    }
}
