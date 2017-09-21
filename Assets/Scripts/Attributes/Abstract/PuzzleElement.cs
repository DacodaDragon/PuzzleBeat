using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSolve(PuzzleElement element);

public class PuzzleElement : MonoBehaviour {

    public OnSolve onSolve;

    public void Solve()
    {
        if (onSolve != null)
            onSolve(this);
    }
}
