using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class LevelManager : MonoBehaviour, ISingleton
    {
        public static LevelManager Ins;
        public LevelItem[] levels;

        private void Awake()
        {
            MakeSingleton();
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        private void Init()
        {
            if (levels == null || levels.Length <= 0) return;

            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];

                if(level == null) continue;

                string levelDataKey = GamePref.LevelUnlocked.ToString() + i;

                if(i == 0)
                {
                    Pref.SetLevelUnlocked(i, true);
                }else
                {
                    if (!PlayerPrefs.HasKey(levelDataKey))
                    {
                        Pref.SetLevelUnlocked(i, false);
                    }
                }    
            }
        }

        public LevelItem GetLevel()
        {
            if(levels != null && levels.Length > 0)
            {
                return levels[Pref.CurLevelId];
            }
            return null;
        }

        public void MakeSingleton()
        {
            if (Ins == null)
            {
                Ins = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);
        }
    }
}
