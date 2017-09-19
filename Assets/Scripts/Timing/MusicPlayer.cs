using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DDR
{
    public delegate void OnBeatDelegate();

    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource m_AudioSource;
        [SerializeField] private float m_time;
        [SerializeField] private float m_bmp;
        [SerializeField] private float m_preDelay;
        [SerializeField] private float m_SpeedMultiplier;

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
        }

        public void Update()
        {
            m_time = m_AudioSource.time;
            UpdateTimer(OnWhole, ref NextWhole, 1);
            UpdateTimer(OnHalve, ref NextHalve, 0.5f);
            UpdateTimer(OnThird, ref NextThird, 1f / 3f);
            UpdateTimer(OnFourth, ref NextFourth, 0.25f);
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
            return m_time - m_preDelay;
        }

        public float GetTimeInBeat()
        {
            return BPM.TimeToBeat(m_time - m_preDelay, m_bmp);
        }

        public float GetBmp()
        {
            return m_bmp;
        }

        public void Play()
        {
            m_AudioSource.Play();
        }
    }
}