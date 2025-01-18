using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class BulletMonster : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private float force;
        private BaseCharacter player;
        private bool isTrigger = false;

        public void Init(BaseCharacter p)
        {
            Physics2D.IgnoreCollision(p.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            player = p;
            Destroy(gameObject, 3f);
        }

        public void Fire(float direct)
        {
            body.velocity = force * direct * Vector2.right;
        }

        public void FireCurve(Transform target)
        {
            transform.DOJump(target.position, 1.5f, 1, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isTrigger) return;
            isTrigger = true;

            if (other.CompareTag("Player"))
            {
                BaseCrep creep = other.GetComponent<BaseCrep>();
                creep.HitDamage(player.characterSetting.damage);
            }

            Destroy(gameObject);
        }
    }
}
