using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class Item : MonoBehaviour
    {
        public GameObject button;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                button.SetActive(true);
                GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
