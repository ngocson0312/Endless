using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public static class Pref
    {
        public static bool hasBestScore;

        public static int bestScore
        {
            set
            {
                int oldScore = PlayerPrefs.GetInt(GamePref.BestScore.ToString(), 0);

                if (oldScore < value)
                {
                    hasBestScore = true;
                    PlayerPrefs.SetInt(GamePref.BestScore.ToString(), value);
                }
                else
                    hasBestScore = false;
            }

            get => PlayerPrefs.GetInt(GamePref.BestScore.ToString());
        }

        public static int CurLevelId
        {
            set => PlayerPrefs.SetInt(GamePref.CurLevelId.ToString(), value);
            get => PlayerPrefs.GetInt(GamePref.CurLevelId.ToString(), 0);
        }

        public static void SetLevelUnlocked(int levelId, bool unlocked)
        {
            SetBool(GamePref.LevelUnlocked.ToString() + levelId, unlocked);
        }

        public static bool IsLevelUnlocked(int levelId)
        {
            return GetBool(GamePref.LevelUnlocked.ToString() + levelId);
        }
        
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.HasKey(key) ?
                PlayerPrefs.GetInt(key) == 1 ? true : false : defaultValue;
        }
    }
}
