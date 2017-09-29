using UnityEngine;
using DDR;

public class Gate : MonoBehaviour
{
    Animator m_Animator;
    bool m_isOpen = false;
    public bool IsOpen { get { return m_isOpen; } }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("Open", false);
        FindObjectOfType<MusicPlayer>().AddBeatListener(() => { m_Animator.SetTrigger("Bounce"); }, 1);
    }

    public void Open()
    {
        // Don't open if we are already open..
        if (IsOpen)
            return;

        m_Animator.SetBool("Open", true);
        m_Animator.SetTrigger("Opening");
        m_isOpen = true;
    }
}