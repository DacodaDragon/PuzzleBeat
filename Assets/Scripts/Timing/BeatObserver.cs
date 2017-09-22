namespace DDR
{
    public class BeatObserver
    {
        private OnBeatDelegate m_OnTrigger;
        private float m_nextMeasure;
        private float m_previousMeasure;
        private float m_measure;

        public event OnBeatDelegate OnTrigger { add { m_OnTrigger += value; } remove { m_OnTrigger -= value; }}

        public float Measure { get { return m_measure; } }

        public BeatObserver(float beatMeasure, float currentTime = 0)
        {
            m_measure         = beatMeasure;
            m_nextMeasure     = currentTime + beatMeasure;
            m_previousMeasure = currentTime - beatMeasure;
        }

        public void Update(float timeInBeats)
        {
            // Time forwards!
            if (timeInBeats > m_nextMeasure)
            {
                m_nextMeasure     += m_measure;
                m_previousMeasure += m_measure;
                Trigger();
            }

            // Time backwards!
            if (timeInBeats < m_previousMeasure)
            {
                m_nextMeasure     += m_measure;
                m_previousMeasure += m_measure;
                Trigger();
            }
        }

        private void Trigger()
        {
            if (m_OnTrigger != null)
                m_OnTrigger.Invoke();
        }
    }
}