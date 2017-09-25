using UnityEngine;
using UnityEngine.Audio;
using DDR.TimeManagement;

namespace DDR
{
    public delegate void OnBeatDelegate();
    public delegate void OnSongBeginDelegate();
    public delegate void OnSongEndDelegate();
    public delegate void OnSongPauseDelegate();
    public delegate void OnSongResumeDelegate();

    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource m_AudioSource;
        private AudioMixer m_AudioMixer;

        [SerializeField] private float m_time;
        [SerializeField] private float m_timeInBeats;
        [SerializeField] private float m_deltaTime;
        [SerializeField] private float m_bmp;
        [SerializeField] private float m_timeOffset;
        [SerializeField] private bool  m_playing;

        public float Time { get { return m_time; } }
        public float TimeInBeats { get { return BPM.TimeToBeat(m_time - m_timeOffset,m_bmp); } }
        public float SongSpeed { get { return m_AudioSource.pitch * GetMixerPitch(); } }
        public float Bpm { get { return m_bmp; } }
        public float DeltaTime { get { return m_deltaTime; } }

        private float m_previousSpeed;
        private bool SpeedChanged { get { return m_previousSpeed != SongSpeed; } }

        // Technically not something we want here (Perhaps we need a Time/TimeRhythm Class?)
        BeatObserverManager beatObserverManager = new BeatObserverManager();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioMixer = m_AudioSource.outputAudioMixerGroup.audioMixer;
            m_previousSpeed = SongSpeed;
        }

        public void Update()
        {
            if (m_playing)
            {
                UpdateTime();
                beatObserverManager.Update(TimeInBeats);
            }
            m_previousSpeed = SongSpeed;
        }

        private void UpdateTime()
        {
            float newTime = m_time + (UnityEngine.Time.deltaTime * SongSpeed);
            m_deltaTime = newTime - m_time;
            m_time = newTime;
        }

        public void Play()
        {
            m_AudioSource.Play();
            m_time = m_AudioSource.time;
            m_playing = true;
        }

        public void Pause()
        {
            m_AudioSource.Pause();
            m_playing = false;
        }

        public void UnPause()
        {
            m_AudioSource.UnPause();
            m_playing = true;
            Syncronize();
        }

        public void Stop()
        {
            m_playing = false;
            m_AudioSource.Stop();
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

        public void AddBeatListener(OnBeatDelegate function, float measure)
        {
            beatObserverManager.SubScribe(function, measure);
        }

        public void RemoveBeatListener(OnBeatDelegate function, float measure)
        {
            beatObserverManager.UnSubscribe(function, measure);
        }
    }
}