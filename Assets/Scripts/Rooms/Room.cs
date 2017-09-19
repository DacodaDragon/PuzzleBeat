﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomPath))]
public class Room : MonoBehaviour
{
    [SerializeField]
    private string IDString;
    [SerializeField]
    private int IDNumber; 
    [SerializeField]
    private int m_LengthInBeats;

    private RoomPath m_roomPath;
    public RoomPath Path { get { return m_roomPath; } }

    void Awake()
    {
        m_roomPath = GetComponent<RoomPath>();
    }

    public int ID { get { return IDNumber; } }

    public int BeatLength { get { return m_LengthInBeats; } set { m_LengthInBeats = value; } }
    public PathNode EndNode { get { return m_roomPath.EndNode; } }
    public PathNode StartNode { get { return m_roomPath.StartNode; } }

    public void Enable()
    {
        gameObject.SetActive(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
