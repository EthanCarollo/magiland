
using Core.Controllers.Quest;
using UnityEngine;
using UnityEngine.Events;
namespace Core.Interactions
{
    public abstract class QuestLockedInteractionZone<T> : MonoBehaviour where T: MonoBehaviour
    {
        [SerializeField] private Collider triggerZone;       
        [SerializeField] private Canvas worldCanvasGui;     
        [SerializeField] private LayerMask targetLayers;
        
        [SerializeField] protected abstract BaseQuestController<T> QuestController { get; }
        
        private bool isLocked = true;
        private bool playerInside = false;

        private void Awake()
        {
            if (worldCanvasGui != null)
                worldCanvasGui.enabled = false;
        }

        private void Start()
        {
            if (QuestController != null)
            {
                QuestController.OnQuestEnd += UnlockZone;
            }
            else
            {
                Debug.LogWarning("QuestLockedInteractionZone: Aucun quest controller assigné!");
            }
            
            InputController.Instance.OnInteract += HandleInteract;
        }

        private void OnDisable()
        {
            if (QuestController != null)
            {
                QuestController.OnQuestEnd -= UnlockZone;
            }
            
            InputController.Instance.OnInteract -= HandleInteract;
        }

        private void UnlockZone()
        {
            isLocked = false;
            if (QuestController != null)
            {
                QuestController.OnQuestEnd -= UnlockZone;
            }
            
            // Si le joueur est déjà dans la zone, afficher le GUI
            if (playerInside && worldCanvasGui != null)
            {
                worldCanvasGui.enabled = true;
            }
        }

        private void HandleInteract()
        {
            if (playerInside && !isLocked)
            {
                Interact();
            }
        }

        protected abstract void Interact();

        private void OnTriggerEnter(Collider other)
        {
            if (IsInLayerMask(other.gameObject))
            {
                playerInside = true;
                
                if (!isLocked && worldCanvasGui != null)
                    worldCanvasGui.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
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
        
        // Propriétés publiques pour vérifier l'état
        public bool IsZoneUnlocked => !isLocked;
        public bool IsPlayerInside => playerInside && !isLocked;
    }
}
