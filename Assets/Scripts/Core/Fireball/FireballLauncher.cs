using System;
using Core.Controllers;
using Core.Controllers.Quest;
using UnityEngine;

namespace Core.Fireball
{
    public class FireballLauncher : MonoBehaviour
    {
        public GameObject fireballPrefab;
        public float launchInterval = 2f;
        public bool launchFireball = true;

        private float timer;

        void Start()
        {
            if(BossQuestController.Instance) BossQuestController.Instance.OnQuestEnd += StopLaunchingFireball;
        }

        private void OnDisable()
        {
            if(BossQuestController.Instance) BossQuestController.Instance.OnQuestEnd -= StopLaunchingFireball;
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= launchInterval && launchFireball)
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

        
        public void StopLaunchingFireball()
        {
            launchFireball = false;
        }
    }
}