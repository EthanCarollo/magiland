using UnityEngine;

namespace Core.Controllers.Quest
{
    public class BusQuestController : BaseQuestController<BusQuestController>
    {
        public override int AdvancementPointGoal
        {
            get { return 1; }
        }

        public void OnEnemyDied()
        {
            if (questEnd == false)
            {
                _actualAdvancement++;
                RaiseQuestAdvancement(ActualAdvancement, AdvancementPointGoal);
                if (ActualAdvancement >= AdvancementPointGoal) RaiseQuestEnd();
            }
        }
    }
}