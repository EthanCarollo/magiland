using System;
using System.Collections;
using System.Collections.Generic;
using Data.Intro;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Controllers
{
    public class IntroController : BaseController<IntroController>
    {
        [SerializeField] private List<IntroNarratorData> introductions;

        public GameObject introPanel;
        public TextMeshProUGUI introText;
        public TextMeshProUGUI narratorNameText;
        public Image narratorImage;
        public AudioSource audioSource;

        [SerializeField] private float typingSpeed = 0.05f;

        private Coroutine typingCoroutine;
        private Coroutine talkingCoroutine;
        private Sprite currentNarratorSprite;

        public void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            introPanel.SetActive(false);
            StartIntroduction(introductions.GetRandom());
        }

        private void StartIntroduction(IntroNarratorData introNarrator)
        {
            introPanel.SetActive(true);
            narratorNameText.text = introNarrator.narrator.narratorName;
            currentNarratorSprite = introNarrator.narrator.narratorSprite;

            if (talkingCoroutine != null)
                StopCoroutine(talkingCoroutine);

            talkingCoroutine = StartCoroutine(PlayTalkingAnimation(introNarrator.narrator.narratorTalkingAnimationFrames));

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeText(introNarrator.text, introNarrator.narrator.narratorVoice));
        }

        private IEnumerator TypeText(string textToType, AudioClip voiceClip)
        {
            introText.text = "";

            if (voiceClip != null && audioSource != null)
            {
                audioSource.clip = voiceClip;
                audioSource.loop = true;
                audioSource.Play();
            }

            foreach (char letter in textToType)
            {
                introText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            if (talkingCoroutine != null)
                StopCoroutine(talkingCoroutine);

            narratorImage.sprite = currentNarratorSprite;

            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();
        }

        private IEnumerator PlayTalkingAnimation(AnimationFrame[] frames)
        {
            int index = 0;

            while (true)
            {
                if (frames.Length == 0) yield break;

                narratorImage.sprite = frames[index].frame;
                yield return new WaitForSeconds(frames[index].duration);

                index = (index + 1) % frames.Length;
            }
        }

        public void ClickedOnIntro()
        {
            introPanel.SetActive(false);

            if (talkingCoroutine != null)
                StopCoroutine(talkingCoroutine);

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            narratorImage.sprite = currentNarratorSprite;

            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
