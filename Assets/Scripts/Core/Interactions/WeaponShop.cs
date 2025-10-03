using Core.Controllers;
using Data.Weapons;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ZoneEnter))]
public class WeaponShop : MonoBehaviour
{
    [SerializeField] private Canvas worldCanvasGui;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private WeaponDatabase weaponShopDatabase;
    private ZoneEnter zoneEnter;

    void Start()
    {
        zoneEnter = GetComponent<ZoneEnter>();
        zoneEnter.OnEnterZone += OnEnter;
        zoneEnter.OnExitZone += OnExit;

        if (worldCanvasGui != null)
            worldCanvasGui.enabled = false;

        weaponIcon.sprite = GetNextWeapon().weaponIcon;
    }

    void OnEnter()
    {
        if (worldCanvasGui != null)
            worldCanvasGui.enabled = true;

        InputController.Instance.OnInteract += Interact;
    }

    void OnExit()
    {
        if (worldCanvasGui != null)
            worldCanvasGui.enabled = false;

        InputController.Instance.OnInteract -= Interact;
    }

    void Interact()
    {
        PlayerController.Instance.SetNewWeapon(GetNextWeapon());
        weaponIcon.sprite = GetNextWeapon().weaponIcon;
    }

    public WeaponData GetNextWeapon()
    {
        return weaponShopDatabase.GetNextWeapon(PlayerController.Instance.GetCurrentWeapon());
    }

    void OnDestroy()
    {
        zoneEnter.OnEnterZone -= OnEnter;
        zoneEnter.OnExitZone -= OnExit;
    }
}
