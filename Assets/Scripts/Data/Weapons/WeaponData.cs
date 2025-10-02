using System;
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
        
        public abstract (Ray lastRay, RaycastHit? lastHit) Shoot(LayerMask hitMask, LayerMask passThroughMask, PlayerWeapon playerWeapon);
    }
}
