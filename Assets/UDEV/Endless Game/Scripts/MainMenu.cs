using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class MainMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (AudioController.Ins == null) return;

            bool isMusicOn = Pref.GetBool(GamePref.IsMusicOn.ToString(), true);
            bool isSoundOn = Pref.GetBool(GamePref.IsSoundOn.ToString(), true);

            if (AudioController.Ins.musicAus)
                AudioController.Ins.musicAus.mute = !isMusicOn;

            if (AudioController.Ins.sfxAus)
                AudioController.Ins.sfxAus.mute = !isSoundOn;

            AudioController.Ins.PlayMusic(AudioController.Ins.menus);
        }
    }
}
