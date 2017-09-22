using System.Collections;
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
        private AudioMixer m_AudioMixer;

        [SerializeField] private float m_time;
        [SerializeField] private float m_bmp;
        [SerializeField] private float m_timeOffset;
        [SerializeField] private float m_SpeedMultiplier;
        [SerializeField] private bool m_timing;

        public float Time { get { return m_time; } }
        public float TimeInBeats { get { return BPM.TimeToBeat(m_time + m_timeOffset, m_bmp); } }
        public float SongSpeed { get { return m_AudioSource.pitch * GetMixerPitch(); } }
        public float Bpm { get { return m_bmp; } }

        private float m_previousSpeed;
        private bool SpeedChanged { get { return m_previousSpeed != SongSpeed; } }
        public float SpeedMultiplier { get { return m_SpeedMultiplier; } }
        
        // This needs to be done differently. IDEA: Make a class that handles 
        // =======================================================================================================================
        private OnBeatDelegate m_OnWholeBeatsEvent;
        private OnBeatDelegate m_OnHalveBeatsEvent;
        private OnBeatDelegate m_OnThirdBeatsEvent;
        private OnBeatDelegate m_OnFourthBeatsEvent;

        public event OnBeatDelegate OnWholeBeats { add { m_OnWholeBeatsEvent += value; } remove { m_OnWholeBeatsEvent -= value; } }
        public event OnBeatDelegate OnHalveBeats { add { m_OnHalveBeatsEvent += value; } remove { m_OnHalveBeatsEvent -= value; } }
        public event OnBeatDelegate OnThirdBeats { add { m_OnThirdBeatsEvent += value; } remove { m_OnThirdBeatsEvent -= value; } }
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
            m_previousSpeed = SongSpeed;
        }

        public void Update()
        {
            UpdateTime();
            // Update some timers.
            if (SongSpeed > 0)
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

            m_previousSpeed = SongSpeed;
        }

        private void UpdateTime()
        {
            m_time += UnityEngine.Time.deltaTime * SongSpeed;
        }

        private void UpdateTimer(OnBeatDelegate beatDelegate, ref float timer, float step)
        {
            if (TimeInBeats > timer)
            {
                timer += step;
                if (beatDelegate != null && !SpeedChanged)
                    beatDelegate.Invoke();
            }
        }

        private void ReveseUpdateTimer(OnBeatDelegate beatDelegate, ref float timer, float step)
        {
            if (TimeInBeats < timer)
            {
                timer -= step;
                if (beatDelegate != null && !SpeedChanged)
                    beatDelegate.Invoke();
            }
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

        /// <param name="Pitch">float value from -3 to 3 </param>
        public void SetPitch(float pitch)
        {
            m_AudioSource.pitch = pitch;
        }

        private float GetMixerPitch()
        {
            if (m_AudioMixer)
            {
                float pitch;
                if (m_AudioMixer.GetFloat("Pitch", out pitch)) return pitch;
            }
            // play at normal speed if no mixer is connected or "Pitch" parameter isn't exposed / doesn't exist
            return 1;
        }
    }
}
