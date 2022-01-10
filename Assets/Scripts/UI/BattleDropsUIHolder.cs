using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleDropsUIHolder : MonoBehaviour
{
    public Drops drop;

    // In battle, sets the text of which Drops are useable, per that character's turn
    // The ability to use the Drop is set with OnClickEvent() with a simple "return"
    public void SetDropText()
    {
        if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
        {
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().KnowsDrop(drop))
            {
                GetComponentInChildren<TextMeshProUGUI>().text = drop.dropName;
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
        {
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().KnowsDrop(drop))
            {
                GetComponentInChildren<TextMeshProUGUI>().text = drop.dropName;
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().KnowsDrop(drop))
            {
                GetComponentInChildren<TextMeshProUGUI>().text = drop.dropName;
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
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().KnowsDrop(drop))
            {
                Engine.e.battleSystem.DropChoice(drop);
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
        {
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().KnowsDrop(drop))
            {
                Engine.e.battleSystem.DropChoice(drop);
            }
            else
            {
                return;
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().KnowsDrop(drop))
            {
                Engine.e.battleSystem.DropChoice(drop);
            }
            else
            {
                return;
            }
        }
    }

    public void DisplayHelpText()
    {
        if (drop != null)
        {
            if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
            {
                if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().KnowsDrop(drop))
                {
                    Engine.e.battleSystem.battleHelpReference.text = drop.dropDescription;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = string.Empty;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().KnowsDrop(drop))
                {
                    Engine.e.battleSystem.battleHelpReference.text = drop.dropDescription;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = string.Empty;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().KnowsDrop(drop))
                {
                    Engine.e.battleSystem.battleHelpReference.text = drop.dropDescription;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = string.Empty;
                }
            }
        }
    }
}