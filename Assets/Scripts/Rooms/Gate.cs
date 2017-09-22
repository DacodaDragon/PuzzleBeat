using UnityEngine;
using System;
using DDR;

public class Gate : PuzzleElement
{
    private int MaxSolveAmount = 0;
    private int CurrentSolveAmount = 0;

    public void Start()
    {
        OnSolveEvent += Open;
        FindObjectOfType<MusicPlayer>().OnWhole += Bounce;
    }

    private void Bounce()
    {
        // Bounce (for animation purposes)
    }

    // Its kind of ugle because we hook it in the
    // puzzleelements's onSolve event.. I'll patch
    // this up some other time, right now it just
    // needs to work.
    private void Open(PuzzleElement element)
    {
        // [TODO] Open Gate
        // Open Gate Animation
        // Disable life sequence thing
    }

    public void SolveReciever(PuzzleElement element)
    {
        CurrentSolveAmount += 1;
        if (CurrentSolveAmount == MaxSolveAmount)
            Solve();
    }
}