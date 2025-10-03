using Core.Controllers;
using Core.Controllers.Quest;
using Core.Scene;
using Core.UserInterface;
using Data.Weapons;
using UnityEngine;

namespace Core.Interactions
{
    public class BusSceneInteraction : QuestLockedInteractionZone<BusQuestController>
    {
        protected override BaseQuestController<BusQuestController> QuestController
        { get { return BusQuestController.Instance; } }

        protected override void Interact()
        {
            Debug.Log("Launch interaction");
            PlayerController playerController = PlayerController.Instance;
            WeaponData actualWeapon = FindAnyObjectByType<PlayerWeapon>()?.GetWeapon();
            SceneTransitor.Instance.LoadScene(2, (() =>
            {
                PlayerController.Instance.UpdateLife(playerController.life);
                PlayerController.Instance.transform.position = new Vector3(0, playerController.transform.position.y, 0);
                FindAnyObjectByType<PlayerWeapon>()?.SetNewWeapon(actualWeapon);
            }));
        }
    }
}