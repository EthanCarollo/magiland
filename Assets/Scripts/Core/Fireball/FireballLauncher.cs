using Core.Controllers;
using UnityEngine;

namespace Core.Fireball
{
    public class FireballLauncher : MonoBehaviour
    {
        public GameObject fireballPrefab;
        public float launchInterval = 2f;

        private float timer;

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= launchInterval)
            {
                LaunchFireball();
                timer = 0f;
            }
        }

        public void LaunchFireball()
        {
            if (fireballPrefab != null && PlayerController.Instance != null)
            {
                GameObject fireballObj = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                Fireball fireball = fireballObj.GetComponent<Fireball>();
                if (fireball != null)
                {
                    fireball.Launch(PlayerController.Instance.transform.position);
                }
            }
        }
    }
}