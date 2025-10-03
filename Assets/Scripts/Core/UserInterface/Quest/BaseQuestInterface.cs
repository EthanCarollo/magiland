using System;
using Core.Controllers.Quest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Core.UserInterface.Quest
{
    public abstract class BaseQuestInterface<T> : MonoBehaviour where T : BaseQuestController<T>
    {
        [SerializeField] protected GameObject QuestContainer;
        [SerializeField] protected TextMeshProUGUI QuestDescription;
        [SerializeField] protected Slider QuestProgressSlider;
        
        protected abstract string QuestText { get; }
        protected abstract string EndQuestText { get; }
        protected abstract BaseQuestController<T> QuestController { get; }
        
        
        private void Start()
        {
            QuestDescription.text = QuestText;
            QuestProgressSlider.value = 0;
            QuestProgressSlider.maxValue = QuestController.AdvancementPointGoal;
            if (QuestController != null)
            {
                QuestController.OnQuestAdvancement += UpdateUI;
                QuestController.OnQuestEnd += EndQuest;
            }
        }

        public void OnDisable()
        {
            if (QuestController != null)
            {
                QuestController.OnQuestAdvancement -= UpdateUI;
                QuestController.OnQuestEnd -= EndQuest;
            }
        }

        public void EndQuest()
        {
            QuestDescription.text = EndQuestText;
        }

        public void UpdateUI(int actualProgress, int maxProgress)
        {
            LeanTween.value(QuestProgressSlider.gameObject, QuestProgressSlider.value, actualProgress, 0.5f)
                .setOnUpdate((float val) => { QuestProgressSlider.value = val; })
                .setEaseOutQuad();

            LeanTween.cancel(QuestContainer);
            LeanTween.scale(QuestContainer, Vector3.one * 1.1f, 0.1f).setLoopPingPong(1);
        }
    }
}