using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class Block : MonoBehaviour, ICompChk
    {
        public float moveSpeed;
        public MoveDirection moveDirection;
        public Sprite[] sprites;
        public bool canMove;
        public float blockGrap;
        public int minScore;
        public int maxScore;

        private Rigidbody2D m_rb;
        private SpriteRenderer m_sp;

        private int m_id;
        private int m_curScore;

        public SpriteRenderer Sp { get => m_sp;}
        public int Id { get => m_id;}
        public int CurScore { get => m_curScore;}

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_sp = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_id = GetInstanceID();
            m_curScore = Random.Range(minScore, maxScore);
        }

        // Update is called once per frame
        void Update()
        {
            BlockMoving();
        }

        public bool IsComponentsNull()
        {
            bool checking = m_rb == null || m_sp == null;
            if (checking)
                Debug.LogError("Some component is null. Please check!!!.");

            return checking;
        }

        public void ChangeSprite(ref int idx)
        {
            if (sprites == null || sprites.Length <= 0 || IsComponentsNull()) return;

            m_sp.sprite = sprites[idx];

            idx++;

            if (idx >= sprites.Length)
                idx = 0;
        }

        private void BlockMoving()
        {
            if (IsComponentsNull() || !canMove) return;

            if(moveDirection == MoveDirection.Left)
            {
                m_rb.velocity = Vector2.left * moveSpeed;
            }else if(moveDirection == MoveDirection.Right)
            {
                m_rb.velocity = Vector2.right * moveSpeed;
            }

            Vector3 centerPos = new Vector3(0, transform.position.y, transform.position.z);
            float distToCenterPos = Vector2.Distance(transform.position, centerPos);
            if(distToCenterPos <= 0.1f)
            {
                m_rb.velocity = Vector2.zero;
                transform.position = centerPos;
            }
        }

        public void PlayerLand()
        {
            if (IsComponentsNull() || !canMove) return;

            canMove = false;
            m_rb.velocity = Vector2.zero;
        }

        public void SpriteOrderUp(SpriteRenderer prevBlockSp)
        {
            if(IsComponentsNull()) return;

            m_sp.sortingOrder = prevBlockSp.sortingOrder + 1;
        }
    }
}
