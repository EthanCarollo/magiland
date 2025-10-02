using UnityEngine;

namespace Data.Weapons
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Weapons/GunData")]
    public class GunData : WeaponData
    {
        public override (Ray lastRay, RaycastHit? lastHit) Shoot(LayerMask hitMask, LayerMask passThroughMask, PlayerWeapon playerWeapon)
        {
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