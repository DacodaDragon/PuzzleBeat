using UnityEngine;
public class Level : ScriptableObject
{
    [SerializeField]
    Room[] m_roomPrefabs;
    [SerializeField]
    int[] m_IDList;
}