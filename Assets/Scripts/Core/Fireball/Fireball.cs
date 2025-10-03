using Core.Controllers;
using UnityEngine;

namespace Core.Fireball
{
    public class Fireball : MonoBehaviour
    {
        public float speed = 20f;
        public LayerMask playerLayer;
        private Vector3 direction;
        private bool _didDamage = false;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip fireballSound;

        public void Launch(Vector3 targetPosition)
        {
            if(audioSource && fireballSound) audioSource.PlayOneShot(fireballSound);
            direction = (targetPosition - transform.position).normalized;
            Debug.Log(targetPosition);
        }

        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & playerLayer) != 0)
            {
                if (!_didDamage)
                {
                    _didDamage = true;
                    PlayerController.Instance.TakeDamage();
                }
            }
        }
    }
}