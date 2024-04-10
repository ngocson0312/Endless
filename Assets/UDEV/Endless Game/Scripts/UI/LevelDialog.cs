using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UDEV.EndlessGame
{
    public class LevelDialog : Dialog, ICompChk
    {
        public Transform gridRoot;
        public LevelItemUI itemUIPb;
        public Text bestScoreTxt;

        public bool IsComponentsNull()
        {
            bool checking = LevelManager.Ins == null || gridRoot == null || itemUIPb == null;

            if (checking)
                Debug.LogError("Some component is null. Please check!!!.");

            return checking;
        }

        public override void Show(bool isShow)
        {
            base.Show(isShow);

            if (bestScoreTxt)
                bestScoreTxt.text = $"TOP SCORE: {Pref.bestScore}";

            UpdateUI();
        }

        private void UpdateUI()
        {
            var levels = LevelManager.Ins.levels;

            if (levels == null || levels.Length <= 0 || IsComponentsNull()) return;

            Helper.ClearChilds(gridRoot);

            for (int i = 0; i < levels.Length; i++)
            {
                int levelId = i;

                var level = levels[i];

                if (Pref.bestScore >= level.scoreRequire)
                    Pref.SetLevelUnlocked(levelId, true);

                if(level == null) continue;

                var levelUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);

                levelUIClone.transform.SetParent(gridRoot);

                levelUIClone.transform.localPosition = Vector3.zero;//(0, 0, 0)

                levelUIClone.transform.localScale = Vector3.one; //(1, 1 , 1);

                levelUIClone.UpdateUI(level, levelId);

                if (levelUIClone.btn)
                {
                    levelUIClone.btn.onClick.RemoveAllListeners();
                    levelUIClone.btn.onClick.AddListener(() => LevelClickEvent(level, levelId));
                }
            }
        }

        private void LevelClickEvent(LevelItem level, int levelId)
        {
            if (level == null) return;

            bool isUnlocked = Pref.IsLevelUnlocked(levelId);

            if (isUnlocked)
            {
                Pref.CurLevelId = levelId;

                SceneManager.LoadScene(GameScene.GamePlay.ToString());

                AudioController.Ins.StopPlayMusic();
            }
        }
    }
}
