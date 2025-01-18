using System;
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

    [Serializable]
    public class BaseMovement
    {
        public BaseCharacter character;
        public Rigidbody2D body;
        public Vector2 rotate;
        public DirectPlayer directPlayer = DirectPlayer.Idle;

        public BaseMovement(BaseCharacter character, Rigidbody2D body)
        {
            this.character = character;
            this.body = body;
        }

        public virtual void Update()
        {
            TransformRotateToDirect();
            CheckState();
        }

        public virtual void FixedUpdate()
        {
            MovingNormal();
        }

        protected virtual void CheckState()
        {

        }

        public void TransformRotateToDirect()
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

        public void FlipFollowRotation()
        {
            if (rotate.x > 0)
            {
                character.FlipCharacter(true);
            }
            else if (rotate.x < 0)
            {
                character.FlipCharacter(false);
            }
        }

        public void MovingNormal()
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
    }
}
