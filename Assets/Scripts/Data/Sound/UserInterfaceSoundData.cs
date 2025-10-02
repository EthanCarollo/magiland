using UnityEngine;

namespace Data.Sound
{
    [CreateAssetMenu(fileName = "UserInterfaceSoundData", menuName = "Sound/UserInterfaceSoundData")]
    public class UserInterfaceSoundData : ScriptableObject
    {
        [Header("Buttons Sound")]
        public AudioClip hoverButtonAudioClip;
        public AudioClip clickButtonAudioClip;
    }
}