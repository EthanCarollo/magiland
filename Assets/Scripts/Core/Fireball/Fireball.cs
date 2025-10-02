using UnityEngine;

namespace Core.Fireball
{
    public class Fireball : MonoBehaviour
    {
        public float speed = 20f;
        private Vector3 direction;

        public void Launch(Vector3 targetPosition)
        {
            direction = (targetPosition - transform.position).normalized;
            Debug.Log(targetPosition);
        }

        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}