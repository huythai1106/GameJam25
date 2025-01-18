using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    [Serializable]
    public class PlayerMovement : BaseMovement
    {
        public bool isGrounded = false;
        internal int jumpStep = 1;
        internal float jumpBufferCounter = 0;
        private readonly float bufferTime = 0.1f;
        private Player player;

        public PlayerMovement(BaseCharacter character, Rigidbody2D body) : base(character, body)
        {
            player = character as Player;
        }

        public override void Update()
        {
            base.Update();

            CheckState();
            if (jumpBufferCounter > 0 && isGrounded)
            {
                Jump();
            }
            jumpBufferCounter -= Time.deltaTime;

        }

        public override void FixedUpdate()
        {
            Moving();
        }

        public void CheckState()
        {
            if (player.statePlayer == StatePlayer.Bubbling)
            {
                player.state.SetStatePlayer(StateCharacter.Fly);
            }
            else
            {
                if (isGrounded)
                {
                    if (rotate.x != 0)
                    {
                        player.state.SetStatePlayer(StateCharacter.Run);
                    }
                    else
                    {
                        player.state.SetStatePlayer(StateCharacter.Idle);
                    }
                }
                else
                {
                    player.state.SetStatePlayer(StateCharacter.Jump);
                }
            }
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



        private void MovingBubbling()
        {
            character.body.velocity = rotate * 5;

            FlipFollowRotation();
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

        // private void CheckGrounded()
        // {
        //     // RaycastHit2D ray = Physics2D.BoxCast(player.center.position, player.size, 0f, Vector2.down, .01f, GameManager.Instance.layerGround);
        //     // isGrounded = ray.collider != null;
        // }

        public void ResetMoverment()
        {
            directPlayer = DirectPlayer.Idle;
            isGrounded = false;
        }
    }
}
