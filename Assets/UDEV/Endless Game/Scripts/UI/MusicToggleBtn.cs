using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.EndlessGame
{
    public class MusicToggleBtn : ToggleBtn
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            m_isOn = Pref.GetBool(GamePref.IsMusicOn.ToString(), true);
            UpdateSprite();
        }

        public override void ClickEvent()
        {
            m_isOn = !m_isOn;

            Pref.SetBool(GamePref.IsMusicOn.ToString(), m_isOn);

            if (AudioController.Ins == null) return;

            AudioController.Ins.musicAus.mute = !m_isOn;
        }
    }
}
