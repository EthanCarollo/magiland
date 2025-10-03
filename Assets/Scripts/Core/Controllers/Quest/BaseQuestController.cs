using UnityEngine;

namespace Core.Controllers.Quest
{
    public abstract class BaseQuestController<T> : BaseController<T> where T : Object
    {
        public delegate void QuestEnd();
        public event QuestEnd OnQuestEnd;
        
        public delegate void QuestAdvancement(int progress, int maximum);
        public event QuestAdvancement OnQuestAdvancement;
        
        protected int _actualAdvancement = 0;
        public int ActualAdvancement { get => _actualAdvancement; }
        public abstract int AdvancementPointGoal { get; }
        
        [SerializeField] protected bool questEnd = false;

        /**
         * If you read this, I know I actually just can add a line in my condition on line 43, I
         * just wanted to see if you would really read this boring code
         *
         * and make it, maybe, a little bit fun for your eyes.
         */
        public void Start()
        {
            OnQuestEnd += EndQuest;
        }

        public void EndQuest()
        {
            OnQuestEnd -= EndQuest;
            questEnd = true;
        }
        
        protected void RaiseQuestAdvancement(int progress, int maximum)
        {
            OnQuestAdvancement?.Invoke(progress, maximum);
        }

        protected void RaiseQuestEnd()
        {
            OnQuestEnd?.Invoke();
        }
        
        
        public void AdvanceQuest()
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