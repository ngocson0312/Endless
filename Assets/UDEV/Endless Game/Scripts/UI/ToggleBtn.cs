using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.EndlessGame
{
    public class ToggleBtn : MonoBehaviour
    {
        public Sprite on, off;
        private Button m_btn;
        protected bool m_isOn;

        private void Awake()
        {
            m_btn = GetComponent<Button>();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (m_btn == null) return;
            
            m_btn.onClick.AddListener(() => BtnClickEvent());
        }

        private void BtnClickEvent()
        {
            ClickEvent();
            UpdateSprite();
        }

        protected void UpdateSprite()
        {
            Image img = m_btn.GetComponent<Image>();
            if (img)
            {
                img.sprite = m_isOn ? on : off;
            }
        }

        public virtual void ClickEvent()
        {
            
        }
    }
}
