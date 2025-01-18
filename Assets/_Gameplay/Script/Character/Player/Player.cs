using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Spine;
using UnityEngine;

namespace ParadoxGameStudio
{
    public enum StatePlayer
    {
        Normal,
        Bubbling
    }

    public class Player : BaseCharacter
    {
        public bool isUnlockSkill1 = false;
        public bool isUnlockSkill2 = false;

        [Header("Player")]
        // public Gun gun;
        public GunSetting gunSetting;
        public PlayerMovement movement;
        public PlayerState state;
        public GameObject currentOneWayPlatform;
        public float range;
        public bool isShoot = false;
        public int maxCountJump = 1;
        public StatePlayer statePlayer = StatePlayer.Normal;
        public bool isCharging = false;
        public int maxMana = 9;

        [SerializeField] private float defaultGravity;
        public AudioSource audioSource;


        [Header("CheckGround")]
        public Transform center;
        public Vector2 size;

        // event
        public EventData eventAttack_0;
        public EventData eventAttack_1;

        protected override void Init()
        {
            base.Init();
            movement = new PlayerMovement(this, body);
            state = new PlayerState(this, anim);
            body.gravityScale = defaultGravity;

            eventAttack_0 = state.anim.skeleton.Data.FindEvent("Attack_0");
            eventAttack_1 = state.anim.skeleton.Data.FindEvent("Attack_1");

            anim.AnimationState.Event += ActiveAttack_0;
            anim.AnimationState.Event += ActiveAttack_1;
        }

        protected override void Update()
        {
            if (isDead) return;
            // PreventPos();

            base.Update();
            movement.Update();
        }

        protected override void FixedUpdate()
        {
            if (isDead) return;

            base.FixedUpdate();
            movement.FixedUpdate();
        }

        private void PreventPos()
        {
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            transform.ClampPosition(minX, maxX, minY, maxY);
        }

        public override void Attack()
        {
            if (isInAttack) return;
            isInAttack = true;
            properties.currentMana = Mathf.Min(properties.currentMana + 1, maxMana);
            manaUI?.Change(properties.currentMana);
            SoundManager.instance.PlaySoundEffect("drinking");

            state.PlayAnim(1, "Attack_0", false, (e) =>
            {
                isInAttack = false;
                anim.state.AddEmptyAnimation(1, 0.1f, 0);
            });
        }

        public void ChargeAttack()
        {
            if (isInAttack) return;
            if (properties.currentMana < 3) return;

            properties.currentMana -= 3;
            manaUI?.Change(properties.currentMana);

            isInAttack = true;

            isCharging = true;
            if (statePlayer == StatePlayer.Normal)
            {
                body.gravityScale = 0.7f;
            }
            body.velocity = Vector2.zero;

            state.PlayAnim(1, "Attack_1", false, (e) =>
           {
               isInAttack = false;
               anim.state.AddEmptyAnimation(1, 0.1f, 0);

               if (statePlayer == StatePlayer.Normal)
               {
                   body.gravityScale = defaultGravity;
               }

               isCharging = false;
           });
        }

        public void UltimateAttack()
        {
            if (isInAttack) return;
            if (statePlayer == StatePlayer.Bubbling) return;
            if (properties.currentMana < 3) return;

            properties.currentMana -= 3;
            manaUI?.Change(properties.currentMana);

            ChangeState(StatePlayer.Bubbling);
            TurnOnShield();
        }

        public void ActiveAttack_0(TrackEntry entry, Spine.Event e)
        {
            if (e.Data == eventAttack_0)
            {
                SoundManager.instance.PlaySoundEffect("lightAtk");
                Bullet b = Instantiate(gunSetting.bulletPrefab);
                b.Init(this);
                b.transform.position = pointGun.position;

                b.Fire(Mathf.Sign(transform.localScale.x));
            }
        }

        public void ActiveAttack_1(TrackEntry entry, Spine.Event e)
        {
            if (e.Data == eventAttack_1)
            {
                SoundManager.instance.PlaySoundEffect("chargeAtk");
                Bullet b = Instantiate(gunSetting.bulletPrefab);
                b.Init(this);
                b.damage = 3;
                b.transform.position = pointGun.position;

                b.Fire(Mathf.Sign(transform.localScale.x));
            }
        }

        public void Jump()
        {
            if (statePlayer == StatePlayer.Bubbling)
            {
                BreakShield();
            }
            else
            {
                movement.HandleJump();
            }
        }

        public override void TurnOnShield()
        {
            base.TurnOnShield();
            shieldParticle.gameObject.SetActive(true);
            shieldParticle.Play();
        }

        public override void BreakShield()
        {
            ChangeState(StatePlayer.Normal);
            properties.healthShield = 0;

            shieldParticle.Pause();
            shieldParticle.gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector3 hit = collision.contacts[0].normal;
            Debug.Log(hit.y);
            if (hit.y > 0.1f)
            {
                EffectManager.Instance.CreatedEffect("hitGround", center);
                // movement.isGrounded = true;
                // _character.state.SetStatePlayer(StatePlayer.Jump, false);
                movement.jumpStep = maxCountJump;
                // _character.canControll = true;
                // EffectManager.Instance.CreatedEffect("touchGround", transform);

                if (collision.gameObject.CompareTag("OneWayPlatform"))
                {
                    currentOneWayPlatform = collision.gameObject;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("OneWayPlatform") && currentOneWayPlatform != null && currentOneWayPlatform.name == collision.gameObject.name)
            {
                movement.isGrounded = false;
                currentOneWayPlatform = null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center.position, size);
        }

        private IEnumerator DisableCollision()
        {
            if (currentOneWayPlatform != null)
            {
                Collider2D platformCollider = currentOneWayPlatform.GetComponent<Collider2D>();

                Physics2D.IgnoreCollision(col, platformCollider);
                yield return new WaitForSeconds(0.5f);
                Physics2D.IgnoreCollision(col, platformCollider, false);
            }
        }

        public void ButtonDown()
        {
            StartCoroutine(DisableCollision());
        }

        public void ChangeState(StatePlayer state)
        {
            statePlayer = state;

            if (statePlayer == StatePlayer.Normal)
            {
                EventChangeNormal();
            }
            else
            {
                EventChangeBubbling();
            }
        }

        public void StartCharge()
        {
            Debug.Log("StartCharge");
        }

        public void EndCharge()
        {
            Debug.Log("EndCharge");
        }

        public void EventChangeNormal()
        {
            body.gravityScale = defaultGravity;
        }

        public void EventChangeBubbling()
        {
            body.gravityScale = 0;
        }

        public override void Dead()
        {
            base.Dead();
            movement.rotate = Vector2.zero;
            anim.GetComponent<Renderer>().enabled = false;

            Invoke(nameof(ResetScene), 2f);
        }

        public void ResetScene()
        {
            GameManager.Instance.ResetScene();
        }
    }
}
