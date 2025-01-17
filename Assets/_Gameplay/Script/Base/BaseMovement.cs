using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class BaseMovement
    {
        public BaseCharacter character;
        public Rigidbody2D body;

        public BaseMovement(BaseCharacter character, Rigidbody2D body)
        {
            this.character = character;
            this.body = body;
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }
    }

}
