using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Girl : BaseCharacter
    {
        public GunSetting gunSetting;
        public bool isStun;
        public Transform[] pointMove;
        public Transform pointStart;
        public float timeDelay = 2f;
        public EventData eventAttack;
        private Vector2 rotate;

        public GameObject item;
        public GameObject UIBoss;

        protected override void Init()
        {
            UIBoss.SetActive(true);
            base.Init();
            SetTarget(GameManager.Instance.player.transform);

            eventAttack = anim.skeleton.Data.FindEvent("Attack_0");
            anim.AnimationState.Event += ActionAttack;

            MoveToPoint(pointStart);
        }

        protected override void Update()
        {
            base.Update();
            if (isDead) return;

            if (!isStun)
            {
                RotateFollowTarget();
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void RotateFollowTarget()
        {
            rotate = (currentTarget.transform.position - transform.position).normalized;

            FlipCharacter(rotate.x > 0);
            RotateFollowVector(rotate);
        }

        private void RotateFollowVector(Vector2 v)
        {
            float rotateZ = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            if (rotateZ < -90)
            {
                rotateZ = 180 - Mathf.Abs(rotateZ);
            }
            else if (rotateZ > 90)
            {
                rotateZ = -(180 - rotateZ);
            }
            transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        }

        public override void TurnOnShield()
        {
            if (isDead) return;

            base.TurnOnShield();
            shieldParticle.gameObject.SetActive(true);
            shieldParticle.Play();
            body.gravityScale = 0;
            isStun = false;

            MoveToPoint();
        }

        public override void BreakShield()
        {
            base.BreakShield();
            shieldParticle.Pause();
            shieldParticle.gameObject.SetActive(false);
            SoundManager.instance.PlaySoundEffect("ladyBreakShield");
            Stun();
        }

        public void MoveToPoint(Transform t)
        {
            if (isDead) return;

            anim.state.SetAnimation(0, "Idle_0", true);
            transform.DOMove(t.position, 1f).OnComplete(() =>
            {
                Invoke(nameof(Attack), timeDelay);
            });
        }

        public void MoveToPoint()
        {
            if (isDead) return;

            var t = pointMove[Random.Range(0, pointMove.Length)];
            anim.state.SetAnimation(0, "Idle_0", true);
            transform.DOMove(t.position, 1f).OnComplete(() =>
            {
                Invoke(nameof(Attack), timeDelay);
            });
        }

        public override void Attack()
        {
            base.Attack();
            SoundManager.instance.PlaySoundEffect("ladyAtk");
            anim.state.SetAnimation(0, "Attack_0", true).Complete += (e) =>
            {
                anim.state.SetAnimation(0, "Idle_0", true);
                Invoke(nameof(MoveToPoint), timeDelay);
            };
        }

        private void ActionAttack(TrackEntry entry, Spine.Event e)
        {
            if (e.Data == eventAttack)
            {
                // spawn chai
                Bullet b = Instantiate(gunSetting.bulletPrefab);
                b.Init(this);
                b.transform.position = pointGun.position;

                b.Fire(rotate.normalized);
            }
        }

        public void Stun()
        {
            isStun = true;
            DOTween.Kill(transform);

            CancelInvoke();
            anim.state.SetAnimation(0, "Fall_0", true);

            body.gravityScale = 2.5f;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Ground"))
            {
                anim.state.SetAnimation(0, "Stun_0", true);
                Invoke(nameof(TurnOnShield), 5f);
            }
        }

        public override void Dead()
        {
            isDead = true;
            CancelInvoke();
            anim.state.SetAnimation(0, "Stun_1", true);
            item.SetActive(true);
            Destroy(gameObject, 2f);
            Invoke(nameof(EffectDead), 1.9f);
        }
    }
}
