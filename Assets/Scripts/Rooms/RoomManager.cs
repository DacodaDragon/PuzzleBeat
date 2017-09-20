using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
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
            ActivateRoomsection(i);
            m_currentRoom = i;
        }

        return m_rooms[i].GetPosition(Beat);
    }

    private void ActivateRoomsection(int RoomNumber)
    {
        Debug.Log("Room: " + RoomNumber);

        if (RoomNumber - 2 >= 0)
            m_rooms[RoomNumber - 2].Disable();
        if (RoomNumber - 1 >= 0)
            m_rooms[RoomNumber - 1].Enable();

        m_rooms[RoomNumber].Enable();

        if (RoomNumber + 1 < m_rooms.Length)
            m_rooms[RoomNumber+1].Enable();
    }
}
