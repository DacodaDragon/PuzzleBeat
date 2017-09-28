using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class PuzzleTap : PuzzleElement
{
    /// Beattime in room
    /// to activate the
    /// puzzle element .-.
    [SerializeField]
    float m_BeatInRoom;

    /// Tap Visible
    [SerializeField]
    float m_BeatVisible;


    Animator animator;
    void Start ()
    {
        animator = GetComponent<Animator>();
        TouchListener listener = GetComponent<TouchListener>();
        
        DDR.MusicPlayer musicPlayer = GameObject.Find("_MusicPlayer").GetComponent<DDR.MusicPlayer>();
        
        listener.onTouchPress += () => 
        {
            animator.SetBool("IsActive", true);
            animator.SetBool("IsPressed", true);
        };

        listener.onTouchRelease += () => 
        {
            animator.SetBool("IsPressed", false);
            if (IsSolved == false)
                Solve();
        };

        musicPlayer.AddBeatListener
            (() => { animator.SetTrigger("Bounce"); }, 1);
    }

    void Update()
    {
        DDR.MusicPlayer musicPlayer = GameObject.Find("_MusicPlayer").GetComponent<DDR.MusicPlayer>();
        animator.speed = Mathf.Abs(musicPlayer.SongSpeed);
    }
}
