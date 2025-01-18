using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
        [Header("Player")]
        public Gun gun;
        public PlayerMovement movement;
        public GameObject currentOneWayPlatform;
        public float range;
        public bool isShoot = false;
        public int maxCountJump = 1;
        public StatePlayer statePlayer = StatePlayer.Normal;

        [SerializeField] private float defaultGravity;


        [Header("CheckGround")]
        public Transform center;
        public Vector2 size;

        protected override void Init()
        {
            base.Init();
            movement = new PlayerMovement(this, body);
            body.gravityScale = defaultGravity;
        }

        protected override void Update()
        {
            base.Update();
            movement.Update();

            if (isShoot)
            {
                gun.Shoot();
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            movement.FixedUpdate();
        }

        public override void Attack()
        {
            base.Attack();
            gun.Shoot();
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

        public void UltimateAttack()
        {
            Debug.Log("UltimateAttack");
            ChangeState(StatePlayer.Bubbling);
            TurnOnShield();
        }

        public void ChargeAttack()
        {
            Debug.Log("CaseAttack");
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
            if (hit.y > 0)
            {
                EffectManager.Instance.CreatedEffect("hitGround", center);
                movement.isGrounded = true;
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

        public void EventChangeNormal()
        {
            body.gravityScale = defaultGravity;
        }

        public void EventChangeBubbling()
        {
            body.gravityScale = 0;
        }
    }
}
