using Core.Controllers;
using UnityEngine;

[RequireComponent(typeof(ZoneEnter))]
public class WeaponShop : MonoBehaviour
{
    [SerializeField] private Canvas worldCanvasGui;
    [SerializeField] private WeaponDatabase weaponShopDatabase;
    private ZoneEnter zoneEnter;

    void Start()
    {
        zoneEnter = GetComponent<ZoneEnter>();
        zoneEnter.OnEnterZone += OnEnter;
        zoneEnter.OnExitZone += OnExit;

        if (worldCanvasGui != null)
            worldCanvasGui.enabled = false;
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
        PlayerController.Instance.SetNewWeapon(weaponShopDatabase.GetNextWeapon(PlayerController.Instance.GetCurrentWeapon()));
    }

    void OnDestroy()
    {
        zoneEnter.OnEnterZone -= OnEnter;
        zoneEnter.OnExitZone -= OnExit;
    }
}
