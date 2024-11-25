using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    public GameObject[] charSelectionButtons;
    public TextMeshProUGUI currentClass, lvl, hp, mp, enr, expTillNextLvl, dropCostReduction, skillCostReduction;
    public TextMeshProUGUI[] dropLevels, dropExperience, attackStats, defenseStats, resistances, additionalStats;

    public void DisplayStats(int charIndex)
    {
        currentClass.text = "(" + Engine.e.party[charIndex].GetComponent<Character>().currentClass + ")";
        lvl.text = "Lvl: " + Engine.e.party[charIndex].GetComponent<Character>().lvl;
        hp.text = "HP: " + Engine.e.party[charIndex].GetComponent<Character>().currentHealth + " / " + Engine.e.party[charIndex].GetComponent<Character>().maxHealth;
        mp.text = "MP: " + Engine.e.party[charIndex].GetComponent<Character>().currentMana + " / " + Engine.e.party[charIndex].GetComponent<Character>().maxMana;
        enr.text = "ENR: " + Engine.e.party[charIndex].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[charIndex].GetComponent<Character>().maxEnergy;

        expTillNextLvl.text = "Exp Until Next Lvl: " + Engine.e.party[charIndex].GetComponent<Character>().experiencePoints + " / " + Engine.e.party[charIndex].GetComponent<Character>().levelUpReq;

        dropCostReduction.text = "Drop Cost Reduction: " + Engine.e.party[charIndex].GetComponent<Character>().dropCostReduction + "%";
        skillCostReduction.text = "Skill Cost Reduction: " + Engine.e.party[charIndex].GetComponent<Character>().skillCostReduction + "%";

        dropLevels[0].text = "Fire: " + Engine.e.party[charIndex].GetComponent<Character>().fireDropsLevel;
        dropLevels[1].text = "Ice: " + Engine.e.party[charIndex].GetComponent<Character>().iceDropsLevel;
        dropLevels[2].text = "Lightning: " + Engine.e.party[charIndex].GetComponent<Character>().lightningDropsLevel;
        dropLevels[3].text = "Water: " + Engine.e.party[charIndex].GetComponent<Character>().waterDropsLevel;
        dropLevels[4].text = "Shadow: " + Engine.e.party[charIndex].GetComponent<Character>().shadowDropsLevel;
        dropLevels[5].text = "Holy: " + Engine.e.party[charIndex].GetComponent<Character>().holyDropsLevel;

        dropExperience[0].text = "Fire: " + Engine.e.party[charIndex].GetComponent<Character>().fireDropsExperience + " / " + Engine.e.party[charIndex].GetComponent<Character>().fireDropsLvlReq;
        dropExperience[1].text = "Ice: " + Engine.e.party[charIndex].GetComponent<Character>().iceDropsExperience + " / " + Engine.e.party[charIndex].GetComponent<Character>().iceDropsLvlReq;
        dropExperience[2].text = "Lightning: " + Engine.e.party[charIndex].GetComponent<Character>().lightningDropsExperience + " / " + Engine.e.party[charIndex].GetComponent<Character>().lightningDropsLvlReq;
        dropExperience[3].text = "Water: " + Engine.e.party[charIndex].GetComponent<Character>().waterDropsExperience + " / " + Engine.e.party[charIndex].GetComponent<Character>().waterDropsLvlReq;
        dropExperience[4].text = "Shadow: " + Engine.e.party[charIndex].GetComponent<Character>().shadowDropsExperience + " / " + Engine.e.party[charIndex].GetComponent<Character>().shadowDropsLvlReq;
        dropExperience[5].text = "Holy: " + Engine.e.party[charIndex].GetComponent<Character>().holyDropsExperience + " / " + Engine.e.party[charIndex].GetComponent<Character>().holyDropsLvlReq;

        // Offensive Stats
        attackStats[0].text = Engine.e.party[charIndex].GetComponent<Character>().strength.ToString();
        attackStats[1].text = Engine.e.party[charIndex].GetComponent<Character>().fireDropAttackBonus.ToString();
        attackStats[2].text = Engine.e.party[charIndex].GetComponent<Character>().iceDropAttackBonus.ToString();
        attackStats[3].text = Engine.e.party[charIndex].GetComponent<Character>().lightningDropAttackBonus.ToString();
        attackStats[4].text = Engine.e.party[charIndex].GetComponent<Character>().waterDropAttackBonus.ToString();
        attackStats[5].text = Engine.e.party[charIndex].GetComponent<Character>().shadowDropAttackBonus.ToString();
        attackStats[6].text = Engine.e.party[charIndex].GetComponent<Character>().holyDropAttackBonus.ToString();

        // Defensive Stats
        defenseStats[0].text = Engine.e.party[charIndex].GetComponent<Character>().physicalDefense.ToString() + "%";
        defenseStats[1].text = Engine.e.party[charIndex].GetComponent<Character>().fireDefense.ToString() + "%";
        defenseStats[2].text = Engine.e.party[charIndex].GetComponent<Character>().iceDefense.ToString() + "%";
        defenseStats[3].text = Engine.e.party[charIndex].GetComponent<Character>().lightningDefense.ToString() + "%";
        defenseStats[4].text = Engine.e.party[charIndex].GetComponent<Character>().waterDefense.ToString() + "%";
        defenseStats[5].text = Engine.e.party[charIndex].GetComponent<Character>().shadowDefense.ToString() + "%";
        defenseStats[6].text = Engine.e.party[charIndex].GetComponent<Character>().holyDefense.ToString() + "%";

        // Resistances
        resistances[0].text = "Sleep: " + Engine.e.party[charIndex].GetComponent<Character>().sleepDefense.ToString() + "%";
        resistances[1].text = "Poison: " + Engine.e.party[charIndex].GetComponent<Character>().poisonDefense.ToString() + "%";
        resistances[2].text = "Confuse: " + Engine.e.party[charIndex].GetComponent<Character>().confuseDefense.ToString() + "%";
        resistances[3].text = "Death: " + Engine.e.party[charIndex].GetComponent<Character>().deathDefense.ToString() + "%";

        // Additional Stats
        additionalStats[0].text = "Intelligence: " + Engine.e.party[charIndex].GetComponent<Character>().intelligence.ToString();
        additionalStats[1].text = "Haste: " + Engine.e.party[charIndex].GetComponent<Character>().haste.ToString() + "%";
        additionalStats[2].text = "Dodge: " + Engine.e.party[charIndex].GetComponent<Character>().dodgeChance.ToString() + "%";
        additionalStats[3].text = "Crit: " + Engine.e.party[charIndex].GetComponent<Character>().critChance.ToString() + "%";

    }
}
