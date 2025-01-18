using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class CurveCrep : BaseCrep
    {
        public GunSetting gunSetting;

        public override void HandleAttack()
        {
            base.HandleAttack();
            Bullet b = Instantiate(gunSetting.bulletPrefab);
            b.Init(this);
            b.transform.position = pointGun.position;

            b.FireCurve(currentTarget);
        }
    }
}
