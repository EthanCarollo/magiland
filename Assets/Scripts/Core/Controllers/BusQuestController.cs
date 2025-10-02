using UnityEngine;

namespace Core.Quest
{
    public class BusQuestController : BaseController<BusQuestController>
    {
        public delegate void QuestEnd();
        public event QuestEnd OnQuestEnd;
        
        public delegate void QuestAdvancement(int progress, int maximum);
        public event QuestAdvancement OnQuestAdvancement;

        // Serializer for the debug just
        private int _actualKilledEnemy = 0;
        public int actualKilledEnemy { get => _actualKilledEnemy; }
        private int _maxKilledEnemy = 10;
        public int maxKilledEnemy { get => _maxKilledEnemy; }
        [SerializeField] private bool questEnd = false;

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
        
        public void OnEnemyDied()
        {
            if (questEnd == false)
            {
                _actualKilledEnemy++;
                OnQuestAdvancement?.Invoke(actualKilledEnemy, maxKilledEnemy);
                if (actualKilledEnemy >= maxKilledEnemy) OnQuestEnd?.Invoke();
            }
        }
    }
}