using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class ShieldControl : MonoBehaviour
    {
        public BaseCharacter player;
        public List<BaseCharacter> charactersInShield = new();
        public string tagName;

        private void OnEnable()
        {
            InvokeRepeating(nameof(HitPlayer), 0, 1.5f);
        }

        private void OnDisable()
        {
            charactersInShield.Clear();
            CancelInvoke(nameof(HitPlayer));
        }

        private void Start()
        {
            InvokeRepeating(nameof(HitPlayer), 0, 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagName))
            {
                player.HitDamage(1);

                var c = other.GetComponent<BaseCharacter>();
                c.HitDamage(1);

                charactersInShield.Add(c);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(tagName))
            {
                var c = other.GetComponent<BaseCharacter>();
                charactersInShield.Remove(c);
            }
        }

        public void HitPlayer()
        {
            if (charactersInShield.Count > 0)
            {
                player.HitDamage(1);
                foreach (var item in charactersInShield)
                {
                    item.HitDamage(1);
                }
            }
        }
    }
}
