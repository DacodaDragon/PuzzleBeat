using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnRoomChangeDelegate(int roomNumber);

public class RoomManager : MonoBehaviour
{
    OnRoomChangeDelegate onRoomChange;

    public event OnRoomChangeDelegate OnRoomChange
    { add { onRoomChange += value; } remove { onRoomChange -= onRoomChange; } }

    Room[] m_rooms;
    int m_currentRoom = -1;

    public void AddRooms(IEnumerable<Room> rooms)
    {
        m_rooms = rooms.ToArray(); 
    }

    public Transform2DParams GetPositionInRoom(float Beat)
    {
        int i = 0;
        while (Beat > m_rooms[i].BeatLength)
        {
            Beat -= m_rooms[i].BeatLength;
            i++;
        }

        if (i != m_currentRoom)
        {
            // Notify current room
            onRoomChange?.Invoke(i);

            if (i - 1 >= 0)
            {
                if (!m_rooms[i - 1].IsOpen)
                {
                    GameObject.Find("_MusicPlayer").GetComponent<PitchController>().PlayCurve();
                    m_rooms[i - 1].Open();
                }
            }

            ActivateRoomsection(i);
            m_currentRoom = i;
        }

        return m_rooms[i].GetPosition(Beat);
    }

    private void ActivateRoomsection(int roomNumber)
    {
        Debug.Log("Room: " + roomNumber);

        // Failsafe
        // Deactivate all rooms if we we skip one or multiple rooms.
        if (Mathf.Abs(roomNumber - m_currentRoom) > 1)
            DeactivateAllRooms();

        // Disable the room before the room we leave
        if (roomNumber - 2 >= 0)
            m_rooms[roomNumber - 2].Disable();

        // Activate the room behind us
        if (roomNumber - 1 >= 0)
            m_rooms[roomNumber - 1].Enable();

        // Activate the room we are in
        m_rooms[roomNumber].Enable();

        // Activate the room in front of us
        if (roomNumber + 1 < m_rooms.Length)
            m_rooms[roomNumber+1].Enable();
    }

    private void DeactivateAllRooms()
    {
        for (int i = 0; i < m_rooms.Length; i++)
        {
            m_rooms[i].Disable();
        }
    }
}
