using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using Data.Weapons;

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
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private Camera _camera;

        private Vector3 originalPos;
        private Coroutine shakeCoroutine;
        
        public int life = 5;
        public delegate void OnLifeChange(int newLife);
        public event OnLifeChange LifeChanged;
        private bool shakeCameraEnabled = true;

        public void Start()
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.OnGamePaused += ToggleEnablePlayer;
                GameController.Instance.OnGameOver += DisablePlayer;
            }
        }

        public void OnDisable()
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.OnGamePaused -= ToggleEnablePlayer;
                GameController.Instance.OnGameOver -= DisablePlayer;
            }
        }

        public void TakeDamage(int damage = 1)
        {
            UpdateLife(life - damage);
        }

        public void UpdateLife(int newLife)
        {
            life = Mathf.Max(newLife, 0);
            LifeChanged?.Invoke(life);
        }
        
        public void ShakeCamera(float shakeMagnitude = 0.3f, float shakeDuration = 0.1f)
        {
            if (!shakeCameraEnabled) return;
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            shakeCoroutine = StartCoroutine(DoCameraShake(shakeMagnitude, shakeDuration));
        }

        private IEnumerator DoCameraShake(float shakeMagnitude, float duration)
        {
            originalPos = _camera.transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
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

        public void ToggleMovementActivation()
        {
            _playerMovementSimpleRB.enabled = !_playerMovementSimpleRB.enabled;
            _simpleMouseLook.enabled = !_simpleMouseLook.enabled;
            _weaponSmoothFollow.enabled = !_weaponSmoothFollow.enabled;
        }

        public void SetNewWeapon(WeaponData weapon)
        {
            _playerWeapon.SetNewWeapon(weapon);
        }

        public WeaponData GetCurrentWeapon()
        {
            return _playerWeapon.weapon;
        }

        void DisablePlayer()
        {
            _playerMovementSimpleRB.enabled = false;
            _simpleMouseLook.enabled = false;
            _weaponSmoothFollow.enabled = false;
            _playerWeapon.enabled = false;
            shakeCameraEnabled = false;
        }

        void ToggleEnablePlayer(bool disable)
        {
            if(_playerMovementSimpleRB != null) _playerMovementSimpleRB.enabled = !disable;
            if(_simpleMouseLook != null) _simpleMouseLook.enabled = !disable;
            if(_weaponSmoothFollow != null) _weaponSmoothFollow.enabled = !disable;
            if(_playerWeapon != null) _playerWeapon.enabled = !disable;
            shakeCameraEnabled = !disable;
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
