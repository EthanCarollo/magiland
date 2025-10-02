using System;
using Core.Quest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UserInterface
{
    public class BusQuestInterface : MonoBehaviour
    {
        [SerializeField] private GameObject busQuestContainer;
        [SerializeField] private TextMeshProUGUI busQuestDescription;
        [SerializeField] private Slider busQuestProgressSlider;

        private void Start()
        {
            busQuestDescription.text =
                $"Fau unlck lbus, et pr ça fau tuer {BusQuestController.Instance.maxKilledEnemy} mec";
            busQuestProgressSlider.value = 0;
            busQuestProgressSlider.maxValue = BusQuestController.Instance.maxKilledEnemy;
            if (BusQuestController.Instance != null)
                BusQuestController.Instance.OnQuestAdvancement += UpdateUI;
        }

        public void OnDisable()
        {
            if (BusQuestController.Instance != null)
                BusQuestController.Instance.OnQuestAdvancement -= UpdateUI;
        }

        public void UpdateUI(int actualProgress, int maxProgress)
        {
            LeanTween.value(busQuestProgressSlider.gameObject, busQuestProgressSlider.value, actualProgress, 0.5f)
                .setOnUpdate((float val) => { busQuestProgressSlider.value = val; })
                .setEaseOutQuad();

            LeanTween.cancel(busQuestContainer);
            LeanTween.scale(busQuestContainer, Vector3.one * 1.1f, 0.1f).setLoopPingPong(1);
        }
    }
}