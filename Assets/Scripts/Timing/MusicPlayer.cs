using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace DDR
{
    public delegate void OnBeatDelegate();

    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource m_AudioSource;
        private AudioMixer m_AudioMixer;
        [SerializeField] private float m_time;
        [SerializeField] private float m_bmp;
        [SerializeField] private float m_timeOffset;
        [SerializeField] private float m_SpeedMultiplier;
        [SerializeField] private bool  m_timing;

        public OnBeatDelegate OnWhole;
        public OnBeatDelegate OnHalve;
        public OnBeatDelegate OnThird;
        public OnBeatDelegate OnFourth;

        private float NextWhole;
        private float NextHalve;
        private float NextThird;
        private float NextFourth;

        private float[] spectrumData;
        public float SpeedMultiplier { get { return m_SpeedMultiplier; } }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioMixer = m_AudioSource.outputAudioMixerGroup.audioMixer;
            OnWhole += Syncronize;
        }

        public void Update()
        {
            UpdateTime();

            // Update some timers.
            UpdateTimer(OnWhole, ref NextWhole,    1);
            UpdateTimer(OnHalve, ref NextHalve,    0.5f);
            UpdateTimer(OnThird, ref NextThird,    1f / 3f);
            UpdateTimer(OnFourth, ref NextFourth,  0.25f);
        }

        private void UpdateTime()
        {
            m_time += Time.deltaTime * GetSongSpeed();
        }

        public void UpdateTimer(OnBeatDelegate beatDelegate, ref float Timer, float step)
        {
            if (GetTimeInBeat() > Timer)
            {
                Timer += step;
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