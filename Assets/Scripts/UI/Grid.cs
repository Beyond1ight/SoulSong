using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.U2D;
public class Grid : MonoBehaviour
{
    public GameObject[] charSelectionButtons;
    public GameObject[] connectionLines;
    public bool gridDisplayed, grieveScreen, macScreen, fieldScreen, riggsScreen;
    public GameObject[] skillsButtons, fireDropsButtons, iceDropsButtons, lightningDropsButtons, waterDropsButtons, shadowDropsButtons, holyDropsButtons;
    public bool knowsAllFireDrops = false, knowsAllIceDrops = false, knowsAllLightningDrops = false, knowsAllWaterDrops = false, knowsAllShadowDrops = false, knowsAllHolyDrops = false;
    public GameObject confirmSkillAcquisition;
    public TextMeshProUGUI confirmSkillSentence;
    public TextMeshProUGUI[] apDisplay;
    public AbilityStatNode[] nodes;
    public int tier, grievePosition, macPosition, fieldPosition, riggsPosition;
    public GameObject cursor, helpReference, helpTextParentObj;
    public CinemachineVirtualCamera gridPerspective, centerOfGridPerspective;

    private void Start()
    {
        helpReference.transform.SetParent(helpTextParentObj.transform);
    }
    /*public void UnlockSkillChoiceCheck(int _tier)
    {
        string apCost = string.Empty;

        if (Engine.e.gameSkills[_tier] != null)
        {
            apCost = "Costs " + Engine.e.gameSkills[_tier].abilityPointCost + "AP";
        }
        else
        {
            return;
        }

        if (grieveScreen)
        {
            Character grieve = Engine.e.party[0].GetComponent<Grieve>();
            if (grieve.lvl > 20)
            {
                if (grieve.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (grieve.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = string.Empty;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Grieve? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
                    }
                    else
                    {
                        return;

                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        if (macScreen)
        {
            Character mac = Engine.e.party[1].GetComponent<Mac>();

            if (mac.lvl > 20)
            {
                if (mac.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (mac.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Mac? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
                    }
                    else
                    {
                        return;

                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        if (fieldScreen)
        {
            Character field = Engine.e.party[2].GetComponent<Field>();

            if (field.lvl > 20)
            {
                if (field.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (field.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Field? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
                    }
                    else
                    {
                        return;

                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        if (riggsScreen)
        {
            Character riggs = Engine.e.party[3].GetComponent<Riggs>();

            if (riggs.lvl > 20)
            {
                if (riggs.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (riggs.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Riggs? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
                    }
                    else
                    {
                        return;

                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    public void UnlockSkillChoice()
    {
        if (Engine.e.gameSkills[tier] != null)
        {
            if (grieveScreen)
            {
                Character grieve = Engine.e.party[0].GetComponent<Grieve>();

                grieve.skills[tier] = Engine.e.gameSkills[tier];
                grieve.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                grieve.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;

            }

            if (macScreen)
            {
                Character mac = Engine.e.party[1].GetComponent<Mac>();

                mac.skills[tier] = Engine.e.gameSkills[tier];
                mac.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                mac.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;
            }

            if (fieldScreen)
            {
                Character field = Engine.e.party[2].GetComponent<Field>();

                field.skills[tier] = Engine.e.gameSkills[tier];
                field.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                field.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;

            }

            if (riggsScreen)
            {
                Character riggs = Engine.e.party[3].GetComponent<Riggs>();

                riggs.skills[tier] = Engine.e.gameSkills[tier];
                riggs.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                riggs.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;

            }

            confirmSkillSentence.text = string.Empty;
            confirmSkillAcquisition.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(skillsButtons[tier]);

            DisplayAP();
        }
        else
        {
            return;
        }
    }

    public void DenySkillUnlock()
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(skillsButtons[tier]);
        confirmSkillAcquisition.SetActive(false);
        confirmSkillSentence.text = string.Empty;
        tier = -1;
    }*/

    public void SetSkills()
    {
        Character grieve = Engine.e.party[0].GetComponent<Grieve>();
        Character mac = null;
        Character field = null;
        Character riggs = null;

        if (Engine.e.party[1] != null)
        {
            mac = Engine.e.party[1].GetComponent<Mac>();
        }
        if (Engine.e.party[2] != null)
        {
            field = Engine.e.party[2].GetComponent<Field>();
        }
        if (Engine.e.party[3] != null)
        {
            riggs = Engine.e.party[3].GetComponent<Riggs>();
        }

        if (grieve.skillTotal <= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if (grieve.skills[i] != null)
                {
                    if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (grieve.skills[i] != null)
                        {
                            skillsButtons[i].GetComponentInChildren<TMP_Text>().text = grieve.skills[i].skillName;
                        }
                    }
                }
            }
        }

        /*   if (mac != null)
           {
               if (mac.skillTotal <= 5)
               {
                   for (int i = 5; i < 10; i++)
                   {
                       if (mac.skills[i] != null)
                       {
                           if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                           {
                               skillsButtons[i].GetComponentInChildren<TMP_Text>().text = mac.skills[i].skillName;
                           }
                       }
                   }
               }
           }

           if (field != null)
           {
               if (field.skillTotal <= 5)
               {
                   for (int i = 10; i < 15; i++)
                   {
                       if (field.skills[i] != null)
                       {
                           if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                           {
                               skillsButtons[i].GetComponentInChildren<TMP_Text>().text = field.skills[i].skillName;
                           }
                       }
                   }
               }
           }

           if (riggs != null)
           {
               if (riggs.skillTotal <= 5)
               {
                   for (int i = 15; i < 20; i++)
                   {
                       if (riggs.skills[i] != null)
                       {
                           if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                           {
                               skillsButtons[i].GetComponentInChildren<TMP_Text>().text = riggs.skills[i].skillName;
                           }
                       }
                   }
               }
           }*/

        tier = -1;
    }

    /*public void SetDrops()
    {

        for (int i = 0; i < 10; i++)
        {
            if (!knowsAllFireDrops)
            {
                if (Engine.e.fireDrops[i] != null)
                {
                    if (fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.fireDrops[i].isKnown)
                        {
                            fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.fireDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllIceDrops)
            {
                if (Engine.e.iceDrops[i] != null)
                {
                    if (iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.iceDrops[i].isKnown)
                        {
                            iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.iceDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllLightningDrops)
            {
                if (Engine.e.lightningDrops[i] != null)
                {
                    if (lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.lightningDrops[i].isKnown)
                        {
                            lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.lightningDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllWaterDrops)
            {
                if (Engine.e.waterDrops[i] != null)
                {
                    if (waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.waterDrops[i].isKnown)
                        {
                            waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.waterDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllShadowDrops)
            {
                if (Engine.e.shadowDrops[i] != null)
                {
                    if (shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.shadowDrops[i].isKnown)
                        {
                            shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.shadowDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllHolyDrops)
            {
                if (Engine.e.holyDrops[i] != null)
                {
                    if (holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.holyDrops[i].isKnown)
                        {
                            holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.holyDrops[i].dropName;
                        }
                    }
                }
            }
        }
    }*/

    public void SetGrieveScreen()
    {
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        grieveScreen = true;

        if (Engine.e.party[1] != null)
        {
            charSelectionButtons[1].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charSelectionButtons[2].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[3] != null)
        {
            charSelectionButtons[3].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].grieveUnlocked)
            {
                for (int k = 0; k < nodes[i].connectionLines.Length; k++)
                {
                    if (nodes[i].connectionLines[k] != null)
                    {
                        nodes[i].connectionLines[k].GetComponent<SpriteShapeRenderer>().color = Color.cyan;
                    }
                }
            }
        }

        //  EventSystem.current.SetSelectedGameObject(null);

        //  EventSystem.current.SetSelectedGameObject(nodes[grievePosition].gameObject);
        cursor.GetComponent<GridCursor>().currentNode = nodes[grievePosition];
        Vector3 cursorPos = new Vector3(nodes[grievePosition].transform.position.x, nodes[grievePosition].transform.position.y, -5);

        cursor.transform.position = cursorPos;
        // cursor.GetComponent<GridCursor>().SetDirectionChoices();
        /*for (int i = 0; i < nodes.Length; i++)
        {
                   if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (!grieveUnlocked)
                {
                                        skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                           skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                }
            }
        }*/
    }

    public void SetMacScreen()
    {
        grieveScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        macScreen = true;


        if (Engine.e.party[0] != null)
        {
            charSelectionButtons[0].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charSelectionButtons[2].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[3] != null)
        {
            charSelectionButtons[3].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].macUnlocked)
            {
                for (int k = 0; k < nodes[i].connectionLines.Length; k++)
                {
                    if (nodes[i].connectionLines[k] != null)
                    {
                        nodes[i].connectionLines[k].GetComponent<SpriteShapeRenderer>().color = Color.blue;
                    }
                }
            }
        }

        cursor.GetComponent<GridCursor>().currentNode = nodes[macPosition];
        Vector3 cursorPos = new Vector3(nodes[macPosition].transform.position.x, nodes[macPosition].transform.position.y, -5);

        cursor.transform.position = cursorPos;
    }

    public void SetFieldScreen()
    {
        grieveScreen = false;
        macScreen = false;
        riggsScreen = false;
        fieldScreen = true;

        if (Engine.e.party[0] != null)
        {
            charSelectionButtons[0].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charSelectionButtons[1].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[3] != null)
        {
            charSelectionButtons[3].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].fieldUnlocked)
            {
                for (int k = 0; i < nodes[k].connectionLines.Length; k++)
                {
                    if (nodes[i].connectionLines[k] != null)
                    {
                        nodes[i].connectionLines[k].GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }


    }

    public void SetRiggsScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = true;

        if (Engine.e.party[0] != null)
        {
            charSelectionButtons[0].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charSelectionButtons[1].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charSelectionButtons[2].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].riggsUnlocked)
            {
                for (int k = 0; i < nodes[k].connectionLines.Length; k++)
                {
                    if (nodes[i].connectionLines[k] != null)
                    {
                        nodes[i].connectionLines[k].GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }

        /*for (int i = 0; i < nodes.Length; i++)
        {
            if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (!nodes[i].riggsUnlocked)
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                }
            }
        }*/
    }

    public void ClearGrid()
    {
        for (int i = 0; i < nodes.Length; i++)
        {

            nodes[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
        for (int i = 0; i < connectionLines.Length; i++)
        {
            if (connectionLines[i] != null)
            {
                connectionLines[i].GetComponent<SpriteShapeRenderer>().color = Color.gray;
            }
        }
    }
    public void CloseGrid()
    {
        Engine.e.canvasReference.SetActive(true);
        Engine.e.canvasReference.GetComponent<PauseMenu>().OpenPauseMenu();
        Engine.e.gridReference.gameObject.SetActive(false);
        gridDisplayed = false;

        Time.timeScale = 0f;

        if (grieveScreen == true || macScreen == true || fieldScreen == true || riggsScreen == true)
        {
            grieveScreen = false;
            macScreen = false;
            fieldScreen = false;
            riggsScreen = false;

            for (int i = 0; i < connectionLines.Length; i++)
            {
                if (connectionLines[i] != null)
                {
                    connectionLines[i].GetComponent<SpriteShapeRenderer>().color = Color.gray;
                }
            }
        }
    }

    public void DisplayCharSelection()
    {
        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                charSelectionButtons[i].SetActive(true);
            }
        }
    }


    public void DisplayAP()
    {
        for (int i = 0; i < apDisplay.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                if (Engine.e.party[i].GetComponent<Character>().lvl >= 20)
                {
                    apDisplay[i].text = Engine.e.party[i].GetComponent<Character>().availableSkillPoints.ToString();
                }
            }
        }
    }

    // For New Game
    public void SetupGrid()
    {
        gridDisplayed = false;
        for (int i = 0; i < connectionLines.Length; i++)
        {
            if (connectionLines[i] != null)
            {
                if (connectionLines[i].GetComponent<SpriteShapeRenderer>().color != Color.gray)
                {
                    connectionLines[i].GetComponent<SpriteShapeRenderer>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < nodes.Length; i++)
        {

            nodes[i].GetComponent<SpriteRenderer>().color = Color.red;
            nodes[i].grieveUnlocked = false;
            nodes[i].macUnlocked = false;
            nodes[i].fieldUnlocked = false;
            nodes[i].riggsUnlocked = false;

            if (nodes[i].nodeIndex == 0 || nodes[i].nodeIndex == -1)
                nodes[i].nodeIndex = i;
        }

        nodes[0].grieveUnlocked = true;
        nodes[1].macUnlocked = true;
        nodes[2].fieldUnlocked = true;
        nodes[3].riggsUnlocked = true;

        grievePosition = 0;
        macPosition = 1;
        fieldPosition = 2;
        riggsPosition = 3;
    }
}

