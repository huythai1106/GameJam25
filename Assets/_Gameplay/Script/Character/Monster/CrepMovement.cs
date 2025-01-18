using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class CrepMovement : BaseMovement
    {
        public BaseCrep crep;

        public CrepMovement(BaseCharacter character, Rigidbody2D body) : base(character, body)
        {
            crep = character as BaseCrep;
        }

        protected override void CheckState()
        {
            if (character.typeCharacter == TypeCharacter.Monster)
            {
                if (rotate.x != 0)
                {
                    crep.state.SetStatePlayer(StateCreep.Run);
                }
                else
                {
                    crep.state.SetStatePlayer(StateCreep.Idle);
                }
            }
        }
    }
}
