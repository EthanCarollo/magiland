using System;
using Core.Controllers.Quest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UserInterface.Quest
{
    public class BusQuestInterface : BaseQuestInterface
    {
        protected override string QuestText
        {
            get { return $"Fau unlck lbus, et pr ça fau tuer {BusQuestController.Instance.AdvancementPointGoal} mec"; ; }
        }

        protected override string EndQuestText
        {
            get { return "gg i son mor, mtn le bus est unlock, vzy"; }
        }

        protected override BaseQuestController QuestController
        {
            get { return BusQuestController.Instance; }
        }
    }
}