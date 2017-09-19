using UnityEngine;
using UnityEngine.Events;
public class LevelGeneratorComponent : MonoBehaviour
{
    [SerializeField] private int[] m_RoomIDList;
    [SerializeField] private GameObject[] m_RoomList;
    LevelGenerator lv = new LevelGenerator();

    UnityEvent OnComplete;

    public void GenerateLevel()
    {
        lv.FeedRooms(m_RoomList);
        lv.GenerateASync(m_RoomIDList, this);
        lv.onLevelLoadEnded += OnLevelLoadEnd;
    }

    public void GenerateLevel(Room[] RoomPrefabs, int[] RoomIDList)
    {
        lv.FeedRooms(RoomPrefabs);
        lv.GenerateASync(RoomIDList, this);
        lv.onLevelLoadEnded += OnLevelLoadEnd;
    }

    public void GenerateLevel(int[] RoomIdList)
    {
        lv.FeedRooms(m_RoomList);
        lv.GenerateASync(RoomIdList, this);
        lv.onLevelLoadEnded += OnLevelLoadEnd;
    }

    public void OnLevelLoadEnd()
    {
        if (lv.Failed)
        {

        }
    }
}