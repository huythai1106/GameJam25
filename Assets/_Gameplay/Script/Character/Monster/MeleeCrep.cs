using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class MeleeCrep : BaseCrep
    {
        public override void ActiveAttack()
        {
            RaycastHit2D hit = Physics2D.CircleCast(pointGun.transform.position, characterSetting.rangeAttack, Vector2.right, 0, GameManager.Instance.layerPlayer);

            if (hit)
            {
                GameManager.Instance.player.HitDamage(10);
            }
        }
    }
}
