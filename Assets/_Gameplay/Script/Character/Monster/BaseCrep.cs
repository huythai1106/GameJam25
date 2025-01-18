using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine;
using UnityEngine;

namespace ParadoxGameStudio
{
    public enum TypeCreep
    {
        Melee,
        Range,
        Curve
    }
    public class BaseCrep : BaseCharacter
    {
        public EventData eventAttack;
        public CrepMovement movement;
        public TypeCreep typeCreep;
        public CrepState state;
        public string nameAnimAttack;

        protected override void Init()
        {
            base.Init();
            movement = new CrepMovement(this, body);
            state = new CrepState(this, anim);

            eventAttack = state.anim.skeleton.Data.FindEvent(nameAnimAttack);

            anim.AnimationState.Event += TriggerAttack;
        }

        public void TriggerAttack(TrackEntry entry, Spine.Event e)
        {
            if (e.Data == eventAttack)
            {
                HandleAttack();
            }
        }

        public virtual void HandleAttack()
        {
            SoundManager.instance.PlaySoundEffect("zomAtk");
        }

        protected override void Update()
        {
            if (isDead) return;

            base.Update();
            movement.Update();
            SetTargetPlayer();
        }

        public void SetTargetPlayer()
        {
            if (!currentTarget && Vector2.Distance(transform.position, GameManager.Instance.player.transform.position) < 7)
            {
                SoundManager.instance.PlaySoundEffect("zomBegin");
                SetTarget(GameManager.Instance.player.transform);
            }
        }

        protected override void FixedUpdate()
        {
            if (isDead) return;

            base.FixedUpdate();
            movement.FixedUpdate();
        }


        public override void Attack()
        {
            base.Attack();
            if (isInAttack) return;
            isInAttack = true;

            state.PlayAnim(1, nameAnimAttack, false, (e) =>
            {
                anim.state.AddEmptyAnimation(1, 0.1f, 0);
                Invoke(nameof(ResetAttack), 1f);
            });
        }

        public void ResetAttack()
        {
            isInAttack = false;
        }

        public override void HitDamage(int damage)
        {
            base.HitDamage(damage);
            SetTarget(GameManager.Instance.player.transform);
        }

        public bool IsTargetInRange()
        {
            if (!currentTarget) return false;

            return Vector2.Distance(currentTarget.position, pointGun.position) < characterSetting.rangeAttack;
        }

        public override void Dead()
        {
            base.Dead();
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pointGun.position, characterSetting.rangeAttack);
        }


    }
}
