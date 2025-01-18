using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class TriggerArea : MonoBehaviour
    {
        public BaseCrep[] creps;
        private bool isTrigger = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !isTrigger)
            {
                isTrigger = true;
                for (int i = 0; i < creps.Length; i++)
                {
                    creps[i].SetTarget(other.transform);
                }
            }
        }
    }
}
