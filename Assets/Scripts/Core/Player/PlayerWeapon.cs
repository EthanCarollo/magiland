using System.Collections;
using Data.Weapons;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData weapon;
    [SerializeField] private LayerMask enemyMask;
    private MeshRenderer weaponRenderer;
    private Material weaponMaterial;
    private AudioSource audioSource;
    private bool isShooting = false;

    // Gizmos raycast
    private Ray lastRay;               // 🔵 on stocke le dernier ray tiré
    private RaycastHit lastHit;        // 🔵 dernier impact
    private bool hasHit;

    void Awake()
    {
        weaponRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

        weaponMaterial = weaponRenderer.material;
        SetupWeapon(weapon);
    }

    void OnEnable()
    {
        if(InputController.Instance != null) 
            InputController.Instance.OnShoot += OnShoot;
    }

    void SetupWeapon(WeaponData weaponData)
    {
        weaponMaterial.SetTexture("_BaseMap", weaponData.idleFrames[0].frame.texture);
    }

    void ResetWeapon()
    {
        isShooting = false;
        SetupWeapon(weapon);
    }

    void OnShoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            weapon.Shoot(transform.position, transform.rotation);
            if (weapon.shootSound != null) audioSource.PlayOneShot(weapon.shootSound);

            // Shoot using raycast
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            lastRay = ray;
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
            {
                // Vérifier layer Enemy
                bool isEnemy = ((1 << hit.collider.gameObject.layer) & enemyMask) != 0;

                if (isEnemy)
                {
                    hasHit = true;
                    lastHit = hit;
                    hit.collider.GetComponent<EnemyBehaviour>()?.TakeDamage(weapon.damage);
                }
            }
            else
            {
                hasHit = false;
            }

            StartCoroutine(AnimateWeapon(weapon.animationFrames));
        }
    }

    IEnumerator AnimateWeapon(AnimationFrame[] frames)
    {
        for (int i = 0; i < frames.Length; i++)
        {
            weaponMaterial.SetTexture("_BaseMap", frames[i].frame.texture);
            yield return new WaitForSeconds(frames[i].duration);
        }

        ResetWeapon();
    }

    void OnDisable()
    {
        if(InputController.Instance != null) 
            InputController.Instance.OnShoot -= OnShoot;
    }

    void OnDrawGizmos()
    {
        // Si on a tiré, dessine le dernier rayon
        if (lastRay.direction != Vector3.zero)
        {
            Gizmos.color = Color.red;

            if (hasHit)
            {
                // Ligne jusqu’au point d’impact
                Gizmos.DrawLine(lastRay.origin, lastHit.point);
                // Sphère au point d’impact
                Gizmos.DrawSphere(lastHit.point, 0.1f);
            }
            else
            {
                // Ligne "infinie" (par ex. 100 unités)
                Gizmos.DrawLine(lastRay.origin, lastRay.origin + lastRay.direction * 100f);
            }
        }
    }
}
