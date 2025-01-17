using System.Collections;
using System.Collections.Generic;
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
    }

    public class BaseCharacter : MonoBehaviour
    {
        public EventGame EventGame;
        public EventGame eventHitAttack;

        internal Rigidbody2D body;
        internal Collider2D col;
        internal ControllerBase controller;


        [Header("Common")]
        public Properties properties;
        public TypeCharacter typeCharacter;
        public BaseCharacterSetting characterSetting;
        public Transform currentTarget;
        public bool isPoisonous;

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
            SetTarget();
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
            }
            else
            {
                transform.localScale = new(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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
        }

        protected virtual void SetTarget() { }
        protected virtual void Attack() { }

        protected virtual void HitDamage(float damage)
        {
            eventHitAttack?.Invoke();
        }

        protected virtual void Dead()
        {
            // eventDead?.Invoke();
        }

        // public void AddEffect(BaseEffect e)
        // {
        //     if (effects.ContainsKey(e.typeEffect))
        //     {
        //         if (e.maxTime >= effects[e.typeEffect].timeCounter)
        //             effects[e.typeEffect].ResetTimeCounter(e.maxTime);
        //     }
        //     else
        //     {
        //         effects.Add(e.typeEffect, e);
        //         effects[e.typeEffect].OnEffect(this);
        //     }
        // }
    }
}
