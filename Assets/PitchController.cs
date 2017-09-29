using UnityEngine;

public class PitchController : MonoBehaviour {

    [SerializeField]
    AudioSource m_AudioSource;
    [SerializeField]
    DDR.MusicPlayer m_MusicPlayer;
    [SerializeField]
    AnimationCurve Curve;
    [SerializeField]
    float m_Time;

    bool m_IsPlaying = false;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayCurve();
        }

        if (m_IsPlaying)
        {
            m_Time += Time.deltaTime * m_MusicPlayer.MixerSpeed;
            m_AudioSource.pitch = Curve.Evaluate(m_Time);
            if (m_Time > 2) m_IsPlaying = false;
        }
    }

    public void PlayCurve()
    {
        m_Time = 0;
        m_IsPlaying = true;
    }
}
