using Core.Controllers;
using Core.Controllers.Quest;
using Core.Scene;

namespace Core.Interactions
{
    public class BossSceneInteraction : QuestLockedInteractionZone<BossQuestController>
    {
        protected override BaseQuestController<BossQuestController> QuestController
        { get { return BossQuestController.Instance; } }

        protected override void Interact()
        {
            SceneTransitor.Instance.LoadScene(0, (() =>
            {
                Destroy(PlayerController.Instance?.transform.root.gameObject);
            }));
        }
    }
}