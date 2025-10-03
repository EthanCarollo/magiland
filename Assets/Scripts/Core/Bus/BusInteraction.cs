using System;
using Core.Controllers.Quest;
using Core.Scene;
using UnityEngine;

namespace Core.Bus
{
    public class BusInteraction : MonoBehaviour
    {
        [SerializeField] private Collider triggerZone;       
        [SerializeField] private Canvas worldCanvasGui;     
        [SerializeField] private LayerMask targetLayers;
        private bool IsLocked = true;
        private bool playerInside = false;

        private void Awake()
        {
            if (worldCanvasGui != null)
                worldCanvasGui.enabled = false;
        }

        private void OnDisable()
        {
            InputController.Instance.OnInteract -= HandleInteract;
        }

        private void Start()
        {
            InputController.Instance.OnInteract += HandleInteract;
            BusQuestController.Instance.OnQuestEnd += UnlockBus;
        }

        private void UnlockBus()
        {
            this.IsLocked = false;
            BusQuestController.Instance.OnQuestEnd -= UnlockBus;
        }

        private void HandleInteract()
        {
            if (playerInside && !IsLocked)
            {
                SceneTransitor.Instance.LoadScene(2);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsLocked) return;
            if (IsInLayerMask(other.gameObject))
            {
                playerInside = true;
                if (worldCanvasGui != null)
                    worldCanvasGui.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsLocked) return;
            if (IsInLayerMask(other.gameObject))
            {
                playerInside = false;
                if (worldCanvasGui != null)
                    worldCanvasGui.enabled = false;
            }
        }

        private bool IsInLayerMask(GameObject obj)
        {
            return (targetLayers.value & (1 << obj.layer)) != 0;
        }
    }
}