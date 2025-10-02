using UnityEngine;

namespace Data.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public GameObject enemyPrefab;
        public string enemyName;
        public float enemySpeed;
        public int maxLife;
        public Sprite deadBody;
    }
}