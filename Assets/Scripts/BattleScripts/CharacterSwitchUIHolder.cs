using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchUIHolder : MonoBehaviour
{
    public Character character;

    public void OnClickEvent()
    {
        if (Engine.e.inBattle)
        {
            if (!Engine.e.battleSystem.charSkillSwitchCheck)
            {
                if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
                {
                    Engine.e.battleSystem.char1Switching = true;
                    Engine.e.battleSystem.char1SwitchToIndex = character.partyIndex;
                    Engine.e.battleSystem.DeactivateChar1MenuButtons();

                    Engine.e.battleSystem.battleQueue.Enqueue(BattleState.CHAR1TURN);

                    if (Engine.e.battleSystem.char2Ready)
                    {
                        Engine.e.battleSystem.ActivateChar2MenuButtons();
                    }
                    if (Engine.e.battleSystem.char3Ready)
                    {
                        Engine.e.battleSystem.ActivateChar3MenuButtons();
                    }

                }
                if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
                {
                    Engine.e.battleSystem.char2Switching = true;
                    Engine.e.battleSystem.char2SwitchToIndex = character.partyIndex;
                    Engine.e.battleSystem.DeactivateChar2MenuButtons();

                    Engine.e.battleSystem.battleQueue.Enqueue(BattleState.CHAR2TURN);

                    if (Engine.e.battleSystem.char1Ready)
                    {
                        Engine.e.battleSystem.ActivateChar1MenuButtons();
                    }
                    if (Engine.e.battleSystem.char3Ready)
                    {
                        Engine.e.battleSystem.ActivateChar3MenuButtons();
                    }

                }
                if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
                {
                    Engine.e.battleSystem.char3Switching = true;
                    Engine.e.battleSystem.char3SwitchToIndex = character.partyIndex;
                    Engine.e.battleSystem.DeactivateChar3MenuButtons();

                    Engine.e.battleSystem.battleQueue.Enqueue(BattleState.CHAR3TURN);

                    if (Engine.e.battleSystem.char1Ready)
                    {
                        Engine.e.battleSystem.ActivateChar1MenuButtons();
                    }
                    if (Engine.e.battleSystem.char2Ready)
                    {
                        Engine.e.battleSystem.ActivateChar2MenuButtons();
                    }
                }
            }
            else
            {
                Engine.e.battleSystem.charSkillSwitchCheck = false;

                if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
                {
                    Engine.e.battleSystem.char1SkillSwitch = true;
                    Engine.e.battleSystem.char1Target = character.partyIndex;
                    Engine.e.battleSystem.DeactivateChar1MenuButtons();

                    Engine.e.battleSystem.battleQueue.Enqueue(BattleState.CHAR1TURN);

                    if (Engine.e.battleSystem.char2Ready)
                    {
                        Engine.e.battleSystem.ActivateChar2MenuButtons();
                    }
                    if (Engine.e.battleSystem.char3Ready)
                    {
                        Engine.e.battleSystem.ActivateChar3MenuButtons();
                    }

                }
                if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
                {
                    Engine.e.battleSystem.char2SkillSwitch = true;

                    Engine.e.battleSystem.char2Target = character.partyIndex;
                    Engine.e.battleSystem.DeactivateChar2MenuButtons();

                    Engine.e.battleSystem.battleQueue.Enqueue(BattleState.CHAR2TURN);

                    if (Engine.e.battleSystem.char1Ready)
                    {
                        Engine.e.battleSystem.ActivateChar1MenuButtons();
                    }
                    if (Engine.e.battleSystem.char3Ready)
                    {
                        Engine.e.battleSystem.ActivateChar3MenuButtons();
                    }

                }
                if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
                {
                    Engine.e.battleSystem.char3SkillSwitch = true;

                    Engine.e.battleSystem.char3Target = character.partyIndex;
                    Engine.e.battleSystem.DeactivateChar3MenuButtons();

                    Engine.e.battleSystem.battleQueue.Enqueue(BattleState.CHAR3TURN);

                    if (Engine.e.battleSystem.char1Ready)
                    {
                        Engine.e.battleSystem.ActivateChar1MenuButtons();
                    }
                    if (Engine.e.battleSystem.char2Ready)
                    {
                        Engine.e.battleSystem.ActivateChar2MenuButtons();
                    }
                }
            }

            Engine.e.battleSystem.availableCharSwitchButtons.SetActive(false);
            Engine.e.battleSystem.state = BattleState.ATBCHECK;

        }
    }
}
