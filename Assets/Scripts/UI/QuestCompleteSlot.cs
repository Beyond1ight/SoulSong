using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class QuestCompleteSlot : MonoBehaviour
{
    public Quest quest;
    public GameObject questName, questItemReward, questMoneyReward;
    public int index;
    public GameObject scrollReference;

    public void AddQuest(Quest _quest)
    {
        quest = _quest;
        questName.GetComponent<TMP_Text>().text = _quest.questName;
        //questItemReward.GetComponent<TMP_Text>().text = _quest.itemReward.itemName;
        //questMoneyReward.GetComponent<TMP_Text>().text = _quest.moneyReward.ToString() + "G";
    }


    public void ClearSlot()
    {

        quest = null;
        questName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        questItemReward.GetComponent<TMP_Text>().text = string.Empty;
        questMoneyReward.GetComponentInChildren<TMP_Text>().text = string.Empty;

    }
    public void SetHelpText()
    {

        if (!Engine.e.adventureLogReference.adventureLogScreenCompletedQuestsSet)
        {
            Engine.e.adventureLogReference.adventureLogScreenCompletedQuestsSet = true;
        }

        if (quest != null)
        {
            Engine.e.adventureLogReference.questDescription.text = quest.questDescription;
            Engine.e.adventureLogReference.questComplete.text = "Complete!";
        }
        else
        {
            Engine.e.adventureLogReference.questDescription.text = string.Empty;
            Engine.e.adventureLogReference.questComplete.text = string.Empty;
        }
    }

    public void ClearHelpText()
    {
        if (Engine.e.adventureLogReference.adventureLogScreenCompletedQuestsSet)
        {
            Engine.e.adventureLogReference.adventureLogScreenCompletedQuestsSet = false;
        }
        Engine.e.adventureLogReference.questDescription.text = string.Empty;
        Engine.e.adventureLogReference.questComplete.text = string.Empty;

    }
}


