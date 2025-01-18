using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace ParadoxGameStudio
{
    [System.Serializable]
    public struct Properties
    {
        public float speed;
        public float maxHealth;
        public float currentHealth;
        public float jumpPower;
        public float currentMana;
        public float healthShield;
    }

    public class BaseCharacter : MonoBehaviour
    {
        internal Rigidbody2D body;
        internal Collider2D col;
        internal ControllerBase controller;


        [Header("Common")]
        public Properties properties;
        public TypeCharacter typeCharacter;
        public BaseCharacterSetting characterSetting;
        public Transform currentTarget;
        public bool isPoisonous;
        public Transform pointGun;
        public SkeletonAnimation anim;
        public ParticleSystem shieldParticle;

        public bool isDead = false;
        // public Dictionary<TypeEffect, BaseEffect> effects = new();

        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            Init();
        }

        protected virtual void Update()
        {
            UpdateEffects();
            HandleEffect();
        }

        protected virtual void FixedUpdate()
        {
            // SetTarget();
        }

        private void UpdateEffects()
        {
            // foreach (TypeEffect entry in effects.Keys.ToList())
            // {
            //     if (effects[entry].timeCounter > 0)
            //     {
            //         effects[entry].timeCounter -= Time.deltaTime;
            //         if (effects[entry].timeCounter <= 0)
            //         {
            //             effects[entry].OffEffect(this);
            //             effects.Remove(entry);
            //         }
            //     }
            // }
        }

        private void HandleEffect()
        {
            if (isPoisonous)
            {
                // Do something
            }
        }

        public void FlipCharacter(bool value) // true is scale right, false is left
        {
            if (value)
            {
                transform.localScale = new(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                if (shieldParticle)
                {
                    shieldParticle.transform.localScale = new(Mathf.Abs(shieldParticle.transform.localScale.x), shieldParticle.transform.localScale.y, shieldParticle.transform.localScale.z);
                }
            }
            else
            {
                transform.localScale = new(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                if (shieldParticle)
                {
                    shieldParticle.transform.localScale = new(-Mathf.Abs(shieldParticle.transform.localScale.x), shieldParticle.transform.localScale.y, shieldParticle.transform.localScale.z);
                }
            }
        }

        protected virtual void Init()
        {
            SetupProperties();
        }

        public virtual void SetupProperties()
        {
            properties.speed = characterSetting.speed;
            properties.maxHealth = characterSetting.maxHealth;
            properties.currentHealth = characterSetting.maxHealth;
            properties.jumpPower = characterSetting.jumpPower;
            properties.currentMana = characterSetting.mana;
            properties.healthShield = characterSetting.startHealShield;
        }

        public virtual void TurnOnShield()
        {
            properties.healthShield = characterSetting.maxHealthShield;
        }

        public virtual void SetTarget(Transform t)
        {
            currentTarget = t;
        }
        public virtual void Attack() { }

        public virtual void HitDamage(float damage)
        {
            StartCoroutine(BlingBling());
            EffectManager.Instance.CreatedEffect("bloodExp", transform);

            if (properties.healthShield > 0)
            {
                properties.healthShield = Mathf.Max(0, properties.healthShield - damage);
                if (properties.healthShield == 0)
                {
                    BreakShield();
                }
            }

            properties.currentHealth = Mathf.Max(0, properties.currentHealth - damage);

            if (properties.currentHealth == 0)
            {
                Dead();
            }
        }

        public virtual void BreakShield() { }

        public virtual void Dead()
        {
            isDead = true;
        }

        public IEnumerator BlingBling()
        {
            anim?.skeleton.SetColor(new Color(1, 0.3f, 0.3f));
            yield return new WaitForSeconds(0.1f);
            anim?.skeleton.SetColor(Color.white);
        }
    }
}
