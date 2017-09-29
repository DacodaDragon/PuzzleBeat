using UnityEngine;
using DDR;

public delegate void OnSolveDelegate(PuzzleElement element);

public class PuzzleElement : MonoBehaviour {

    private bool            m_IsSolved;
    private OnSolveDelegate OnSolve;
    private Room            m_room;
    private MusicPlayer     m_musicPlayer;

    protected MusicPlayer musicPlayer { get { return m_musicPlayer; } }
    protected Room room { get { return m_room; } }

    public void SetRoomReference(Room room)
    {
        m_room = room;
    }

    public void SetMusicplayerReference(MusicPlayer musicPlayer)
    {
        m_musicPlayer = musicPlayer;
    }

    public event OnSolveDelegate OnSolveEvent
    {
        add { OnSolve += value; }
        remove { OnSolve -= value; }
    }

    public bool IsSolved { get { return m_IsSolved; } }

    // We can only solve this once
    protected void Solve()
    {
        if (!m_IsSolved)
        {
            OnSolve?.Invoke(this);
            m_IsSolved = true;
        }
    }
}
