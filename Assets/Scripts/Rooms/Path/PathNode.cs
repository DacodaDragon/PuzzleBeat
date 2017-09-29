using UnityEngine;

public class PathNode : MonoBehaviour
{
    public Vector3 LocalPosition { get { return transform.localPosition; } set { transform.localPosition = value; } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 LocalEulerRotation { get { return transform.localEulerAngles; } set { transform.localRotation = Quaternion.Euler(value); } }
    public Vector3 EulerRotation { get { return transform.eulerAngles; } set { transform.rotation = Quaternion.Euler(value); } }
}
