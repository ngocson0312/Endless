using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UDEV.EndlessGame
{
    public class PauseDialog : Dialog
    {
        public override void Show(bool isShow)
        {
            base.Show(isShow);

            Time.timeScale = 0f;
        }

        public override void Close()
        {
            Time.timeScale = 1f;

            base.Close();
        }

        public void BackHome()
        {
            Close();
            SceneManager.LoadScene(GameScene.MainMenu.ToString());
        }

        public void Replay()
        {
            Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
