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
        public int maxHealth;
        public int currentHealth;
        public float jumpPower;
        public int currentMana;
        public int healthShield;
    }

    public class BaseCharacter : MonoBehaviour
    {
        internal Rigidbody2D body;
        internal Collider2D col;
        internal ControllerBase controller;

        [Header("UI")]
        public StatusStackUI healthUI;
        public StatusStackUI manaUI;
        public StatusStackUI shieldUI;

        [Header("Common")]
        public Properties properties;
        public TypeCharacter typeCharacter;
        public BaseCharacterSetting characterSetting;
        public Transform currentTarget;
        public Transform pointGun;
        public SkeletonAnimation anim;
        public ParticleSystem shieldParticle;
        public bool isInAttack = false;

        public bool isDead = false;
        // public Dictionary<TypeEffect, BaseEffect> effects = new();

        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            Init();
        }

        protected virtual void Init()
        {
            SetupProperties();
        }

        protected virtual void Update()
        {
            if (isDead) return;
        }

        protected virtual void FixedUpdate()
        {
            if (isDead) return;
            // SetTarget();
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

        public virtual void SetupProperties()
        {
            properties.speed = characterSetting.speed;
            properties.maxHealth = characterSetting.maxHealth;
            properties.currentHealth = characterSetting.maxHealth;
            properties.jumpPower = characterSetting.jumpPower;
            properties.currentMana = characterSetting.mana;
            properties.healthShield = characterSetting.startHealShield;

            healthUI?.SetUp(properties.maxHealth);
            healthUI?.Change(properties.currentHealth);

            manaUI?.SetUp(characterSetting.maxMana);
            manaUI?.Change(characterSetting.mana);

            shieldUI?.SetUp(characterSetting.maxHealthShield);
            shieldUI?.Change(properties.healthShield);
        }

        public virtual void TurnOnShield()
        {
            SoundManager.instance.PlaySoundEffect("shieldOn");
            properties.healthShield = characterSetting.maxHealthShield;
            shieldUI?.Change(properties.healthShield);
        }

        public virtual void SetTarget(Transform t)
        {
            currentTarget = t;
        }
        public virtual void Attack() { }

        public virtual void HitDamage(int damage)
        {
            SoundManager.instance.PlaySoundEffect("hitBody");
            if (properties.healthShield > 0)
            {
                properties.healthShield = Mathf.Max(0, properties.healthShield - damage);
                shieldUI?.Change(properties.healthShield);
                if (properties.healthShield == 0)
                {
                    BreakShield();
                }
            }
            else
            {
                StartCoroutine(BlingBling());
                EffectManager.Instance.CreatedEffect("bloodExp", transform);
                properties.currentHealth = Mathf.Max(0, properties.currentHealth - damage);
                healthUI?.Change(properties.currentHealth);

                if (properties.currentHealth == 0)
                {
                    Dead();
                }
            }
        }

        public virtual void BreakShield() { }

        public virtual void Dead()
        {
            isDead = true;
            EffectDead();
        }

        public void EffectDead()
        {
            EffectManager.Instance.CreatedEffect("smokeExp", transform.position);
            EffectManager.Instance.CreatedEffect("bubbleRising", transform.position, new Option()
            {
                timeDestroy = 6f,
                offset = new Vector3(0, -1, 0),
            });
        }

        public IEnumerator BlingBling()
        {
            if (!isDead)
            {
                anim?.skeleton.SetColor(new Color(1, 0.3f, 0.3f));
                yield return new WaitForSeconds(0.1f);
                if (!isDead)
                    anim?.skeleton.SetColor(Color.white);
            }

        }
    }
}
