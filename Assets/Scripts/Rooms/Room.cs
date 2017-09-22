using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomPath))]
public class Room : MonoBehaviour
{
    [SerializeField] private string m_IDString;
    [SerializeField] private int m_IDNumber;
    [SerializeField] private int m_LengthInBeats;

    private RoomPath m_roomPath;
    private Gate m_gate;

    public int ID { get { return m_IDNumber; } }
    public int BeatLength { get { return m_LengthInBeats; } set { m_LengthInBeats = value; } }
    public RoomPath Path { get { return m_roomPath; } }
    public PathNode EndNode { get { return m_roomPath.EndNode; } }
    public PathNode StartNode { get { return m_roomPath.StartNode; } }

    void Awake()
    {
        m_roomPath = GetComponent<RoomPath>();
        m_gate = GetComponentInChildren<Gate>();
        HookPuzzleElements(GetComponentsInChildren<PuzzleElement>(true));
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public Transform2DParams GetPosition(float beat)
    {
        Vector2[] s = RoomPathUtility.ConvertToVectors(m_roomPath);
        float SegmentCount = s.Length - 1;
        float SegmentLength = BeatLength / SegmentCount;

        int segment = 0;
        float DistanceToTravelInSegment = beat;
        while (DistanceToTravelInSegment > SegmentLength)
        {
            DistanceToTravelInSegment -= SegmentLength;
            segment++;
        }

        Vector2 position = Vector2.Lerp(s[segment], s[segment + 1], DistanceToTravelInSegment / SegmentLength);
        Quaternion rotation = Quaternion.Euler(0, 0, Vector2DMath.GetAngleBetween(s[segment], s[segment + 1]));
        return new Transform2DParams(position, rotation);
    }

    private void HookPuzzleElements(PuzzleElement[] elements)
    {
        // [TODO] Hook Puzzle elements to gate?
    }
}