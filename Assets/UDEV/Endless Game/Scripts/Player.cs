using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class Player : MonoBehaviour, ICompChk
    {
        public float jumpForce;
        public LayerMask blockLayer;
        public float blockCheckingRadius;
        public float blockCheckingOffset;
        public GameObject landVfxPb;

        private Rigidbody2D m_rb;
        private Animator m_anim;
        private Vector2 m_centerPos;
        private bool m_isOnBlock;
        private int m_blockId;
        private bool m_isDead;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_anim = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (m_isDead || IsComponentsNull()) return;

            transform.position = new Vector3(
                0, transform.position.y, 0f
                );

            Jump();

            if(m_rb.velocity.y < 0)
            {
                if (m_isOnBlock)
                {
                    m_anim.SetBool(ChacAnim.Jump.ToString(), false);
                    m_anim.SetBool(ChacAnim.Land.ToString(), true);
                }else
                {
                    m_anim.SetBool(ChacAnim.Jump.ToString(), false);
                }
            }
        }

        private void FixedUpdate()
        {
            IsOnBlock();
        }

        public bool IsComponentsNull()
        {
            bool checking = m_rb == null || m_anim == null;
            if (checking)
                Debug.LogError("Some component is null. Please check!!!.");

            return checking;
        }

        private void IsOnBlock()
        {
            m_centerPos = new Vector3(transform.position.x,
                transform.position.y - blockCheckingOffset, transform.position.z
                );
            Collider2D col = Physics2D.OverlapCircle(m_centerPos, blockCheckingRadius, blockLayer);

            m_isOnBlock = col != null ? true : false;
        }

        public void Jump()
        {
            if (!GamepadController.Ins.CanJump || !m_isOnBlock || IsComponentsNull()) return;

            GamepadController.Ins.CanJump = false;

            m_rb.velocity = Vector2.up * jumpForce;

            m_anim.SetBool(ChacAnim.Jump.ToString(), true);
            m_anim.SetBool(ChacAnim.Land.ToString(), false);

            AudioController.Ins.PlaySound(AudioController.Ins.jump);
        }

        public void BackToIdle()
        {
            m_anim.SetBool(ChacAnim.Land.ToString(), false);
            m_anim.SetTrigger(ChacAnim.Idle.ToString());
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(GameTag.Block.ToString()))
            {
                Block block = col.gameObject.GetComponent<Block>();
                if (block && block.Id != m_blockId)
                {
                    m_blockId = block.Id;
                    GameManager.Ins.AddScore(block.CurScore);
                    block.PlayerLand();
                }

                if(col != null && col.contactCount > 0 && landVfxPb)
                {
                    Vector3 spawnPos = new Vector3(transform.position.x,
                        col.contacts[0].point.y, 0f
                        );

                    Instantiate(landVfxPb, spawnPos, Quaternion.identity);
                }

                AudioController.Ins.PlaySound(AudioController.Ins.land);

                Debug.Log("da va cham voi block");
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(GameTag.DeadZone.ToString()) && !m_isDead)
            {
                m_isDead = true;
                m_anim.SetTrigger(ChacAnim.Dead.ToString());
                gameObject.layer = LayerMask.NameToLayer(GameLayer.Dead.ToString());
                GameManager.Ins.Gameover();
            }
        }

        private void OnDrawGizmos()
        {
            m_centerPos = new Vector3(transform.position.x,
                transform.position.y - blockCheckingOffset, transform.position.z
                );
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(m_centerPos, blockCheckingRadius);
        }
    }
}
