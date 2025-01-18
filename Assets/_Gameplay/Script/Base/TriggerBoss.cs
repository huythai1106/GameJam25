using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class TriggerBoss : MonoBehaviour
    {
        public BaseCharacter boss;
        public GameObject col;
        private bool isTrigger = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !isTrigger)
            {
                isTrigger = true;
                boss.gameObject.SetActive(true);
                SoundManager.instance.PlayLoopSound(GameManager.Instance.bossBGM);
                col.SetActive(true);
            }
        }
    }
}
