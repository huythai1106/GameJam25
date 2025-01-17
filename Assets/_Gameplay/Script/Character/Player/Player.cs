using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Player : BaseCharacter
    {
        [Header("Player")]
        public Gun gun;
        public PlayerMovement movement;
        public GameObject currentOneWayPlatform;
        public float range;
        public bool isShoot = false;
        public int maxCountJump = 1;


        [Header("CheckGround")]
        public Transform center;
        public Vector2 size;

        public Transform pointGun;

        protected override void Init()
        {
            base.Init();
            movement = new PlayerMovement(this, body);
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector3 hit = collision.contacts[0].normal;
            if (hit.y > 0)
            {
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

        protected override void SetTarget()
        {

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
    }
}
