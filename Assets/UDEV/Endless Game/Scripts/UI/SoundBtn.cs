using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.EndlessGame
{
    public class SoundBtn : MonoBehaviour
    {
        private Button m_btn;

        private void Awake()
        {
            m_btn = GetComponent<Button>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (m_btn == null) return;

            //m_btn.onClick.RemoveAllListeners();
            m_btn.onClick.AddListener(() => PlaySound());
        }

        private void PlaySound()
        {
            if (AudioController.Ins == null) return;

            AudioController.Ins.PlaySound(AudioController.Ins.btnClick);
        }
    }
}
