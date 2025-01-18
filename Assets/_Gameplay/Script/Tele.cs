using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Tele : MonoBehaviour
    {
        public Transform port;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.position = port.transform.position;
            }
        }
    }
}
