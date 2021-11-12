using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Quest quest;
    public GameObject questName, questItemReward, questMoneyReward;
    public int index;
    public GameObject scrollReference;

    public void AddQuest(Quest _quest)
    {
        quest = _quest;
        questName.GetComponent<TMP_Text>().text = _quest.questName;

        if (quest.itemReward != null)
        {
            questItemReward.GetComponent<TMP_Text>().text = _quest.itemReward.itemName;
        }

        questMoneyReward.GetComponent<TMP_Text>().text = _quest.moneyReward.ToString() + "G";

    }


    public virtual void SetHelpText()
    {
        if (!Engine.e.adventureLogReference.adventureLogScreenSet)
        {
            Engine.e.adventureLogReference.adventureLogScreenSet = true;
        }

        if (quest != null)
        {
            if (quest.isComplete)
            {
                Engine.e.adventureLogReference.questComplete.text = "Ready To Turn In!";
            }

            Engine.e.adventureLogReference.questDescription.text = quest.questDescription;

            if (quest.objectiveCount.Length == 3)
            {
                if (quest.objectiveGoal[0] != 0)
                {
                    Engine.e.adventureLogReference.questObjective1Count.text = quest.objectiveTarget[0] + " : " + quest.objectiveCount[0] + "/" + quest.objectiveGoal[0];
                    if (quest.objectiveCount[0] == quest.objectiveGoal[0])
                    {
                        Engine.e.adventureLogReference.questObjective1Count.fontStyle = FontStyles.Strikethrough;
                    }
                }

                if (quest.objectiveGoal[1] != 0)
                {
                    Engine.e.adventureLogReference.questObjective2Count.text = quest.objectiveTarget[1] + " : " + quest.objectiveCount[1] + "/" + quest.objectiveGoal[1];
                    if (quest.objectiveCount[1] == quest.objectiveGoal[1])
                    {
                        Engine.e.adventureLogReference.questObjective2Count.fontStyle = FontStyles.Strikethrough;
                    }
                }
                if (quest.objectiveGoal[2] != 0)
                {
                    Engine.e.adventureLogReference.questObjective3Count.text = quest.objectiveTarget[2] + " : " + quest.objectiveCount[2] + "/" + quest.objectiveGoal[2];
                    if (quest.objectiveCount[2] == quest.objectiveGoal[2])
                    {
                        Engine.e.adventureLogReference.questObjective3Count.fontStyle = FontStyles.Strikethrough;
                    }
                }
            }

            if (quest.objectiveCount.Length == 2)
            {
                if (quest.objectiveGoal[0] != 0)
                {
                    Engine.e.adventureLogReference.questObjective1Count.text = quest.objectiveTarget[0] + " : " + quest.objectiveCount[0] + "/" + quest.objectiveGoal[0];
                    if (quest.objectiveCount[0] == quest.objectiveGoal[0])
                    {
                        Engine.e.adventureLogReference.questObjective1Count.fontStyle = FontStyles.Strikethrough;
                    }
                }

                if (quest.objectiveGoal[1] != 0)
                {
                    Engine.e.adventureLogReference.questObjective2Count.text = quest.objectiveTarget[1] + " : " + quest.objectiveCount[1] + "/" + quest.objectiveGoal[1];
                    if (quest.objectiveCount[1] == quest.objectiveGoal[1])
                    {
                        Engine.e.adventureLogReference.questObjective2Count.fontStyle = FontStyles.Strikethrough;
                    }
                }
            }

            if (quest.objectiveCount.Length == 1)
            {
                if (quest.objectiveGoal[0] != 0)
                {
                    Engine.e.adventureLogReference.questObjective1Count.text = quest.objectiveTarget[0] + " : " + quest.objectiveCount[0] + "/" + quest.objectiveGoal[0];
                    if (quest.objectiveCount[0] == quest.objectiveGoal[0])
                    {
                        Engine.e.adventureLogReference.questObjective1Count.fontStyle = FontStyles.Strikethrough;
                    }
                }
            }
        }
        else
        {
            Engine.e.adventureLogReference.questDescription.text = string.Empty;
            Engine.e.adventureLogReference.questObjective1Count.text = string.Empty;
            Engine.e.adventureLogReference.questObjective2Count.text = string.Empty;
            Engine.e.adventureLogReference.questObjective3Count.text = string.Empty;
            Engine.e.adventureLogReference.questComplete.text = string.Empty;

        }
    }

    public virtual void ClearHelpText()
    {
        if (Engine.e.adventureLogReference.adventureLogScreenSet)
        {
            Engine.e.adventureLogReference.adventureLogScreenSet = false;
        }
        Engine.e.adventureLogReference.questDescription.text = string.Empty;
        Engine.e.adventureLogReference.questObjective1Count.text = string.Empty;
        Engine.e.adventureLogReference.questObjective2Count.text = string.Empty;
        Engine.e.adventureLogReference.questObjective3Count.text = string.Empty;
        Engine.e.adventureLogReference.questObjective1Count.fontStyle &= ~FontStyles.Strikethrough;
        Engine.e.adventureLogReference.questObjective2Count.fontStyle &= ~FontStyles.Strikethrough;
        Engine.e.adventureLogReference.questObjective3Count.fontStyle &= ~FontStyles.Strikethrough;
        Engine.e.adventureLogReference.questComplete.text = string.Empty;

    }

    public void ClearSlot()
    {

        quest = null;
        questName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        questItemReward.GetComponent<TMP_Text>().text = string.Empty;
        questMoneyReward.GetComponentInChildren<TMP_Text>().text = string.Empty;

    }
}

