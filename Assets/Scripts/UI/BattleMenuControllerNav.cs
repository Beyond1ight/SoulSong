using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleMenuControllerNav : MonoBehaviour
{

    public GameObject char1FirstTurn, char2FirstTurn, char3FirstTurn;
    public GameObject battlePhysicalAttackFirstEnemy, battlePhysicalAttackSecondEnemy, battlePhysicalAttackThirdEnemy, battlePhysicalAttackFourthEnemy;
    public GameObject battleSkillAttackFirstEnemy, battleSkillAttackSecondEnemy, battleSkillAttackThirdEnemy, battleSkillAttackFourthEnemy;
    public GameObject battleAlly1Target, battleSkillAlly1Target;
    public GameObject char1AvailSwitchGrieve, char1AvailSwitchMac, char1AvailSwitchField, char1AvailSwitchRiggs;
    public GameObject char2AvailSwitchGrieve, char2AvailSwitchMac, char2AvailSwitchField, char2AvailSwitchRiggs;
    public GameObject char3AvailSwitchGrieve, char3AvailSwitchMac, char3AvailSwitchField, char3AvailSwitchRiggs;
    public GameObject battleItemsFirstCheck;



    // Skills First Choice
    public GameObject battleSkillFirstChoice, battleSkillsBackButtonChar1, battleSkillsBackButtonChar2, battleSkillsBackButtonChar3;

    // Drop First Choice
    public GameObject dropFirstButton;


    public void OpenFirstTurn()
    {
        Engine.e.battleSystem.char1BattlePanel.SetActive(true);
        if (Engine.e.activeParty.activeParty[1] != null)
        {
            Engine.e.battleSystem.char2BattlePanel.SetActive(true);
        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            Engine.e.battleSystem.char3BattlePanel.SetActive(true);
        }
        Engine.e.battleSystem.enemyPanel.SetActive(true);

        Engine.e.battleHelp.text = string.Empty;
        if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char1FirstTurn);
        }
        if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char2FirstTurn);
        }
        if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char3FirstTurn);
        }
    }

    public void OpenSkillChoice()
    {
        Engine.e.battleSystem.char1BattlePanel.SetActive(false);
        Engine.e.battleSystem.char2BattlePanel.SetActive(false);
        Engine.e.battleSystem.char3BattlePanel.SetActive(false);


        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(battleSkillFirstChoice);

    }

    public void OpenSkillTarget()
    {
        if (Engine.e.battleSystem.lastSkillChoice.dps)
        {
            if (Engine.e.battleSystem.enemies[0].GetComponent<Enemy>().health > 0)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(battleSkillAttackFirstEnemy);
            }
            else
            {
                if (Engine.e.battleSystem.enemies[1] != null)
                {
                    if (Engine.e.battleSystem.enemies[1].GetComponent<Enemy>().health > 0)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(battleSkillAttackFirstEnemy);
                    }
                }
                else
                {
                    if (Engine.e.battleSystem.enemies[2] != null)
                    {
                        if (Engine.e.battleSystem.enemies[2].GetComponent<Enemy>().health > 0)
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(battleSkillAttackFirstEnemy);
                        }
                    }
                    else
                    {
                        if (Engine.e.battleSystem.enemies[3] != null)
                        {
                            if (Engine.e.battleSystem.enemies[3].GetComponent<Enemy>().health > 0)
                            {
                                EventSystem.current.SetSelectedGameObject(null);
                                EventSystem.current.SetSelectedGameObject(battleSkillAttackFirstEnemy);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(battleSkillAlly1Target);
        }
    }

    public void OpenDropSupportTarget()
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(battleAlly1Target);
    }

    public void OpenBattleItems()
    {

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.battleSystem.battleItems[0].gameObject);


    }

    public void OpenCharSwitchMenu()
    {
        if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Grieve")
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(char1AvailSwitchGrieve);
                }
                else
                {
                    if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Mac")
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(char1AvailSwitchMac);

                    }
                    else
                    {
                        if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Field")
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(char1AvailSwitchField);
                        }

                        else
                        {
                            if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Riggs")
                            {
                                EventSystem.current.SetSelectedGameObject(null);
                                EventSystem.current.SetSelectedGameObject(char1AvailSwitchRiggs);
                            }
                        }
                    }
                }
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Grieve")
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(char2AvailSwitchGrieve);
                }
                else
                {
                    if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Mac")
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(char2AvailSwitchMac);
                    }
                    else
                    {
                        if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Field")
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(char2AvailSwitchField);
                        }
                        else
                        {
                            if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Riggs")
                            {
                                EventSystem.current.SetSelectedGameObject(null);
                                EventSystem.current.SetSelectedGameObject(char2AvailSwitchRiggs);
                            }
                        }
                    }
                }
            }
        }

        if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Grieve")
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(char3AvailSwitchGrieve);
                }
                else
                {
                    if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Mac")
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(char3AvailSwitchMac);

                    }
                    else
                    {
                        if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Field")
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(char3AvailSwitchField);
                        }

                        else
                        {
                            if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName != "Riggs")
                            {
                                EventSystem.current.SetSelectedGameObject(null);
                                EventSystem.current.SetSelectedGameObject(char3AvailSwitchRiggs);
                            }
                        }
                    }
                }
            }
        }
    }

    public void OpenDropChoice()
    {
        Engine.e.battleSystem.enemyPanel.SetActive(false);
        Engine.e.battleSystem.char1BattlePanel.SetActive(false);
        if (Engine.e.activeParty.activeParty[1] != null)
        {
            Engine.e.battleSystem.char2BattlePanel.SetActive(false);
        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            Engine.e.battleSystem.char3BattlePanel.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(dropFirstButton);
    }

    public void OpenAttackFirstEnemy()
    {
        if (Engine.e.battleSystem.enemies[0].GetComponent<Enemy>().health > 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(battlePhysicalAttackFirstEnemy);
        }
        else
        {
            if (Engine.e.battleSystem.enemies[1] != null)
            {
                if (Engine.e.battleSystem.enemies[1].GetComponent<Enemy>().health > 0)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(battlePhysicalAttackSecondEnemy);
                }

                else
                {
                    if (Engine.e.battleSystem.enemies[2] != null)
                    {
                        if (Engine.e.battleSystem.enemies[2].GetComponent<Enemy>().health > 0)
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(battlePhysicalAttackThirdEnemy);
                        }

                        else
                        {
                            if (Engine.e.battleSystem.enemies[3] != null)
                            {
                                if (Engine.e.battleSystem.enemies[3].GetComponent<Enemy>().health > 0)
                                {
                                    EventSystem.current.SetSelectedGameObject(null);
                                    EventSystem.current.SetSelectedGameObject(battlePhysicalAttackFourthEnemy);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
