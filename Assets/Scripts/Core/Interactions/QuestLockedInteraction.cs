
using Core.Controllers.Quest;
using UnityEngine;
using UnityEngine.Events;
namespace Core.Interactions
{
    [RequireComponent(typeof(ZoneEnter))]
    public abstract class QuestLockedInteractionZone<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private Canvas worldCanvasGui;

        protected abstract BaseQuestController<T> QuestController { get; }

        private ZoneEnter zoneEnter;
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

            Debug.Log("Setup interaction handle interact");
            InputController.Instance.OnInteract += HandleInteract;

            zoneEnter = GetComponent<ZoneEnter>();
            zoneEnter.OnEnterZone += OnEnter;
            zoneEnter.OnExitZone += OnExit;
        }

        private void OnDisable()
        {
            if (QuestController != null)
            {
                QuestController.OnQuestEnd -= UnlockZone;
            }

            InputController.Instance.OnInteract -= HandleInteract;

            zoneEnter.OnEnterZone -= OnEnter;
            zoneEnter.OnExitZone -= OnExit;
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

        void OnEnter()
        {
            playerInside = true;
            if (!isLocked && worldCanvasGui != null)
                worldCanvasGui.enabled = true;
        }

        void OnExit()
        {
            playerInside = false;
            if (worldCanvasGui != null)
                worldCanvasGui.enabled = false;
        }

        // Propriétés publiques pour vérifier l'état
        public bool IsZoneUnlocked => !isLocked;
        public bool IsPlayerInside => playerInside && !isLocked;
    }
}
