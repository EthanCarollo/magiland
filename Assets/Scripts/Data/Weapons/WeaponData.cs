using System;
using Core.Enemy;
using UnityEngine;

namespace Data.Weapons
{
    public abstract class WeaponData : ScriptableObject
    {
        [Header("Weapon Settings")]
        public string weaponName;
        public int damage;
        public AnimationFrame[] idleFrames;
        public AnimationFrame[] animationFrames;
        public AudioClip shootSound;
        public Sprite crossHair;

        public abstract void OnShoot(PlayerWeapon playerWeapon);
        public abstract void OnShootHeld(PlayerWeapon playerWeapon);
        public abstract bool SupportsAutoFire();

        public (Ray lastRay, RaycastHit? lastHit) Shoot(LayerMask hitMask, LayerMask passThroughMask, PlayerWeapon playerWeapon)
        {
            OnShoot(playerWeapon);
            
            var cam = Camera.main;
            if (cam == null) return (default, null);

            var (ray, hit, blocked) = RaycastSelective.Raycast(
                cam.transform.position,
                cam.transform.forward,
                hitMask,
                passThroughMask,
                QueryTriggerInteraction.Ignore
            );

            if (hit.HasValue)
            {
                hit.Value.collider.GetComponent<BaseEnemyBehaviour>()?.TakeDamage(damage);
            }

            return (ray, hit);
        }

        public (Ray lastRay, RaycastHit? lastHit) ShootHeld(LayerMask hitMask, LayerMask passThroughMask, PlayerWeapon playerWeapon)
        {
            OnShootHeld(playerWeapon);
            
            var cam = Camera.main;
            if (cam == null) return (default, null);

            var (ray, hit, blocked) = RaycastSelective.Raycast(
                cam.transform.position,
                cam.transform.forward,
                hitMask,
                passThroughMask,
                QueryTriggerInteraction.Ignore
            );

            if (hit.HasValue)
            {
                hit.Value.collider.GetComponent<BaseEnemyBehaviour>()?.TakeDamage(damage);
            }

            return (ray, hit);
        }
    }
}