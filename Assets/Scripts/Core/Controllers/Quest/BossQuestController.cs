using UnityEngine;

namespace Core.Controllers.Quest
{
    public class BossQuestController : BaseQuestController
    {
        public override int AdvancementPointGoal {
            get { return 1; }
        }
        
        public void OnBossDead()
        {
            if (questEnd == false)
            {
                RaiseQuestAdvancement(1, AdvancementPointGoal);
                RaiseQuestEnd();
            }
        }
    }
}