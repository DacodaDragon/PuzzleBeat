using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace DDR
{
    public delegate void OnBeatDelegate();
    public delegate void OnSongBeginDelegate();
    public delegate void OnSongEndDelegate();
    public delegate void OnSongPause();
    public delegate void OnSongResume();

    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource m_AudioSource;
        private AudioMixer  m_AudioMixer;

        [SerializeField] private float m_time;
        [SerializeField] private float m_bmp;
        [SerializeField] private float m_timeOffset;
        [SerializeField] private float m_SpeedMultiplier;
        [SerializeField] private bool  m_timing;

        private float m_PreviousSpeed;
        private bool SpeedChanged       { get { return m_PreviousSpeed == GetSongSpeed(); } }
        public float SpeedMultiplier    { get { return m_SpeedMultiplier; } }

        // This needs to be done differently. IDEA: Make a class that handles 
        // =======================================================================================================================
        public OnBeatDelegate m_OnWholeBeatsEvent;
        public OnBeatDelegate m_OnHalveBeatsEvent;
        public OnBeatDelegate m_OnThirdBeatsEvent;
        public OnBeatDelegate m_OnFourthBeatsEvent;

        public event OnBeatDelegate OnWholeBeats  { add { m_OnWholeBeatsEvent  += value; } remove { m_OnWholeBeatsEvent  -= value; } }
        public event OnBeatDelegate OnHalveBeats  { add { m_OnHalveBeatsEvent  += value; } remove { m_OnHalveBeatsEvent  -= value; } }
        public event OnBeatDelegate OnThirdBeats  { add { m_OnThirdBeatsEvent  += value; } remove { m_OnThirdBeatsEvent  -= value; } }
        public event OnBeatDelegate OnFourthBeats { add { m_OnFourthBeatsEvent += value; } remove { m_OnFourthBeatsEvent -= value; } }

        private float m_NextWhole;
        private float m_NextHalve;
        private float m_NextThird;
        private float m_NextFourth;
        // =======================================================================================================================

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioMixer = m_AudioSource.outputAudioMixerGroup.audioMixer;
            OnWholeBeats += Syncronize;
            m_PreviousSpeed = GetSongSpeed();
        }

        public void Update()
        {
            UpdateTime();

            // Update some timers.
            if (GetSongSpeed() > 0)
            {
                UpdateTimer(m_OnWholeBeatsEvent, ref m_NextWhole, 1);
                UpdateTimer(m_OnHalveBeatsEvent, ref m_NextHalve, 0.5f);
                UpdateTimer(m_OnThirdBeatsEvent, ref m_NextThird, 1f / 3f);
                UpdateTimer(m_OnFourthBeatsEvent, ref m_NextFourth, 0.25f);
            }
            else
            {
                ReveseUpdateTimer(m_OnWholeBeatsEvent, ref m_NextWhole, 1);
                ReveseUpdateTimer(m_OnHalveBeatsEvent, ref m_NextHalve, 0.5f);
                ReveseUpdateTimer(m_OnThirdBeatsEvent, ref m_NextThird, 1f / 3f);
                ReveseUpdateTimer(m_OnFourthBeatsEvent, ref m_NextFourth, 0.25f);
            }

            m_PreviousSpeed = GetSongSpeed();
        }

        private void UpdateTime()
        {
            m_time += Time.deltaTime * GetSongSpeed();
        }

        private void UpdateTimer(OnBeatDelegate beatDelegate, ref float Timer, float step)
        {
            if (GetTimeInBeat() > Timer)
            {
                Timer += step;
                if (SpeedChanged)
                    if (beatDelegate != null)
                        beatDelegate.Invoke();
            }
        }

        private void ReveseUpdateTimer(OnBeatDelegate beatDelegate, ref float Timer, float step)
        {
            if (GetTimeInBeat() < Timer)
            {
                Timer -= step;
                if (SpeedChanged)
                    if (beatDelegate != null)
                        beatDelegate.Invoke();
            }
        }

        public float GetTime()
        {
            return m_time + m_timeOffset;
        }

        public float GetTimeInBeat()
        {
            return BPM.TimeToBeat(m_time + m_timeOffset, m_bmp);
        }

        public float GetSongSpeed()
        {
            return m_AudioSource.pitch * GetMixerPitch();
        }

        public float GetBmp()
        {
            return m_bmp;
        }

        public void Play()
        {
            m_AudioSource.Play();
            m_time = m_AudioSource.time;
            m_timing = true;
        }

        public void Syncronize()
        {
            m_time = m_AudioSource.time;
        }

        private float GetMixerPitch()
        {

            if (m_AudioMixer)
            {

                float pitch;
                m_AudioMixer.GetFloat("Pitch", out pitch);

                // Pitch from audio mixers are represented as 0%-300%
                return pitch; // Convert to 0-3 for accurate timescaling
            }
            else return 1; // play at normal speed if audiosource is not connected to a mixer.
        }
    }
}