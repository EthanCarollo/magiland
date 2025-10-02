using System.Collections;
using Data.Weapons;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weapon;
    private MeshRenderer weaponRenderer;
    private Material weaponMaterial;
    private AudioSource audioSource;
    private bool isShooting = false;

    void Awake()
    {
        weaponRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

        weaponMaterial = weaponRenderer.material;
        SetupWeapon(weapon);
    }

    void OnEnable()
    {
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
        InputController.Instance.OnShoot -= OnShoot;
    }
}
