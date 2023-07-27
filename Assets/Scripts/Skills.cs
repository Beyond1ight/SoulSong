using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{


    public float skillPower;
    public float skillCost;
    public string skillName;
    public string skillDescription;
    public float skillPointReturn = 0;
    public bool physicalDps, rangedDps, selfSupport, targetSupport, targetAll;
    public bool instantCast;
    public int skillIndex;
    public bool isKnown;
    public bool passiveSkill;
    public float health, mana, energy, haste, strength, intelligence, dropCostReduction, skillCostReduction, physicalDefense, lightningDefense, waterDefense, fireDefense,
    iceDefense, shadowDefense, holyDefense;

    public void SpriteDamageFlash()
    {
        GetComponent<DisplayedAnimationControl>().CallDamageFlash();
    }

    public void StatChange(int partyIndex)
    {
        // add bool for equipping skill to weapon slot (for stat addition / subtraction)
        Character targetChar = Engine.e.party[partyIndex].GetComponent<Character>();

        targetChar.maxHealth += Mathf.Round(targetChar.maxHealth * health);
        targetChar.maxMana += Mathf.Round(targetChar.maxMana * mana);
        targetChar.maxEnergy += Mathf.Round(targetChar.maxEnergy * energy);

        targetChar.strength += targetChar.strength * strength;
        targetChar.intelligence += targetChar.intelligence * intelligence;

        targetChar.physicalDefense += targetChar.physicalDefense * physicalDefense;
        targetChar.lightningDefense += targetChar.lightningDefense * lightningDefense;
        targetChar.waterDefense += targetChar.waterDefense * waterDefense;
        targetChar.fireDefense += targetChar.fireDefense * fireDefense;
        targetChar.iceDefense += targetChar.iceDefense * iceDefense;
        targetChar.shadowDefense += targetChar.shadowDefense * shadowDefense;
        targetChar.holyDefense += targetChar.holyDefense * holyDefense;

        targetChar.dropCostReduction += targetChar.dropCostReduction * dropCostReduction;
        targetChar.skillCostReduction += targetChar.skillCostReduction * skillCostReduction;

    }
}
