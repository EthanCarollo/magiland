using Core.Controllers;
using UnityEngine;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "ShotgunData", menuName = "Weapons/ShotgunData")]
    public class ShotgunData : WeaponData
    {
        [Header("Shotgun Settings")]
        public int spread;
        public int bullets;

        public override (Ray lastRay, RaycastHit? lastHit) Shoot(LayerMask hitMask, LayerMask passThroughMask)
        {
            PlayerController.Instance.ShakeCamera();
            
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
                // Exemple : appliquer des dégâts si c’est un ennemi
                hit.Value.collider.GetComponent<EnemyBehaviour>()?.TakeDamage(damage);
            }

            return (ray, hit);
        }
    }
}