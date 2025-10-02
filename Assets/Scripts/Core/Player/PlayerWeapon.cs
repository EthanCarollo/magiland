using System.Collections;
using Data.Weapons;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData weapon;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private LayerMask passThroughMask;
    [SerializeField] private Image crossHairImage;
    public ParticleSystem shotgunParticle;
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
        SetupCrosshair();
    }

    void SetupCrosshair()
    {
        if (this.crossHairImage != null)
        {
            if (this.weapon.crossHair != null)
            {
                this.crossHairImage.enabled = true;
                this.crossHairImage.sprite = this.weapon.crossHair;
            }
            else
            {
                this.crossHairImage.enabled = false;
            }
        }
    }

    void ResetWeapon()
    {
        isShooting = false;
        SetupWeapon(weapon);
    }

    void OnShoot()
    {
        if (isShooting) return;
    
        isShooting = true;

        (Ray ray, RaycastHit? hit) = weapon.Shoot(hitMask, passThroughMask, this);
        lastRay = ray;

        if (hit.HasValue)
        {
            SpriteRenderer spriteRenderer = hit.Value.collider.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                LeanTween.cancel(spriteRenderer.gameObject);
                LeanTween.color(spriteRenderer.gameObject, Color.red, 0.2f)
                    .setEaseInOutSine()
                    .setOnComplete(() =>
                    {
                        LeanTween.color(spriteRenderer.gameObject, Color.white, 0.8f)
                            .setEaseInOutSine();
                    });
            }

            lastHit = hit.Value;
        }

        if (weapon.shootSound != null)
            audioSource.PlayOneShot(weapon.shootSound);

        StartCoroutine(AnimateWeapon(weapon.animationFrames));
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
