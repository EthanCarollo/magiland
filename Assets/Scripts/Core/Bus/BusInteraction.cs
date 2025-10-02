using UnityEngine;

namespace Core.Bus
{
    public class WorldCanvasTrigger : MonoBehaviour
    {
        [SerializeField] private Collider triggerZone;       
        [SerializeField] private Canvas worldCanvasGui;     
        [SerializeField] private LayerMask targetLayers;

        private void Awake()
        {
            if (worldCanvasGui != null)
                worldCanvasGui.enabled = false;
        }

        // Here, I really use a maximum of DRY for your eyes MR.Professor
        private void OnTriggerEnter(Collider other)
        {
            ShowOrHideWorldCanvasAndVerifyCollider(other, true);
        }

        private void OnTriggerExit(Collider other)
        {
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