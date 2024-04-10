using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.EndlessGame
{
    public class LevelItemUI : MonoBehaviour
    {
        public Text scoreRequireTxt;
        public Image lockThumb;
        public Image unlockThumb;
        public Button btn;

        public void UpdateUI(LevelItem level, int levelId)
        {
            if (level == null) return;

            bool isUnlocked = Pref.IsLevelUnlocked(levelId);

            if (lockThumb)
                lockThumb.gameObject.SetActive(!isUnlocked);

            if(unlockThumb)
                unlockThumb.gameObject.SetActive(isUnlocked);

            if (isUnlocked)
            {
                if (unlockThumb)
                    unlockThumb.sprite = level.unlockThumb;
            }else
            {
                if (scoreRequireTxt)
                    scoreRequireTxt.text = level.scoreRequire.ToString();

                if (lockThumb)
                    lockThumb.sprite = level.lockThumb;
            }
        }
    }
}
