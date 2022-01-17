using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleSkillsUIHolder : MonoBehaviour
{
    public Skills skill;

    // In battle, sets the text of which Skills are useable, per that character's turn
    // The ability to use the Skill is set with OnClickEvent() with a simple "return"
    public void SetSkillText()
    {
        if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
        {
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().KnowsSkill(skill))
            {
                GetComponentInChildren<TextMeshProUGUI>().text = skill.skillName;
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
        {
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().KnowsSkill(skill))
            {
                GetComponentInChildren<TextMeshProUGUI>().text = skill.skillName;
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().KnowsSkill(skill))
            {
                GetComponentInChildren<TextMeshProUGUI>().text = skill.skillName;
            }
            else
            {
                return;
            }
        }
    }
    public void OnClickEvent()
    {

        if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
        {
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().KnowsSkill(skill))
            {
                if (skill.skillIndex != 21)
                {
                    Engine.e.battleSystem.SkillChoice(skill);
                }
                else
                {
                    if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().haltInflicted)
                    {
                        return;
                    }
                    else
                    {
                        Engine.e.battleSystem.SkillChoice(skill);
                    }
                }
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
        {
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().KnowsSkill(skill))
            {
                if (skill.skillIndex != 21)
                {
                    Engine.e.battleSystem.SkillChoice(skill);
                }
                else
                {
                    if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().haltInflicted)
                    {
                        return;
                    }
                    else
                    {
                        Engine.e.battleSystem.SkillChoice(skill);
                    }
                }
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().KnowsSkill(skill))
            {
                if (skill.skillIndex != 21)
                {
                    Engine.e.battleSystem.SkillChoice(skill);
                }
                else
                {
                    if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().haltInflicted)
                    {
                        return;
                    }
                    else
                    {
                        Engine.e.battleSystem.SkillChoice(skill);
                    }
                }
            }
            else
            {
                return;
            }
        }
    }


    public void DisplayHelpText()
    {
        if (skill != null)
        {
            if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
            {
                if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().KnowsSkill(skill))
                {
                    Engine.e.battleSystem.battleHelpReference.text = skill.skillDescription;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = string.Empty;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().KnowsSkill(skill))
                {
                    Engine.e.battleSystem.battleHelpReference.text = skill.skillDescription;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = string.Empty;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().KnowsSkill(skill))
                {
                    Engine.e.battleSystem.battleHelpReference.text = skill.skillDescription;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = string.Empty;
                }
            }
        }
    }
}

