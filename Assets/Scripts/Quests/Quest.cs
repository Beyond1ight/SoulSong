using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Quest", menuName = "Quests/New Quest", order = 0)]
public class Quest : ScriptableObject
{
    public string questName;
    public string questDescription;
    public int moneyReward;
    public Item itemReward;
    public float experienceReward;
    public bool questComplete;
    public string[] questDialogue;
    public string[] questCompleteDialogue;
    public bool inAdventureLog;

    public void ClearQuest()
    {
        questComplete = false;
        inAdventureLog = false;
    }

    public void CompleteQuest()
    {
        questComplete = true;
        Engine.e.adventureLogReference.AddQuestToCompleteQuestLog(this);
    }
    public void AddRewardsToPartyInventory()
    {
        if (itemReward != null)
        {
            Engine.e.partyInventoryReference.AddItemToInventory(itemReward);
        }

        Engine.e.GiveQuestExperiencePoints(experienceReward);

        Engine.e.partyMoney += moneyReward;
    }
}

