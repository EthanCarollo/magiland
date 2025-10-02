using UnityEngine;
using System.Collections;

/**
 * I know you dont like this, but it is so useful in our case
 * forgive us please.
 *
 * please
 *
 * please
 */
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Controllers
{
    public class PlayerController : BaseController<PlayerController>
    {
        [SerializeField] private SimpleMouseLook _simpleMouseLook;
        [SerializeField] private WeaponSmoothFollow _weaponSmoothFollow;
        [SerializeField] private PlayerMovementPhysics _playerMovementSimpleRB;
        [SerializeField] private Camera _camera;

        [Header("Shake Settings")]
        [SerializeField] private float shakeDuration = 0.3f;
        [SerializeField] private float shakeMagnitude = 0.1f;

        private Vector3 originalPos;
        private Coroutine shakeCoroutine;
        
        public int life = 5;
        public delegate void OnLifeChange(int newLife);
        public event OnLifeChange LifeChanged;

        public void TakeDamage(int damage = 1)
        {
            life -= damage;
            LifeChanged?.Invoke(life);
        }
        
        public void ShakeCamera()
        {
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            shakeCoroutine = StartCoroutine(DoCameraShake());
        }

        private IEnumerator DoCameraShake()
        {
            originalPos = _camera.transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
                float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

                _camera.transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            _camera.transform.localPosition = originalPos;
            shakeCoroutine = null;
        }

#if UNITY_EDITOR
        [ContextMenu("Editor Take Damage")]
        private void EditorTakeDamage()
        {
            TakeDamage(1);
        }
#endif
    }
}
