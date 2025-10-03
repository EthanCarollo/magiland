using UnityEngine;
using System.Collections.Generic;

namespace Data.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public GameObject enemyPrefab;
        public string enemyName;
        public float enemySpeed;
        public float enemyRange;
        public float enemyAttackCooldown;
        public int maxLife;
        public List<Sprite> deadBodies;
        public List<AudioClip> enemySounds;
        public AudioClip attackSound;
    }
}