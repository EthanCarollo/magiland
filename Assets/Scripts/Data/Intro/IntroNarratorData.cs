using UnityEngine;

namespace Data.Intro
{
    [CreateAssetMenu(fileName = "IntroNarrator", menuName = "Intro/IntroNarrator")]
    public class IntroNarratorData : ScriptableObject
    {
        public string text;
        public NarratorData narrator;
    }
}