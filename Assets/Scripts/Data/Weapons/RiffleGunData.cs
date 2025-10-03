using Core.Controllers;
using Core.Enemy;
using UnityEngine;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "RiffleGunData", menuName = "Weapons/RiffleGunData")]
    public class RiffleGunData : WeaponData
    {
        [Header("RiffleGun Parameter")] public int salvo;
        
        public override void OnShoot(PlayerWeapon playerWeapon)
        {
            
        }

        public override void OnShootHeld(PlayerWeapon playerWeapon)
        {
            PlayerController.Instance.ShakeCamera(0.1f, 0.07f);
            playerWeapon.rifflegunParticle.Play();
        }

        public override bool SupportsAutoFire()
        {
            return true;
        }
    }
}