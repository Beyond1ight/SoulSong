using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillHelpText : MonoBehaviour
{
    public TextMeshProUGUI helpReference;
    public TextMeshProUGUI abilityMenuHelpReference;
    int characterOutOfBattleIndex;

    public void DisplayHelp(int skillIndex)
    {
        Skills skill = Engine.e.gameSkills[skillIndex];
        int index = 0;
        float skillModifier;
        string energy;
        string menuEnergy;


        // In Battle        
        if (Engine.e.inBattle)
        {
            if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
            {
                index = 0;
            }
            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                index = 1;
            }
            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                index = 2;
            }
            Character character = Engine.e.activeParty.activeParty[index].GetComponent<Character>();

            float skillCost = 999;

            skillModifier = ((Engine.e.activeParty.activeParty[index].GetComponent<Character>().skillScale) / 2);

            if (skill != null)
            {
                skillCost = Mathf.Round(skill.skillCost - (skill.skillCost * character.skillCostReduction / 100) + 0.45f);

                energy = "Costs " + skillCost + " ENR. (Current ENR: " + character.currentEnergy.ToString() + ").";
            }
            else
            {
                energy = string.Empty;
            }

            if (character.skills[skillIndex] != null)
            {
                switch (skillIndex)
                {
                    // Grieve Skills
                    case 0:
                        float skill1Damage = Mathf.Round(character.physicalDamage) * 2;
                        helpReference.text = "Hits an enemy for 2x damage while ignoring defense. (" + skill1Damage + " physical damage). " + energy;
                        break;

                    // Mac Skills
                    case 5:
                        float skill5Damage = Mathf.Round(skill.skillPointReturn + (skill.skillPointReturn * skillModifier));
                        helpReference.text = "Absorbes " + skill5Damage + "MP from an enemy while also dealing " + skill5Damage + " damage. " + energy;
                        break;

                    // Field Skills    
                    case 10:
                        helpReference.text = skill.skillDescription;
                        break;
                    case 11:
                        float skill11Boost = 10 + Mathf.Round(character.skillScale * 10 / 25);
                        helpReference.text = "Reduces target's hit chance by " + skill11Boost + "%";
                        break;
                    case 12:
                        helpReference.text = skill.skillDescription + energy;
                        break;
                    case 13:
                        float skill13Damage = Mathf.Round(skill.skillPower + (skill.skillPower * character.shadowDropsLevel / 20) * skillModifier);
                        helpReference.text = "Deals " + skill13Damage + " Shadow damage while also inflicting Poison. " + energy;
                        break;
                    case 14:
                        helpReference.text = skill.skillDescription + energy;
                        break;

                    // Riggs Skills    
                    case 15:
                        float skill15Boost = Mathf.Round(character.physicalDamage + (character.skillScale * 10 / 25));
                        helpReference.text = "Infuses weapon, increasing Physical damage by " + skill15Boost + "%";
                        break;
                    case 16:
                        float skill15Damage = Mathf.Round(skill.skillPower + (skill.skillPower * character.holyDropsLevel / 20) * skillModifier);
                        helpReference.text = "Blasts an enemy for " + skill15Damage + " Holy damage. " + energy;
                        break;
                }
            }
        }
        else
        {
            if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().grieveScreen)
            {
                index = 0;
            }
            if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().macScreen)
            {
                index = 1;
            }
            if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().fieldScreen)
            {
                index = 2;
            }
            if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().riggsScreen)
            {
                index = 3;
            }

            Character character = Engine.e.party[index].GetComponent<Character>();
            float skillCost = 999;
            skillModifier = ((Engine.e.party[index].GetComponent<Character>().skillScale) / 2);

            if (skill != null)
            {
                skillCost = Mathf.Round(skill.skillCost - (skill.skillCost * character.skillCostReduction / 100) + 0.45f);

                if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().skillsButtons[skillIndex].GetComponentInChildren<TMP_Text>().text == "-")
                {
                    abilityMenuHelpReference.text = string.Empty;
                }
                else
                {
                    menuEnergy = "Costs " + skillCost + " ENR.";

                    switch (skillIndex)
                    {
                        // Grieve Skills
                        case 0:
                            abilityMenuHelpReference.text = skill.skillDescription + menuEnergy;
                            break;

                        // Mac Skills
                        case 5:
                            float absorption = (skill.skillPointReturn + (skill.skillPointReturn * skillModifier));
                            abilityMenuHelpReference.text = "Absorbes " + absorption + "MP from an enemy while also dealing damage. " + menuEnergy;
                            break;

                        // Field Skills
                        case 10:
                            abilityMenuHelpReference.text = skill.skillDescription;
                            break;
                        case 11:
                            float hitChanceReduction = 10 + Mathf.Round(character.skillScale * 10 / 25);
                            abilityMenuHelpReference.text = "Reduces target's hit chance by " + hitChanceReduction + "%";
                            break;
                        case 12:
                            abilityMenuHelpReference.text = skill.skillDescription;
                            break;
                        case 13:
                            float skill13Damage = Mathf.Round(skill.skillPower + (skill.skillPower * character.shadowDropsLevel / 20) * skillModifier);
                            abilityMenuHelpReference.text = "Deals " + skill13Damage + " Shadow damage while also inflicting Poison. " + menuEnergy;
                            break;
                        case 14:
                            abilityMenuHelpReference.text = skill.skillDescription;
                            break;
                        // Riggs Skills
                        case 15:
                            float skill15Boost = Mathf.Round(character.physicalDamage + (character.skillScale * 10 / 25));
                            abilityMenuHelpReference.text = "Infuses weapon, increasing Physical damage by " + skill15Boost + "%";
                            break;
                        case 16:
                            float holyShockDamage = Mathf.Round(skill.skillPower + (skill.skillPower * character.holyDropsLevel / 20) * skillModifier);
                            abilityMenuHelpReference.text = "Blasts an enemy for " + holyShockDamage + " Holy damage. " + menuEnergy;
                            break;
                    }
                }
            }
        }
    }
}
