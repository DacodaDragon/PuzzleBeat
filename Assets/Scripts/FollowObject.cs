using UnityEngine;
public class FollowObject : MonoBehaviour
{
    [SerializeField]
    GameObject m_ObjectToFollow;

    public void LateUpdate()
    {
        transform.position = m_ObjectToFollow.transform.position + new Vector3(0, 0, -10);
    }
}