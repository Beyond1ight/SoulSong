using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{


    public float skillPower;
    public float skillCost;
    public float skillCostToUnlock;
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
    public bool equipping, grieveEquipped, macEquipped, fieldEquipped, riggsEquipped, solaceEquipped, blueEquipped;

    public void SpriteDamageFlash()
    {
        GetComponent<DisplayedAnimationControl>().CallDamageFlash();
    }

    public void StatChange(int partyIndex)
    {
        Character targetChar = Engine.e.party[partyIndex].GetComponent<Character>();

        if (equipping)
        {
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
        else
        {
            targetChar.maxHealth -= Mathf.Round(targetChar.maxHealthBase * health);
            targetChar.maxMana -= Mathf.Round(targetChar.maxManaBase * mana);
            targetChar.maxEnergy -= Mathf.Round(targetChar.maxEnergyBase * energy);

            targetChar.strength -= targetChar.strength * strength;
            targetChar.intelligence -= targetChar.intelligence * intelligence;

            targetChar.physicalDefense -= targetChar.physicalDefense * physicalDefense;
            targetChar.lightningDefense -= targetChar.lightningDefense * lightningDefense;
            targetChar.waterDefense -= targetChar.waterDefense * waterDefense;
            targetChar.fireDefense -= targetChar.fireDefense * fireDefense;
            targetChar.iceDefense -= targetChar.iceDefense * iceDefense;
            targetChar.shadowDefense -= targetChar.shadowDefense * shadowDefense;
            targetChar.holyDefense -= targetChar.holyDefense * holyDefense;

            targetChar.dropCostReduction -= targetChar.dropCostReduction * dropCostReduction;
            targetChar.skillCostReduction -= targetChar.skillCostReduction * skillCostReduction;
        }

        equipping = true;

        if (targetChar.currentHealth > targetChar.maxHealth)
        {
            targetChar.currentHealth = targetChar.maxHealth;
        }

        if (targetChar.currentMana > targetChar.maxMana)
        {
            targetChar.currentMana = targetChar.maxMana;
        }

        if (targetChar.currentEnergy > targetChar.maxEnergy)
        {
            targetChar.currentEnergy = targetChar.maxEnergy;
        }

        //Debug.Log(targetChar.maxHealth)
    }
}
