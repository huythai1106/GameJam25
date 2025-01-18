using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class CrepController : ControllerBase
    {
        private BaseCrep creep;

        protected override void Start()
        {
            base.Start();
            creep = character as BaseCrep;
        }

        protected override void HandleControl()
        {
            if (creep.IsTargetInRange())
            {
                creep.movement.rotate = Vector2.zero;
                creep.Attack();
            }
            else
            {
                if (character.currentTarget)
                {
                    creep.movement.rotate = (creep.currentTarget.position - transform.position).normalized;
                }
                else
                {
                    creep.movement.rotate = Vector2.zero;
                }
            }
        }
    }
}
