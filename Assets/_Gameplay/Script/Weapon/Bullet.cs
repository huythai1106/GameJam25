using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private float force;
        private Gun gun;
        private bool isTrigger = false;

        public void Init(Gun gun)
        {
            this.gun = gun;
            Destroy(gameObject, 3f);
        }

        public void Fire(float direct)
        {
            body.velocity = force * direct * Vector2.right;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isTrigger) return;
            isTrigger = true;

            if (other.CompareTag("Monster"))
            {
                BaseCrep creep = other.GetComponent<BaseCrep>();
                creep.HitDamage(gun.gunSetting.damage);
            }

            Destroy(gameObject);
        }
    }
}
