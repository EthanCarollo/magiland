using System;
using Data.Enemy;
using Extensions;
using UnityEngine;

namespace Core.Enemy
{
    public abstract class BaseEnemyBehaviour : MonoBehaviour
    {
        [Header("References & Data")]
        [SerializeField] protected EnemyData enemy;
        [SerializeField] protected float currentLife;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        protected bool IsDead;
        
        public void Start()
        {
            currentLife = enemy.maxLife;
            OnUpdateUi();
        }
        
        public void Update() { }


        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            OnBeforeTakeDamage();
            currentLife -= damage;
            OnUpdateUi();
            if (currentLife <= 0)
            {
                Death();
            }
            OnUpdateUi();
        }
        
        protected abstract void OnBeforeTakeDamage();
        protected abstract void OnAfterTakeDamage();
        protected abstract void OnUpdateUi();
        
        protected void Death()
        {
            IsDead = true;
            if (enemy.deadBodies.Count > 0)
            {
                spriteRenderer.sprite = enemy.deadBodies.GetRandom();
            }
            LeanTween.delayedCall(4f, () =>
            {
                if (gameObject == null) return;
                try
                {
                    LeanTween.moveY(gameObject, transform.position.y + 200f, 2f)
                        .setEaseInOutSine()
                        .setOnComplete((() =>
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y + 200f, 0);
                        }));
                }
                catch (Exception e)
                {
                    Debug.LogError("Cannot update death sprite of BaseEnemy, maybe scene has been swapped");
                }
            });
            OnDeath();
        }

        protected abstract void OnDeath();
    }
}