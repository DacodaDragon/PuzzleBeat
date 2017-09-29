using UnityEngine;
using UnityEngine.UI;

namespace DDR
{
    public class UIBlinker : MonoBehaviour
    {
        [SerializeField] Color m_StaticColor;
        [SerializeField] Color m_BlitzColor;

        Color m_BeginColor;
        Color m_EndColor;

        float m_lerpValue = 0;

        MusicPlayer m_MusicPlayer;

        [SerializeField]
        float Count;

        public void Start()
        {
            Time time = new Time();
            m_BeginColor = m_EndColor = GetComponent<Image>().color = m_StaticColor;
            m_MusicPlayer = FindObjectOfType<MusicPlayer>();
            m_MusicPlayer.AddBeatListener(Blitz, 1);
        }

        public void Blitz()
        {
            // Bork bork I am a shork.. what?
            SetColor(m_BlitzColor);
            LerpToColor(m_StaticColor);
        }

        public void Update()
        {
            m_lerpValue += (Time.deltaTime * m_MusicPlayer.SongSpeed) / BPM.BeatToTime(1, m_MusicPlayer.Bpm);
            GetComponent<Image>().color = Color.Lerp(m_BeginColor, m_EndColor, m_lerpValue);
        }

        public void SetColor(Color color)
        {
            m_BeginColor = color;
        }

        public void LerpToColor(Color color)
        {

            m_EndColor = color;
            if (m_MusicPlayer.SongSpeed > 0)
                m_lerpValue = 0;
            else m_lerpValue = 1;
        }
    }
}
