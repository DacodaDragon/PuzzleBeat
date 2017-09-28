using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSolveDelegate(PuzzleElement element);

public class PuzzleElement : MonoBehaviour {

    private bool m_IsSolved;
    private OnSolveDelegate OnSolve;
    protected Room room;

    public void SetRoomReference(Room room)
    {
        this.room = room;
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
            if (OnSolve != null)
                OnSolve(this);
            m_IsSolved = true;
        }
    }
}
