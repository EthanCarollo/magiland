using System;
using Core.Controllers.Quest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UserInterface.Quest
{
    public class BossQuestInterface : BaseQuestInterface
    {
        protected override string QuestText
        {
            get { return "tue redbullanemie o plus vite"; }
        }

        protected override string EndQuestText
        {
            get { return "gg il es mor"; }
        }

        protected override BaseQuestController QuestController
        {
            get { return BossQuestController.Instance; }
        }
    }
}