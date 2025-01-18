using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class RangeCrep : BaseCrep
    {
        public GunSetting gunSetting;

        public override void HandleAttack()
        {
            base.HandleAttack();
            Bullet b = Instantiate(gunSetting.bulletPrefab);
            b.Init(this);
            b.transform.position = pointGun.position;

            b.Fire(Mathf.Sign(transform.localScale.x));
        }
    }
}
