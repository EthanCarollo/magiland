using Core.Controllers;
using Core.Controllers.Quest;
using Core.Scene;
using Core.UserInterface;
using Data.Weapons;

namespace Core.Interactions
{
    public class BusSceneInteraction : QuestLockedInteractionZone<BusQuestController>
    {
        protected override BaseQuestController<BusQuestController> QuestController
        { get { return BusQuestController.Instance; } }

        protected override void Interact()
        {
            int actualLife = PlayerController.Instance.life;
            WeaponData actualWeapon = FindAnyObjectByType<PlayerWeapon>()?.GetWeapon();
            SceneTransitor.Instance.LoadScene(2, (() =>
            {
                PlayerController.Instance.UpdateLife(actualLife);
                FindAnyObjectByType<PlayerWeapon>()?.SetNewWeapon(actualWeapon);
            }));
        }
    }
}