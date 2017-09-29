using System.Collections.Generic;
using UnityEngine;

public class RoomPath : MonoBehaviour
{
    [SerializeField]
    List<PathNode> pathNodes = new List<PathNode>();

    public PathNode EndNode
    {
        get
        {
            if (pathNodes.Count < 0) throw new System.Exception("Room does not have enough Pathnodes");
            return pathNodes[pathNodes.Count - 1];
        }
    }
    public PathNode StartNode { get { return pathNodes[0]; } }
    public List<PathNode> Path { get { return pathNodes; } set { pathNodes = value; } }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (/*Selected?*/true)
            for (int i = 0; i < pathNodes.Count - 1; i++)
                Gizmos.DrawLine(pathNodes[i].Position, pathNodes[i + 1].Position);
    }
}