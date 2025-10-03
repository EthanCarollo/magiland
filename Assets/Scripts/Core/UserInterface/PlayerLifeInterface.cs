using Core.Controllers;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Core.UserInterface
{
    public class PlayerLifeInterface : MonoBehaviour
    {
        [SerializeField] private GameObject lifeUiObject;
        [SerializeField] private AnimationFrame[] deathAnimation;
        public static PlayerLifeInterface Instance;

        void Start()
        {
            Instance = this;
            UpdateLifeUI(PlayerController.Instance.life);
            PlayerController.Instance.LifeChanged += UpdateLifeUI;
        }

        void OnDestroy()
        {
            if (PlayerController.Instance != null)
                PlayerController.Instance.LifeChanged -= UpdateLifeUI;
        }

        public void UpdateLifeUI(int newLife)
        {
            int currentChildren = transform.childCount;

            if (newLife < currentChildren)
            {
                int toRemove = currentChildren - newLife;
                for (int i = 0; i < toRemove; i++)
                {
                    Transform heart = transform.GetChild(currentChildren - 1 - i);
                    StartCoroutine(PlayDeathAnimation(heart.gameObject));
                }
            }
            else if (newLife > currentChildren)
            {
                int toAdd = newLife - currentChildren;
                for (int i = 0; i < toAdd; i++)
                {
                    Instantiate(lifeUiObject, transform);
                }
            }
        }

        private IEnumerator PlayDeathAnimation(GameObject heart)
        {
            Image sr = heart.GetComponent<Image>();
            if (sr != null && deathAnimation.Length > 0)
            {
                foreach (var frame in deathAnimation)
                {
                    sr.sprite = frame.frame;
                    yield return new WaitForSeconds(frame.duration);
                }
            }
            Destroy(heart);
        }
    }
}
