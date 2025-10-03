using Core.Controllers;
using Core.Enemy;
using UnityEngine;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "ShotgunData", menuName = "Weapons/ShotgunData")]
    public class ShotGunData : WeaponData
    {
        public override void OnShoot(PlayerWeapon playerWeapon)
        {
            playerWeapon.shotgunParticle.Stop();
            playerWeapon.shotgunParticle.Play();
            PlayerController.Instance.ShakeCamera();
        }

        public override void OnShootHeld(PlayerWeapon playerWeapon)
        {
            
        }

        public override bool SupportsAutoFire()
        {
            return false;
        }
    }
}