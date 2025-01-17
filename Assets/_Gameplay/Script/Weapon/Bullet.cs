using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private float force;

        private void Awake()
        {
            Destroy(gameObject, 3f);
        }

        public void Fire(float direct)
        {
            body.velocity = force * direct * Vector2.right;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}
