using UnityEngine;

namespace Data.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int maxLife;
    }
}