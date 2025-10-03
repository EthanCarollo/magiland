using System;
using Core.Controllers.Quest;
using UnityEngine;

namespace Core.Bus
{
    public class WorldCanvasTrigger : MonoBehaviour
    {
        [SerializeField] private Collider triggerZone;       
        [SerializeField] private Canvas worldCanvasGui;     
        [SerializeField] private LayerMask targetLayers;
        private bool IsLocked = true;
        
        private void Awake()
        {
            if (worldCanvasGui != null)
                worldCanvasGui.enabled = false;
        }

        private void Start()
        {
            BusQuestController.Instance.OnQuestEnd += UnlockBus;
        }

        private void UnlockBus()
        {
            this.IsLocked = false;
            BusQuestController.Instance.OnQuestEnd -=  UnlockBus;
        }

        // Here, I really use a maximum of DRY for your eyes MR.Professor
        private void OnTriggerEnter(Collider other)
        {
            if (IsLocked) return;
            ShowOrHideWorldCanvasAndVerifyCollider(other, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsLocked) return;
            ShowOrHideWorldCanvasAndVerifyCollider(other, false);
        }

        private void ShowOrHideWorldCanvasAndVerifyCollider(Collider other, bool show)
        {
            if (IsInLayerMask(other.gameObject))
            {
                if (worldCanvasGui != null)
                    worldCanvasGui.enabled = show;
            }
        }

        private bool IsInLayerMask(GameObject obj)
        {
            return (targetLayers.value & (1 << obj.layer)) != 0;
        }
    }

}