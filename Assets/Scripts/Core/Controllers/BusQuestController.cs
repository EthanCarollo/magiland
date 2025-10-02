using UnityEngine;

namespace Core.Quest
{
    public class BusQuestController : BaseController<BusQuestController>
    {
        public delegate void QuestEnd();
        public event QuestEnd OnQuestEnd;

        // Serializer for the debug just
        [SerializeField] private int actualKilledEnemy = 0;
        [SerializeField] private int maxKilledEnemy = 10;

        public void OnEnemyDied()
        {
            actualKilledEnemy++;
            if (actualKilledEnemy >= maxKilledEnemy)
            {
                OnQuestEnd?.Invoke();
            }
        }
    }
}