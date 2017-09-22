using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DDR;

public class PathTraveler: MonoBehaviour {

    [SerializeField]
    RoomManager m_roomManager;
    [SerializeField]
    MusicPlayer m_musicplayer;

    bool Traveling = false;

	void Start () {
        if (!m_roomManager)
            m_roomManager = FindObjectOfType<RoomManager>();
        if (!m_musicplayer)
            m_musicplayer = FindObjectOfType<MusicPlayer>();
    }

	void Update () {
        if (Traveling)
        {
            Transform2DParams WorldPosition = m_roomManager.GetPositionInRoom(
                m_musicplayer.TimeInBeats);
            transform.position = WorldPosition.position;
            transform.rotation = WorldPosition.rotation;
        }
    }

    public void StartTraveling()
    {
        Traveling = true;
    }
}
