using UnityEngine;

namespace Data.Intro
{
    [CreateAssetMenu(fileName = "Narrator", menuName = "Intro/Narrator")]
    public class NarratorData : ScriptableObject
    {
        public string narratorName;
        public Sprite narratorSprite;
        public AnimationFrame[] narratorTalkingAnimationFrames;
        public AudioClip narratorVoice;
    }
}