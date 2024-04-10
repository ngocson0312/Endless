using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public enum GameTag
    {
        Player,
        Block,
        DeadZone
    }

    public enum GameLayer
    {
        Player,
        Block,
        Dead
    }

    public enum ChacAnim { 
        Idle,
        Jump,
        Land,
        Dead
    }

    public enum GamePref
    {
        BestScore,
        LevelUnlocked,
        CurLevelId,
        IsMusicOn,
        IsSoundOn
    }

    public enum GameScene { 
        MainMenu,
        GamePlay
    }

    public enum MoveDirection
    {
        Left,
        Right
    }

    public enum GameState
    {
        Starting,
        Playing,
        Gameover
    }

    [System.Serializable]
    public class LevelItem
    {
        public int scoreRequire;
        public Sprite unlockThumb;
        public Sprite lockThumb;
        public Sprite levelBG;
        public Sprite chacPreviewImg;
        public Player playerPb;
        public Block blockPb;
        public GameObject mapPb;
        public float spawnTime;
        public float baseSpeed;
        public float maxSpeed;
    }
}
