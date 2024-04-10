using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.EndlessGame
{
    public class GUIManager : MonoBehaviour
    {
        public static GUIManager Ins;
        public GameObject homeGUI;
        public GameObject gameGUI;
        public Dialog gameoverDialog;
        public Text scoreCountingTxt;
        public Image gameoverImgTxt;

        private void Awake()
        {
            Ins = this;
        }

        public void ShowGameGUI(bool isShow)
        {
            if (gameGUI)
                gameGUI.SetActive(isShow);

            if (homeGUI)
                homeGUI.SetActive(!isShow);
        }

        public void UpdateScore(int score)
        {
            if (scoreCountingTxt)
                scoreCountingTxt.text = score.ToString();
        }

        private IEnumerator ShowGameoverImgTxtCo()
        {
            if (gameoverImgTxt)
                gameoverImgTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            if (gameoverImgTxt)
                gameoverImgTxt.gameObject.SetActive(false);
            if (gameoverDialog)
                gameoverDialog.Show(true);
        }

        public void ShowGameoverImgTxt()
        {
            StartCoroutine(ShowGameoverImgTxtCo());
        }
    }
}
