using UnityEngine;
using UnityEngine.Events;
public class LevelGeneratorComponent : MonoBehaviour
{
    [SerializeField] private int[] m_RoomIDList;
    [SerializeField] private GameObject[] m_RoomList;
    [SerializeField] private RoomManager m_roomManager;
    LevelGenerator lv = new LevelGenerator();

    [SerializeField]
    UnityEvent OnComplete;
    [SerializeField]
    UnityEvent<string> OnFail;


    void Start()
    {
        GenerateLevel();
    }

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
            Debug.LogError(lv.error.Message);
            if (OnFail != null) OnFail.Invoke(lv.error.Message);
            return;
        }
        else if (OnComplete != null)
        {
            m_roomManager.AddRooms(lv.Level);
            OnComplete.Invoke();
        }
    }
}