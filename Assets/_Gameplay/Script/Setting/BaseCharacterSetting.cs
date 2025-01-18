using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    [CreateAssetMenu(menuName = "Paradox/CharacterSetting")]
    public class BaseCharacterSetting : ScriptableObject
    {
        public float speed;
        public int maxHealth;
        public int maxHealthShield;
        public int startHealShield;
        public float jumpPower;
        public int mana;
        public int maxMana;
        public float rangeAttack;
        public int damage;
    }

}
