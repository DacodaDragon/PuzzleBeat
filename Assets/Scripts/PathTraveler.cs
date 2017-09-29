using UnityEngine;
using DDR;

public class PathTraveler: MonoBehaviour {

    [SerializeField]
    private RoomManager m_roomManager;
    [SerializeField]
    private MusicPlayer m_musicplayer;

    private float m_TargetRotation;

    private bool Traveling = false;

	void Start ()
    {
        if (!m_roomManager)
            m_roomManager = FindObjectOfType<RoomManager>();
        if (!m_musicplayer)
            m_musicplayer = FindObjectOfType<MusicPlayer>();
    }

	void Update ()
    {
        if (Traveling)
        {
            Transform2DParams WorldPosition = m_roomManager.GetPositionInRoom(m_musicplayer.TimeInBeats);
            transform.position = WorldPosition.position;
            m_TargetRotation = WorldPosition.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, m_TargetRotation, 0.2f * m_musicplayer.MixerSpeed));
        }
    }

    public void StartTraveling()
    {
        Traveling = true;
    }
}
