using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    [CreateAssetMenu(menuName = "Paradox/GunSetting")]
    public class GunSetting : ScriptableObject
    {
        public float fireRate;
        public float damage;
        public int bulletInMage;
        public float timeReload;
        public Bullet bulletPrefab;
    }
}
