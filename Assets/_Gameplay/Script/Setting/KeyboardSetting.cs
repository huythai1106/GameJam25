using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    [CreateAssetMenu(menuName = "Paradox/KeyboardSetting")]
    public class KeyboardSetting : ScriptableObject
    {
        public KeyBoard playerKeyboard;

        [System.Serializable]
        public class KeyBoard
        {
            public KeyCode left, right, up, down, attack, specialAttack, chargeAttack;

            public KeyCode this[int i]
            {
                get
                {
                    return new KeyCode[] { left, right, up, down, attack, specialAttack, chargeAttack }[i];
                }
            }
        }
    }
}
