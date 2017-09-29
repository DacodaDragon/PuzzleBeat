using UnityEngine;

[RequireComponent(typeof(RoomPath))]
public class Room : MonoBehaviour
{
    [SerializeField] private string m_IDString;
    [SerializeField] private int m_IDNumber;
    [SerializeField] private int m_LengthInBeats;
    [SerializeField] private float m_TimeOffsetInBeats;
    [SerializeField] private int m_PuzzleAmount;
    [SerializeField] private int m_PuzzlesToSolve;
    [SerializeField] private int m_PuzzleToSolveSlack;

    private RoomPath m_roomPath;
    private Gate m_gate;
    bool m_isOpenWithoutGate = false;

    public int ID { get { return m_IDNumber; } }
    public int BeatLength { get { return m_LengthInBeats; } set { m_LengthInBeats = value; } }
    public RoomPath Path { get { return m_roomPath; } }
    public PathNode EndNode { get { return m_roomPath.EndNode; } }
    public PathNode StartNode { get { return m_roomPath.StartNode; } }
    public bool IsOpen { get { if (m_gate) return m_gate.IsOpen; else return m_isOpenWithoutGate; } }
    public float TimeOffsetInBeats { get { return m_TimeOffsetInBeats; } set { m_TimeOffsetInBeats = value; } }

    public void Open()
    {
        if (m_gate)
            m_gate.Open();
        else m_isOpenWithoutGate = true;
    }

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
        Vector2[] VectorPath = RoomPathUtility.ConvertToVectors(m_roomPath);
        float SegmentCount = VectorPath.Length - 1;
        float SegmentLength = BeatLength / SegmentCount;

        int segment = 0;
        float DistanceToTravelInSegment = beat;
        while (DistanceToTravelInSegment > SegmentLength)
        {
            DistanceToTravelInSegment -= SegmentLength;
            segment++;
        }

        Vector2 position = Vector2.Lerp(VectorPath[segment], VectorPath[segment + 1], DistanceToTravelInSegment / SegmentLength);
        Quaternion rotation = Quaternion.Euler(0, 0, Vector2DMath.GetAngleBetween(VectorPath[segment], VectorPath[segment + 1]));
        return new Transform2DParams(position, rotation);
    }

    private void HookPuzzleElements(PuzzleElement[] elements)
    {
        m_PuzzleAmount = elements.Length;
        m_PuzzlesToSolve = elements.Length;
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetRoomReference(this);
            elements[i].SetMusicplayerReference(FindObjectOfType<DDR.MusicPlayer>());
            elements[i].OnSolveEvent += (PuzzleElement element) =>
            {
                m_PuzzlesToSolve -= 1;
                if (m_PuzzlesToSolve < m_PuzzleToSolveSlack)
                {
                    m_gate.Open();
                }
            };
        }
    }
}