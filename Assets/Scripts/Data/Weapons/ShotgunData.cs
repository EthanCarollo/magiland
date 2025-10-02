using UnityEngine;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "ShotgunData", menuName = "Weapons/ShotgunData")]
    public class ShotgunData : WeaponData
    {
        [Header("Shotgun Settings")]
        public int spread;
        public int bullets;
        
        public override void Shoot(Vector3 position, Quaternion rotation)
        {
            Debug.Log("Shotgun Fired");
        }
    }
}