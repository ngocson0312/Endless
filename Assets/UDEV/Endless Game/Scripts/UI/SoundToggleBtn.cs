using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.EndlessGame
{
    public class SoundToggleBtn : ToggleBtn
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            m_isOn = Pref.GetBool(GamePref.IsSoundOn.ToString(), true);
            UpdateSprite();
        }

        public override void ClickEvent()
        {
            m_isOn = !m_isOn;

            Pref.SetBool(GamePref.IsSoundOn.ToString(), m_isOn);

            if (AudioController.Ins == null) return;

            AudioController.Ins.sfxAus.mute = !m_isOn;
        }
    }
}
