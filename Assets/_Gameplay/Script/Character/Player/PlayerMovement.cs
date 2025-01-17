using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class PlayerMovement : BaseMovement
    {
        internal bool isMoveLeft = false;
        internal bool isMoveRight = false;
        internal bool isGrounded = false;
        internal int jumpStep = 1;
        internal float jumpBufferCounter = 0;
        private readonly float bufferTime = 0.1f;
        private Vector2 rotate;

        public PlayerMovement(BaseCharacter character, Rigidbody2D body) : base(character, body) { }

        public override void Update()
        {
            CheckGrounded();

            if (jumpBufferCounter > 0 && isGrounded)
            {
                Jump();
            }
            jumpBufferCounter -= Time.deltaTime;
        }

        public override void FixedUpdate()
        {
            Moving();
            CalculateRotation();
        }

        private void Moving()
        {
            if (isMoveLeft)
            {
                body.velocity = new(-character.properties.speed, body.velocity.y);
                character.FlipCharacter(false);
            }
            else if (isMoveRight)
            {
                body.velocity = new(character.properties.speed, body.velocity.y);
                character.FlipCharacter(true);
            }
            else
            {
                body.velocity = new(0, body.velocity.y);
            }
        }

        private void CalculateRotation()
        {
            if (character.currentTarget)
            {
                rotate = character.currentTarget.position - character.transform.position;
                character.FlipCharacter(rotate.x > 0);
                RotatePlayer(rotate);
            }
            else
            {
                rotate = Vector2.zero;
                if (isMoveLeft)
                {
                    character.FlipCharacter(false);
                }
                else if (isMoveRight)
                {
                    character.FlipCharacter(true);
                }
            }
        }

        public void RotatePlayer(Vector2 r)
        {

        }

        public void HandleJump()
        {
            jumpBufferCounter = bufferTime;
            if (!isGrounded && jumpStep > 0)
            {
                Jump();
                // EffectManager.Instance.CreatedEffect("doubleJump", _character.transform.position);
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
            RaycastHit2D ray = Physics2D.BoxCast((character as Player).center.position, (character as Player).size, 0f, Vector2.down, .01f, GameManager.Instance.layerGround);

            isGrounded = ray.collider != null;
        }

        public void ResetMoverment()
        {
            isMoveLeft = false;
            isMoveRight = false;
            isGrounded = false;
        }
    }
}
