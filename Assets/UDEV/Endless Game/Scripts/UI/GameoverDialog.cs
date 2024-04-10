using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UDEV.EndlessGame
{
    public class GameoverDialog : Dialog
    {
        public Text scoreTxt;
        public Text bestScoreTxt;

        public override void Show(bool isShow)
        {
            base.Show(isShow);

            if (scoreTxt)
                scoreTxt.text = GameManager.Ins.Score.ToString();

            if (Pref.hasBestScore)
            {
                if (bestScoreTxt)
                    bestScoreTxt.text = $"NEW BEST: {Pref.bestScore}";

                AudioController.Ins.PlaySound(AudioController.Ins.bestScore);
            }
            else
            {
                if (bestScoreTxt)
                    bestScoreTxt.text = $"TOP SCORE: {Pref.bestScore}";
            }
        }

        public void BackHome()
        {
            Close();
            SceneManager.LoadScene(GameScene.MainMenu.ToString());
        }

        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
