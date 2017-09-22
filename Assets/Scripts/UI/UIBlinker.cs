using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DDR
{
    public class UIBlinker : MonoBehaviour
    {
        public Color StaticColor;
        public Color BlitzColor;

        Color From;
        Color To;
        Color Current;

        float lerpValue = 0;

        MusicPlayer musicPlayer;

        bool isPressed;

        enum Count
        {
            whole,
            halve,
            third,
            fourth
        }

        [SerializeField]
        Count count;

        public void Start()
        {
            From = To = Current = GetComponent<Image>().color = StaticColor;
            musicPlayer = FindObjectOfType<MusicPlayer>();
            switch (count)
            {
                case Count.whole: musicPlayer.AddBeatListener(Blitz, 1f); break;
                case Count.halve: musicPlayer.AddBeatListener(Blitz, 0.5f); break;
                case Count.third: musicPlayer.AddBeatListener(Blitz, 1f / 3f); break;
                case Count.fourth: musicPlayer.AddBeatListener(Blitz, 0.25f); break;
            }

        }

        public void Blitz()
        {
            // Bork bork I am a shork.. what?

            SetColor(BlitzColor);
            LerpToColor(StaticColor);
        }

        public void Update()
        {
            lerpValue += (Time.deltaTime * musicPlayer.SongSpeed) / BPM.BeatToTime(1, musicPlayer.Bpm);
            GetComponent<Image>().color = Color.Lerp(From, To, lerpValue);
        }

        public void SetColor(Color color)
        {
            From = color;
            Current = color;
        }

        public void LerpToColor(Color color)
        {
            From = Current;
            To = color;
            if (musicPlayer.SongSpeed > 0)
                lerpValue = 0;
            else lerpValue = 1;
        }

        public void OnRelease()
        {
            isPressed = false;
            SetColor(Current);
            LerpToColor(StaticColor);
        }
    }
}
