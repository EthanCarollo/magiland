using UnityEngine;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Weapons/GunData")]
    public class GunData : WeaponData
    {
        public override void Shoot(Vector3 position, Quaternion rotation)
        {
            Debug.Log("Shotgun Fired");
        }
    }
}