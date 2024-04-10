using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class GameManager : MonoBehaviour, ICompChk
    {
        public static GameManager Ins;
        public float speedUp;
        public GameState state;
        public GameObject warningSignPb;

        private Player m_curPlayer;
        private Block m_curBlock;
        private LevelItem m_curLevel;

        private Vector2 m_camSize;
        private int m_blockIdx;
        private float m_blockSpawnPosY;
        private float m_blockSpeed;
        private int m_score;

        public Block CurBlock { get => m_curBlock;}
        public int Score { get => m_score;}

        private void Awake()
        {
            Ins = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        public void Init()
        {
            if (IsComponentsNull()) return;

            state = GameState.Starting;
            m_camSize = Helper.Get2DCamSize();
            m_blockSpawnPosY = -m_camSize.y / 2 + 1f;
            m_curLevel = LevelManager.Ins.GetLevel();
            m_blockIdx = 1;
            Pref.hasBestScore = false;

            if(m_curLevel != null)
            {
                m_blockSpeed = m_curLevel.baseSpeed;
                var mapPb = m_curLevel.mapPb;
                if (mapPb)
                    Instantiate(mapPb, Vector3.zero, Quaternion.identity);

                var blockPb = m_curLevel.blockPb;
                if (blockPb)
                    m_curBlock = Instantiate(blockPb, new Vector3(0, m_blockSpawnPosY, 0f), Quaternion.identity);
            }

            ActivePlayer();

            GUIManager.Ins.ShowGameGUI(false);
        }

        public void ActivePlayer()
        {
            if (IsComponentsNull()) return;

            if (m_curPlayer)
                Destroy(m_curPlayer.gameObject);

            if(m_curLevel != null)
            {
                var newPlayerPb = m_curLevel.playerPb;
                if(newPlayerPb)
                    m_curPlayer = Instantiate(newPlayerPb, new Vector3(0, -1f, 0f), Quaternion.identity);

                if (m_curPlayer)
                    CameraFollow.Ins.target = m_curPlayer.transform;
            }
        }

        IEnumerator SpawnBlockCo()
        {
            if (IsComponentsNull() || m_curLevel == null || m_curBlock == null) 
                yield return null;

            var blockPrefab = m_curLevel.blockPb;

            if (blockPrefab == null)
                yield return null;

            while (state != GameState.Gameover)
            {
                m_blockSpawnPosY += m_curBlock.blockGrap;
                m_blockSpeed += speedUp;
                m_blockSpeed = Mathf.Clamp(m_blockSpeed, m_curLevel.baseSpeed, m_curLevel.maxSpeed);
                float checking = Random.Range(0f, 1f);
                SpriteRenderer prevBlockSp = m_curBlock.Sp;
                GameObject warningSignClone = null;

                if(checking <= 0.5f)
                {
                    Vector3 spawnPos = new Vector3(m_camSize.x / 2 - 0.3f, m_blockSpawnPosY, 0f);
                    warningSignClone = Instantiate(warningSignPb, spawnPos, Quaternion.identity);
                    warningSignClone.transform.localScale = new Vector3(
                        warningSignClone.transform.localScale.x * -1f,
                        warningSignClone.transform.localScale.y,
                        warningSignClone.transform.localScale.z
                        );
                }else
                {
                    Vector3 spawnPos = new Vector3(-(m_camSize.x / 2 - 0.3f), m_blockSpawnPosY, 0f);
                    warningSignClone = Instantiate(warningSignPb, spawnPos, Quaternion.identity);
                }


                yield return new WaitForSeconds(m_curLevel.spawnTime);

                if (checking <= 0.5f)
                {
                    Vector3 spawnPos = new Vector3(m_camSize.x / 2 + 0.6f, m_blockSpawnPosY, 0f);
                    m_curBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                    m_curBlock.moveDirection = MoveDirection.Left;
                }
                else
                {
                    Vector3 spawnPos = new Vector3(-(m_camSize.x / 2 + 0.6f), m_blockSpawnPosY, 0f);
                    m_curBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                    m_curBlock.moveDirection = MoveDirection.Right;
                }
                m_curBlock.ChangeSprite(ref m_blockIdx);
                m_curBlock.SpriteOrderUp(prevBlockSp);
                m_curBlock.moveSpeed = m_blockSpeed;
                m_curBlock.canMove = true;
                if(warningSignClone)
                    Destroy(warningSignClone);
            }
        }

        public void PlayGame()
        {
            if (IsComponentsNull()) return;

            state = GameState.Playing;

            StartCoroutine(SpawnBlockCo());

            GUIManager.Ins.ShowGameGUI(true);

            AudioController.Ins.PlayBackgroundMusic();
        }

        public void Gameover()
        {
            if (IsComponentsNull()) return;
            state = GameState.Gameover;
            CamShake.ins.ShakeTrigger();
            GUIManager.Ins.ShowGameoverImgTxt();
            AudioController.Ins.StopPlayMusic();
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
            Debug.Log("Gameover!!!");
        }

        public void AddScore(int score)
        {
            if (IsComponentsNull() || state != GameState.Playing) return;

            m_score += score;
            Pref.bestScore = m_score;
            GUIManager.Ins.UpdateScore(m_score);
            AudioController.Ins.PlaySound(AudioController.Ins.score);
        }

        public bool IsComponentsNull()
        {
            bool checking = LevelManager.Ins == null || GUIManager.Ins == null;

            if (checking)
                Debug.LogError("Some component is null. Please check!!!.");

            return checking;
        }
    }
}
