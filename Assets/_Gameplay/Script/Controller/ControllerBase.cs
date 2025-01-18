using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class ControllerBase : MonoBehaviour
    {
        internal BaseCharacter character;

        protected virtual void Start()
        {
            character = GetComponent<BaseCharacter>();
            character.controller = this;
        }

        private void Update()
        {
            HandleControl();
        }

        protected virtual void HandleControl() { }
    }

}
