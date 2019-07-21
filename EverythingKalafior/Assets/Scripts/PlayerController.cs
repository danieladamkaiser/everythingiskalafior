using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Serializable;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController, GardenListener
    {
        public Keys Keys;
        public PlayerConfig PlayerConfig;
        public BoxCollider2D PlatformCollider;

        private Rigidbody2D rb;
        private CapsuleCollider2D col;
        private IInput input;
        private bool isActive;
        private bool isDead;
        private float playerSpeed;
        private bool canJump = false;
        private float jumpTolerance = 0.01f;

        public Vector3 dir = Vector3.right;

        public void Awake()
        {
            playerSpeed = PlayerConfig.MoveSpeed;
            input = new KeyboardInput();

            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<CapsuleCollider2D>();
        }

        public void Die()
        {
            isActive = false;
            isDead = true;
            PlatformCollider.enabled = true;
            GetComponent<AnimationMoveControll>().Die();
        }

        public void WakeUp()
        {
            isActive = true;
        }


        public void Jump()
        {
            if (canJump)
            {
                rb.velocity = new Vector2(0, PlayerConfig.JumpForce);
                canJump = false;
            }
        }

        public void Walk(float direction)
        {
            float horizontalVelocity = rb.velocity.x;
            if (Mathf.Abs(horizontalVelocity) <= playerSpeed || (horizontalVelocity < 0 && direction > 0) || (horizontalVelocity > 0 && direction < 0))
            {
                if (canJump)
                {
                    rb.velocity = new Vector2(direction * playerSpeed, rb.velocity.y);
                }
                else
                {
                    float adjustedHorizontalVelocity = Mathf.Clamp(rb.velocity.x + direction * playerSpeed * 0.2f, -playerSpeed, playerSpeed);
                    rb.velocity = new Vector2(adjustedHorizontalVelocity,rb.velocity.y);
                }
            }
        }

        void Start()
        {
        }

        void Update()
        {
            if (isActive )
            {
                Move();
            } 
        }

        private void Move()
        {
            Walk(dir.x);

            if (dir == Vector3.up)
            {
                Jump();
            }

            if (input.GetKeyStatus(Keys.Die) == KeyStatus.JustDown) // ?
            {
                Die();
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            var highestCollisionYPosition = collision.contacts.Max(a => a.point.y);
            var lowestBoundsYPosition = col.bounds.min.y;
            if (highestCollisionYPosition - jumpTolerance < lowestBoundsYPosition && highestCollisionYPosition + jumpTolerance > lowestBoundsYPosition)
            {
                canJump = true;
            }

            if (isDead && collision.contacts.Min(c => c.point.y) < col.bounds.min.y + jumpTolerance)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                col.enabled = false;
            }
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            if (rb.velocity.y+jumpTolerance>0 && rb.velocity.y-jumpTolerance<0)
            canJump = false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
        }

        public void OnNewCauliflower(Cauliflower cauliflower)
        {
            Die();
        }
    }
}