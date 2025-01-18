using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class BaseCrep : BaseCharacter
    {
        public EventData eventData;
        public BaseMovement movement;

        protected override void Init()
        {
            base.Init();
            movement = new BaseMovement(this, body);
        }

        protected override void Update()
        {
            base.Update();
            movement.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            movement.FixedUpdate();
        }

        public override void Attack()
        {
            base.Attack();
            if (isInAttack) return;
            isInAttack = true;

            ActiveAttack();

            Invoke(nameof(ResetAttack), 1f);
        }

        public void ResetAttack()
        {
            isInAttack = false;
        }

        public override void HitDamage(float damage)
        {
            base.HitDamage(damage);
            SetTarget(GameManager.Instance.player.transform);
        }

        public bool IsTargetInRange()
        {
            if (!currentTarget) return false;

            return Vector2.Distance(currentTarget.position, pointGun.position) < characterSetting.rangeAttack;
        }

        public virtual void ActiveAttack()
        {

        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pointGun.position, characterSetting.rangeAttack);
        }
    }
}
