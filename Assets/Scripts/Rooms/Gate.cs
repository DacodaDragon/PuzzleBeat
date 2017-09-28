using UnityEngine;
using System;
using DDR;

public class Gate : MonoBehaviour
{
    Animator animator;
    bool isOpen = false;
    public bool IsOpen { get { return isOpen; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", false);
        FindObjectOfType<MusicPlayer>().AddBeatListener(() => { animator.SetTrigger("Bounce"); }, 1);
    }

    public void Open()
    {
        // Don't open if we are already open..
        if (IsOpen)
            return;

        animator.SetBool("Open", true);
        animator.SetTrigger("Opening");
        isOpen = true;
    }
}