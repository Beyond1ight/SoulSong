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
    public bool questComplete;
    public string[] questDialogue;

}

