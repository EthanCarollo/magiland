using Core.Controllers.Quest;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Core.Enemy
{
    public class BossEnemyBehaviour : BaseEnemyBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyNameText;
        [SerializeField] private Slider enemyLifeSlider;

        public void Start()
        {
            base.Start();
        }

        protected override void OnBeforeTakeDamage()
        {
            
        }

        protected override void OnAfterTakeDamage()
        {
            
        }

        protected override void OnUpdateUi()
        {
            enemyLifeSlider.maxValue = enemy.maxLife;
            enemyNameText.text = enemy.enemyName;
        
            LeanTween.cancel(enemyLifeSlider.gameObject);
            LeanTween.value(enemyLifeSlider.gameObject, enemyLifeSlider.value, currentLife, 0.3f)
                .setOnUpdate((float val) =>
                {
                    enemyLifeSlider.value = val;
                })
                .setEaseOutCubic();
        }

        
        protected override void OnDeath()
        {
            this.GetComponent<Collider>().enabled = false;
            if (BossQuestController.Instance != null)
                BossQuestController.Instance.AdvanceQuest();
        }

    }
}