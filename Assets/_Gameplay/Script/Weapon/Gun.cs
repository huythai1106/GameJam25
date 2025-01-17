using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public enum StatusWeapon
    {
        Ready,
        Waiting
    }

    [System.Serializable]
    public struct GunProperties
    {
        public int bulletRemainingInMage;
        public float timeReload;
    }

    public class Gun : MonoBehaviour
    {
        public Player player;
        public GunSetting gunSetting;
        public GunProperties gunProperties;
        protected StatusWeapon statusWeapon = StatusWeapon.Ready;
        protected Coroutine delayStatusWeapon = null;
        public bool isReloading;

        private void Start()
        {
            Init();
            player.gun = this;
        }

        protected void Init()
        {
            gunProperties.bulletRemainingInMage = gunSetting.bulletInMage;
            gunProperties.timeReload = gunSetting.timeReload;

            isReloading = false;
        }

        public void Shoot()
        {
            if (statusWeapon == StatusWeapon.Ready)
            {
                statusWeapon = StatusWeapon.Waiting;
                // Fire
                Fire();

                // reset
                if (delayStatusWeapon != null) StopCoroutine(delayStatusWeapon);
                delayStatusWeapon = StartCoroutine(ResetStatusWeapon());
            }
        }

        public void Fire()
        {
            if (gunProperties.bulletRemainingInMage > 0 && !isReloading)
            {
                gunProperties.bulletRemainingInMage--;

                CreateBullet();
                PlaySEShoot();
                PlayMuzzleFlash();
            }
            else
            {
                ReloadGun();
            }
        }

        public void CreateBullet()
        {
            // Debug.Log("BUMBUMCHIUCHIU");
            if (!player)
            {
                Debug.Log("Not player");
                return;
            }

            var b = Instantiate(gunSetting.bulletPrefab);
            b.transform.position = player.pointGun.position;

            b.Fire(Mathf.Sign(player.transform.localScale.x));
        }

        private void PlaySEShoot()
        {

        }

        private void PlayMuzzleFlash()
        {

        }

        public void ReloadGun()
        {
            if (isReloading) return;

            isReloading = true;

            Invoke(nameof(ReloadSuccess), gunProperties.timeReload);
        }

        private void ReloadSuccess()
        {
            isReloading = false;
            gunProperties.bulletRemainingInMage = gunSetting.bulletInMage;
        }

        protected virtual IEnumerator ResetStatusWeapon()
        {
            yield return new WaitForSeconds(gunSetting.fireRate);
            statusWeapon = StatusWeapon.Ready;
        }
    }
}
