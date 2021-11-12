using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Quest", menuName = "Quests/New Quest", order = 0)]
public class Quest : ScriptableObject
{
    public string questName, questDescription;
    public int[] objectiveCount, objectiveGoal;
    public string[] objectiveTarget; // Assuming there is a "target" (i.e. a monster to kill, an item to retrieve) this is mainly for the Quest Log to be more specific for "Objective Count"
    public int moneyReward;
    public Item itemReward;
    public float experienceReward;
    public bool isComplete, inAdventureLog, turnedIn;
    public string[] questDialogue, questCompleteDialogue;

    public void ClearQuest()
    {
        isComplete = false;
        inAdventureLog = false;
        turnedIn = false;
        for (int i = 0; i < objectiveCount.Length; i++)
        {
            objectiveCount[i] = 0;
        }
    }

    public void CompleteQuest()
    {
        isComplete = true;
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

