using System;
using UnityEngine;

namespace Data.Weapons
{
    public abstract class WeaponData : ScriptableObject
    {
        public string weaponName;
        public int damage;
        [SerializeField] public AnimationFrame[] animationFrames;
        
        public abstract void Shoot(Vector3 position, Quaternion rotation);
    }
}

[Serializable]
public class AnimationFrame
{
    public Sprite frame;
    public float duration;
}