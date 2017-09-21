using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DDR
{
    public class UIBlinker: MonoBehaviour
    {
        public Color TapColor;
        public Color HoldColor;
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
                case Count.whole: musicPlayer.OnWhole += Blitz; break;
                case Count.halve: musicPlayer.OnHalve += Blitz; break;
                case Count.third: musicPlayer.OnThird += Blitz; break;
                case Count.fourth: musicPlayer.OnFourth += Blitz; break;
            }
            
        }

        public void Blitz()
        {
            if (!isPressed)
            {
                // Bork bork I am a shork.. what?
                SetColor(BlitzColor);
                LerpToColor(StaticColor);
            }
            else
            {
                OnTap();
            }
        }

        public void Update()
        {
            lerpValue += Mathf.Min(1, Time.deltaTime * BPM.BeatToTime(4*musicPlayer.GetSongSpeed(),musicPlayer.GetBmp()));
            Current = Color.Lerp(From, To, lerpValue);
            GetComponent<Image>().color = Current;
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
            lerpValue = 0;
        }

        public void OnTap()
        {
            isPressed = true;
            SetColor(TapColor);
            LerpToColor(HoldColor);
        }

        public void OnRelease()
        {
            isPressed = false;
            SetColor(Current);
            LerpToColor(StaticColor);
        }
    }
}
