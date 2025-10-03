using Core.Controllers.Quest;
using Core.Scene;

namespace Core.Interactions
{
    public class BusSceneInteraction : QuestLockedInteractionZone<BusQuestController>
    {
        protected override BaseQuestController<BusQuestController> QuestController
        { get { return BusQuestController.Instance; } }

        protected override void Interact()
        {
            SceneTransitor.Instance.LoadScene(2);
        }
    }
}