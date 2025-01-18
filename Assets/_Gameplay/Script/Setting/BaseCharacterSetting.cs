using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    [CreateAssetMenu(menuName = "Paradox/CharacterSetting")]
    public class BaseCharacterSetting : ScriptableObject
    {
        public float speed;
        public float maxHealth;
        public float maxHealthShield;
        public float startHealShield;
        public float jumpPower;
        public float mana;
        public float rangeAttack;
        public float damage;
    }

}
