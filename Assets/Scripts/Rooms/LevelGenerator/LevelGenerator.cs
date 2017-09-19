using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnLevelLoadEnded();

public class LevelGenerator
{
    [SerializeField]
    private Room[] m_roomPrefabs;
    private List<Room> m_rooms = new List<Room>();
    private bool m_Loaded;
    private bool m_Failed;
    public System.Exception error;

    public OnLevelLoadEnded onLevelLoadEnded;
    public bool Loaded { get { return m_Loaded; } }
    public bool Failed { get { return m_Failed; } }

    public Room[] Level { get { return m_rooms.ToArray(); } }

    public LevelGenerator()
    {

    }

    public LevelGenerator(IEnumerable<Room> rooms)
    {
        FeedRooms(rooms);
    }

    public LevelGenerator(IEnumerable<GameObject> rooms)
    {
        FeedRooms(rooms);
    }

    public void FeedRooms(IEnumerable<Room> rooms)
    {
        m_roomPrefabs = rooms.ToArray();
    }

    public void FeedRooms(IEnumerable<GameObject> rooms)
    {
        m_roomPrefabs = CheckPrefabs(rooms.ToArray());
    }

    private Room[] CheckPrefabs(GameObject[] objects)
    {
        List<Room> roomList = new List<Room>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (!objects[i].GetComponent<Room>())
                throw new System.Exception("GameObject passed without Room Component!");
            else roomList.Add(objects[i].GetComponent<Room>());
        }
        return roomList.ToArray();
    }

    public void GenerateLevel(int[] IDs)
    {
        for (int i = 0; i < IDs.Length; i++)
        {
            CreateRoom(FindRoom(IDs[i]));
        }
    }


    /// <param name="IDs"></param>
    /// <param name="MonoBehavior"> This is for starting a coroutine</param>
    public void GenerateASync(int[] IDs, MonoBehaviour MonoBehavior)
    {
        MonoBehavior.StartCoroutine(GenerateLevelRoutine(IDs));
    }

    private IEnumerator<YieldInstruction> GenerateLevelRoutine(int[] IDs)
    {
        for (int i = 0; i < IDs.Length; i++)
        {
            int ID = IDs[i];
            Room room = FindRoom(ID);
            CreateRoom(room);//.Disable();
            yield return new WaitForEndOfFrame();
        }
        if (onLevelLoadEnded != null)
            onLevelLoadEnded.Invoke();
    }

    private bool CheckPrefabs()
    {
        for (int i = 0; i < m_roomPrefabs.Length; i++)
        {
            if (!m_roomPrefabs[i].GetComponent<Room>())
                return false;
        }
        return true;
    }

    private Room CreateRoom(Room room)
    {
        Room newRoom = Object.Instantiate(room.gameObject).GetComponent<Room>();
        ConnectRoom(newRoom);
        return newRoom;
    }

    private void ConnectRoom(Room room)
    {

        // First room
        if (m_rooms.Count == 0)
        {
            room.transform.position = Vector3.zero;
            room.transform.rotation = Quaternion.Euler(0, 0, 0);
            m_rooms.Add(room);
            return;
        }

        RoomPath prevRoom = m_rooms[m_rooms.Count - 1].Path;
        room.transform.position = RoomPathUtility.GetLatestNodePosition(prevRoom);
        room.transform.rotation = Quaternion.Euler(0, 0, RoomPathUtility.GeLatestNodeAngle(prevRoom));
        m_rooms.Add(room);

    }

    private Room FindRoom(int ID)
    {
        for (int i = 0; i < m_roomPrefabs.Length; i++)
        {
            Room room = m_roomPrefabs[i].GetComponent<Room>();
            if (room)
                if (room.ID == ID)
                {
                    return m_roomPrefabs[i];
                }
        }
        throw new System.Exception("Couldn't find Room with ID " + ID);
    }
}