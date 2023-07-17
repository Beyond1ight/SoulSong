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
    public AbilityStatNode[] nodes;
    public GameObject[] connectionLines;
    public GameObject[] classProgressionBars;
    public bool gridDisplayed, abilitiesListDisplayed, grieveScreen, macScreen, fieldScreen, riggsScreen, solaceScreen, blueScreen;
    public int grievePosition, macPosition, fieldPosition, riggsPosition, solacePosition, bluePosition;
    public GameObject cursor, helpReference, helpTextParentObj, helpTextCharName, abilitiesList, tier2Path;
    public CinemachineVirtualCamera gridPerspective, centerOfGridPerspective;
    public GameObject[] dropsButtons, skillButtons, classPaths, classConnectionToMiddle, baseClassConnectionToComboClass, charStartingNodes;


    private void Start()
    {
        helpReference.transform.SetParent(helpTextParentObj.transform);
    }

    public void SetGrieveScreen()
    {
        grieveScreen = true;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[0].characterName;
        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        for (int i = 0; i < Engine.e.charClasses.Length; i++)
        {
            if (Engine.e.playableCharacters[0].classUnlocked[i] == true)
            {
                classConnectionToMiddle[i].SetActive(true);
            }
            else
            {
                classConnectionToMiddle[i].SetActive(false);
            }

            if (Engine.e.playableCharacters[0].characterClass[i] == true && Engine.e.playableCharacters[0].classEXP[i] == 100f)
            {
                Engine.e.playableCharacters[0].canSelectNewClass = true;
            }
            else
            {
                Engine.e.playableCharacters[0].canSelectNewClass = false;
            }
        }

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = Engine.e.playableCharacters[0].classEXP[i];
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


        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.party[0].GetComponent<Character>().KnowsDrop(dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop))
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.party[0].GetComponent<Character>().KnowsSkill(skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill))
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        //cursor.GetComponent<GridCursorMovement>().isMoving = false;
        //cursor.GetComponent<GridCursor>().currentAbilityStatNode = nodes[grievePosition];

        if (cursor.GetComponent<GridCursorMovement>().grievePos == Vector3.zero)
        {
            Vector3 cursorPos = new Vector3(charStartingNodes[0].transform.position.x, charStartingNodes[0].transform.position.y, -5);
            cursor.transform.position = cursorPos;
        }
        else
        {
            cursor.transform.position = cursor.GetComponent<GridCursorMovement>().grievePos;
        }

        cursor.GetComponent<GridCursorMovement>().switchingChar = false;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
        //Engine.e.gridReference.gridPerspective.transform.position = cursorPos;


        //  EventSystem.current.SetSelectedGameObject(null);

        //  EventSystem.current.SetSelectedGameObject(nodes[grievePosition].gameObject);
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
        macScreen = true;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[1].characterName;
        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        for (int i = 0; i < Engine.e.charClasses.Length; i++)
        {
            if (Engine.e.playableCharacters[1].classUnlocked[i] == true)
            {
                classConnectionToMiddle[i].SetActive(true);
            }
            else
            {
                classConnectionToMiddle[i].SetActive(false);
            }

            if (Engine.e.playableCharacters[1].characterClass[i] == true && Engine.e.playableCharacters[1].classEXP[i] == 100f)
            {
                Engine.e.playableCharacters[1].canSelectNewClass = true;
            }
            else
            {
                Engine.e.playableCharacters[1].canSelectNewClass = false;
            }
        }

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = Engine.e.playableCharacters[1].classEXP[i];
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null)
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
        }

        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.party[1].GetComponent<Character>().KnowsDrop(dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop))
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.party[1].GetComponent<Character>().KnowsSkill(skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill))
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        if (cursor.GetComponent<GridCursorMovement>().macPos == Vector3.zero)
        {
            Vector3 cursorPos = new Vector3(charStartingNodes[1].transform.position.x, charStartingNodes[1].transform.position.y, -5);
            cursor.transform.position = cursorPos;
        }
        else
        {
            cursor.transform.position = cursor.GetComponent<GridCursorMovement>().macPos;
        }

        cursor.GetComponent<GridCursorMovement>().switchingChar = false;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
        //Engine.e.gridReference.gridPerspective.transform.position = cursorPos;
    }

    public void SetFieldScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = true;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;
        // helpTextCharName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        // helpTextCharName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[2].characterName;

        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        for (int i = 0; i < Engine.e.charClasses.Length; i++)
        {
            if (Engine.e.playableCharacters[2].classUnlocked[i] == true)
            {
                classConnectionToMiddle[i].SetActive(true);
            }
            else
            {
                classConnectionToMiddle[i].SetActive(false);
            }

            if (Engine.e.playableCharacters[2].characterClass[i] == true && Engine.e.playableCharacters[2].classEXP[i] == 100f)
            {
                Engine.e.playableCharacters[2].canSelectNewClass = true;
            }
            else
            {
                Engine.e.playableCharacters[2].canSelectNewClass = false;
            }
        }

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = Engine.e.playableCharacters[2].classEXP[i];
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null)
            {
                if (nodes[i].fieldUnlocked)
                {
                    for (int k = 0; k < nodes[i].connectionLines.Length; k++)
                    {
                        if (nodes[i].connectionLines[k] != null)
                        {
                            nodes[i].connectionLines[k].GetComponent<SpriteShapeRenderer>().color = Color.green;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.party[2].GetComponent<Character>().KnowsDrop(dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop))
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.party[2].GetComponent<Character>().KnowsSkill(skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill))
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        if (cursor.GetComponent<GridCursorMovement>().macPos == Vector3.zero)
        {
            Vector3 cursorPos = new Vector3(charStartingNodes[2].transform.position.x, charStartingNodes[2].transform.position.y, -5);
            cursor.transform.position = cursorPos;
        }
        else
        {
            cursor.transform.position = cursor.GetComponent<GridCursorMovement>().macPos;
        }
        cursor.GetComponent<GridCursorMovement>().switchingChar = false;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
        //Engine.e.gridReference.gridPerspective.transform.position = cursorPos;
    }
    public void SetRiggsScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = true;
        solaceScreen = false;
        blueScreen = false;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[3].characterName;

        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        for (int i = 0; i < Engine.e.charClasses.Length; i++)
        {
            if (Engine.e.playableCharacters[3].classUnlocked[i] == true)
            {
                classConnectionToMiddle[i].SetActive(true);
            }
            else
            {
                classConnectionToMiddle[i].SetActive(false);
            }

            if (Engine.e.playableCharacters[3].characterClass[i] == true && Engine.e.playableCharacters[3].classEXP[i] == 100f)
            {
                Engine.e.playableCharacters[3].canSelectNewClass = true;
            }
            else
            {
                Engine.e.playableCharacters[3].canSelectNewClass = false;
            }
        }

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = Engine.e.playableCharacters[3].classEXP[i];
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null)
            {
                if (nodes[i].riggsUnlocked)
                {
                    for (int k = 0; k < nodes[i].connectionLines.Length; k++)
                    {
                        if (nodes[i].connectionLines[k] != null)
                        {
                            nodes[i].connectionLines[k].GetComponent<SpriteShapeRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.party[3].GetComponent<Character>().KnowsDrop(dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop))
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.party[3].GetComponent<Character>().KnowsSkill(skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill))
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }
        if (cursor.GetComponent<GridCursorMovement>().riggsPos == Vector3.zero)
        {
            Vector3 cursorPos = new Vector3(charStartingNodes[3].transform.position.x, charStartingNodes[3].transform.position.y, -5);
            cursor.transform.position = cursorPos;
        }
        else
        {
            cursor.transform.position = cursor.GetComponent<GridCursorMovement>().riggsPos;
        }
        cursor.GetComponent<GridCursorMovement>().switchingChar = false;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
    }

    public void SetSolaceScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = true;
        blueScreen = false;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[3].characterName;

        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        for (int i = 0; i < Engine.e.charClasses.Length; i++)
        {
            if (Engine.e.playableCharacters[4].classUnlocked[i] == true)
            {
                classConnectionToMiddle[i].SetActive(true);
            }
            else
            {
                classConnectionToMiddle[i].SetActive(false);
            }

            if (Engine.e.playableCharacters[4].characterClass[i] == true && Engine.e.playableCharacters[4].classEXP[i] == 100f)
            {
                Engine.e.playableCharacters[4].canSelectNewClass = true;
            }
            else
            {
                Engine.e.playableCharacters[4].canSelectNewClass = false;
            }
        }

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = Engine.e.playableCharacters[4].classEXP[i];
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null)
            {
                if (nodes[i].riggsUnlocked)
                {
                    for (int k = 0; k < nodes[i].connectionLines.Length; k++)
                    {
                        if (nodes[i].connectionLines[k] != null)
                        {
                            nodes[i].connectionLines[k].GetComponent<SpriteShapeRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.party[4].GetComponent<Character>().KnowsDrop(dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop))
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.party[4].GetComponent<Character>().KnowsSkill(skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill))
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }
        if (cursor.GetComponent<GridCursorMovement>().solacePos == Vector3.zero)
        {
            Vector3 cursorPos = new Vector3(charStartingNodes[4].transform.position.x, charStartingNodes[4].transform.position.y, -5);
            cursor.transform.position = cursorPos;
        }
        else
        {
            cursor.transform.position = cursor.GetComponent<GridCursorMovement>().solacePos;
        }
        cursor.GetComponent<GridCursorMovement>().switchingChar = false;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
    }
    public void SetBlueScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = true;

        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        //helpTextCharName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[3].characterName;


        for (int i = 0; i < Engine.e.charClasses.Length; i++)
        {
            if (Engine.e.playableCharacters[5].classUnlocked[i] == true)
            {
                classConnectionToMiddle[i].SetActive(true);
            }
            else
            {
                classConnectionToMiddle[i].SetActive(false);
            }

            if (Engine.e.playableCharacters[5].characterClass[i] == true && Engine.e.playableCharacters[5].classEXP[i] == 100f)
            {
                Engine.e.playableCharacters[5].canSelectNewClass = true;
            }
            else
            {
                Engine.e.playableCharacters[5].canSelectNewClass = false;
            }
        }

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = Engine.e.playableCharacters[5].classEXP[i];
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null)
            {
                if (nodes[i].riggsUnlocked)
                {
                    for (int k = 0; k < nodes[i].connectionLines.Length; k++)
                    {
                        if (nodes[i].connectionLines[k] != null)
                        {
                            nodes[i].connectionLines[k].GetComponent<SpriteShapeRenderer>().color = Color.yellow;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.party[5].GetComponent<Character>().KnowsDrop(dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop))
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.party[5].GetComponent<Character>().KnowsSkill(skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill))
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }
        if (cursor.GetComponent<GridCursorMovement>().bluePos == Vector3.zero)
        {
            Vector3 cursorPos = new Vector3(charStartingNodes[5].transform.position.x, charStartingNodes[5].transform.position.y, -5);
            cursor.transform.position = cursorPos;
        }
        else
        {
            cursor.transform.position = cursor.GetComponent<GridCursorMovement>().bluePos;
        }
        cursor.GetComponent<GridCursorMovement>().switchingChar = false;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
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

    public void DisplayAbilitiesList()
    {
        abilitiesList.SetActive(true);
        abilitiesListDisplayed = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(dropsButtons[0]);
    }

    public void CloseAbilitiesList()
    {
        abilitiesList.SetActive(false);
        abilitiesListDisplayed = false;

    }

    public void CloseGrid()
    {
        Engine.e.canvasReference.SetActive(true);
        Engine.e.canvasReference.GetComponent<PauseMenu>().DisplayGrievePartyText();
        Engine.e.canvasReference.GetComponent<PauseMenu>().DisplayMacPartyText();
        Engine.e.canvasReference.GetComponent<PauseMenu>().DisplayFieldPartyText();
        Engine.e.canvasReference.GetComponent<PauseMenu>().DisplayRiggsPartyText();
        Engine.e.canvasReference.GetComponent<PauseMenu>().OpenPauseMenu();
        Engine.e.gridReference.gameObject.SetActive(false);
        gridDisplayed = false;
        abilitiesListDisplayed = false;

        helpReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        Time.timeScale = 0f;

        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;

        ClearConnectionLines();
    }

    // Sets lines to gray, mainly for character switching, as well as returning to the Pause Menu
    public void ClearConnectionLines()
    {
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
    }

    // For New Game
    public void SetupGrid()
    {
        gridDisplayed = false;

        for (int i = 1; i < classPaths.Length; i++)
        {
            classPaths[i].SetActive(false);
        }

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

        for (int i = 0; i < classProgressionBars.Length; i++)
        {
            classProgressionBars[i].GetComponent<Slider>().value = 0;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] != null)
            {
                nodes[i].GetComponent<SpriteRenderer>().color = Color.red;
                nodes[i].grieveUnlocked = false;
                nodes[i].macUnlocked = false;
                nodes[i].fieldUnlocked = false;
                nodes[i].riggsUnlocked = false;

                if (nodes[i].nodeIndex == 0 || nodes[i].nodeIndex == -1)
                    nodes[i].nodeIndex = i;
            }
        }
        // nodes[0].grieveUnlocked = true;
        //nodes[1].macUnlocked = true;
        //nodes[2].fieldUnlocked = true;
        // nodes[3].riggsUnlocked = true;

        //grievePosition = 0;
        // macPosition = 1;
        // fieldPosition = 2;
        // riggsPosition = 3;

        // Not sure why this needs to happen, but it does
        Engine.e.gridReference.gameObject.SetActive(true);
        Engine.e.gridReference.gameObject.SetActive(false);

        gridPerspective.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6000f;
        centerOfGridPerspective.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 20000f;

    }

    public void ActivateDropNode(int partyMember, Drops _drop)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null && nodes[i].node.drop != null)
            {
                if (nodes[i].node.drop == _drop)
                {
                    switch (partyMember)
                    {
                        case 0:
                            nodes[i].grieveUnlocked = true;
                            break;
                        case 1:
                            nodes[i].macUnlocked = true;
                            break;
                        case 2:
                            nodes[i].fieldUnlocked = true;
                            break;
                        case 3:
                            nodes[i].riggsUnlocked = true;
                            break;
                    }
                }
            }
        }
    }

    public void WarpToDropNode(Drops _drop)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null && nodes[i].node.drop != null)
            {
                if (nodes[i].node.drop == _drop)
                {
                    cursor.GetComponent<GridCursor>().currentAbilityStatNode = nodes[i];
                    Vector3 cursorPos = new Vector3(nodes[i].transform.position.x, nodes[i].transform.position.y, -5);

                    cursor.transform.position = cursorPos;
                    Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
                    Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;

                    cursor.GetComponent<GridCursor>().DisplayNodeInformation();

                    break;
                }
            }
        }
    }

    public void WarpToSkillNode(Skills _skill)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null && nodes[i].node.skill != null)
            {
                if (nodes[i].node.skill == _skill)
                {
                    cursor.GetComponent<GridCursor>().currentAbilityStatNode = nodes[i];
                    Vector3 cursorPos = new Vector3(nodes[i].transform.position.x, nodes[i].transform.position.y, -5);

                    cursor.transform.position = cursorPos;
                    Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
                    Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;

                    cursor.GetComponent<GridCursor>().DisplayNodeInformation();

                    break;
                }
            }
        }
    }
}

