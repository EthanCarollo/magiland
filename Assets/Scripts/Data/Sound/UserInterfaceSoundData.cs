using UnityEngine;

namespace Data.Sound
{
    [CreateAssetMenu(fileName = "UserInterfaceSoundData", menuName = "Sound/UserInterfaceSoundData")]
    public class UserInterfaceSoundData : ScriptableObject
    {
        public AudioClip hoverButtonAudioClip;
    }
}