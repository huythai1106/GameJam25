using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public enum DirectPlayer
    {
        Left,
        Right,
        Idle
    }

    public class PlayerMovement : BaseMovement
    {
        internal DirectPlayer directPlayer = DirectPlayer.Idle;
        internal bool isGrounded = false;
        internal int jumpStep = 1;
        internal float jumpBufferCounter = 0;
        private readonly float bufferTime = 0.1f;
        public Vector2 rotate;
        private Player player;

        public PlayerMovement(BaseCharacter character, Rigidbody2D body) : base(character, body)
        {
            player = character as Player;
        }

        public override void Update()
        {
            CheckGrounded();

            if (jumpBufferCounter > 0 && isGrounded)
            {
                Jump();
            }
            jumpBufferCounter -= Time.deltaTime;

            CheckRotation();
        }

        private void CheckRotation()
        {
            if (player.statePlayer == StatePlayer.Normal)
            {
                if (rotate.x > 0)
                {
                    directPlayer = DirectPlayer.Right;
                }
                else if (rotate.x < 0)
                {
                    directPlayer = DirectPlayer.Left;
                }
                else
                {
                    directPlayer = DirectPlayer.Idle;
                }
            }
            else
            {

            }
        }

        public override void FixedUpdate()
        {
            Moving();
        }

        private void Moving()
        {
            if (player.statePlayer == StatePlayer.Normal)
            {
                MovingNormal();
            }
            else
            {
                MovingBubbling();
            }
        }

        private void MovingNormal()
        {
            if (directPlayer == DirectPlayer.Left)
            {
                body.velocity = new(-character.properties.speed, body.velocity.y);
                character.FlipCharacter(false);
            }
            else if (directPlayer == DirectPlayer.Right)
            {
                body.velocity = new(character.properties.speed, body.velocity.y);
                character.FlipCharacter(true);
            }
            else
            {
                body.velocity = new(0, body.velocity.y);
            }
        }

        private void MovingBubbling()
        {

        }

        public void HandleJump()
        {
            jumpBufferCounter = bufferTime;
            if (!isGrounded && jumpStep > 0)
            {
                Jump();
                jumpStep--;
            }
        }

        private void Jump()
        {
            isGrounded = false;
            body.velocity = new Vector2(body.velocity.x, character.properties.jumpPower);
            jumpBufferCounter = 0f;
        }

        private void CheckGrounded()
        {
            RaycastHit2D ray = Physics2D.BoxCast(player.center.position, player.size, 0f, Vector2.down, .01f, GameManager.Instance.layerGround);
            isGrounded = ray.collider != null;
        }

        public void ResetMoverment()
        {
            directPlayer = DirectPlayer.Idle;
            isGrounded = false;
        }
    }
}
