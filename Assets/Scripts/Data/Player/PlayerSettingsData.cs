using Data.Singleton;
using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettingsData")]
    public class PlayerSettingsData : SingletonScriptableObject<PlayerSettingsData>
    {
        [Header("Sensibility")]
        public float mouseSensitivity = 50f;
        public float controllerSensitivity = 80f;
        
    }
}