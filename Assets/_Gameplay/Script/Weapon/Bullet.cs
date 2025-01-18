using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private float force;
        private BaseCharacter player;
        [SerializeField] private string tagEnemy;
        private bool isTrigger = false;
        public int damage;

        public void Init(BaseCharacter p)
        {
            Physics2D.IgnoreCollision(p.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            player = p;
            damage = player.characterSetting.damage;
            Destroy(gameObject, 3f);
        }

        public void Fire(float direct)
        {
            body.velocity = force * direct * Vector2.right;
        }

        public void Fire(Vector2 direct)
        {
            body.velocity = force * direct;
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

            if (other.CompareTag(tagEnemy))
            {
                isTrigger = true;
                BaseCharacter creep = other.GetComponent<BaseCharacter>();
                creep.HitDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
