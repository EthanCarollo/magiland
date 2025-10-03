using Data.Singleton;
using UnityEngine;

namespace Data.Sound
{
    [CreateAssetMenu(fileName = "UserInterfaceSoundData", menuName = "Sound/UserInterfaceSoundData")]
    public class UserInterfaceSoundData : SingletonScriptableObject<UserInterfaceSoundData>
    {
        [Header("Buttons Sound")]
        public AudioClip hoverButtonAudioClip;
        public AudioClip clickButtonAudioClip;
        public AudioClip openPopupAudioClip;
    }
}