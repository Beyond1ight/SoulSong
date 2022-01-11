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
    public bool gridDisplayed, abilitiesListDisplayed, grieveScreen, macScreen, fieldScreen, riggsScreen;
    public int grievePosition, macPosition, fieldPosition, riggsPosition;
    public GameObject cursor, helpReference, helpTextParentObj, abilitiesList;
    public CinemachineVirtualCamera gridPerspective, centerOfGridPerspective;
    public GameObject[] dropsButtons, skillButtons;

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

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].node != null)
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
        cursor.GetComponent<GridCursor>().currentNode = nodes[grievePosition];
        Vector3 cursorPos = new Vector3(nodes[grievePosition].transform.position.x, nodes[grievePosition].transform.position.y, -5);

        cursor.transform.position = cursorPos;
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
        //cursor.GetComponent<GridCursorMovement>().isMoving = false;
        cursor.GetComponent<GridCursor>().currentNode = nodes[macPosition];
        Vector3 cursorPos = new Vector3(nodes[macPosition].transform.position.x, nodes[macPosition].transform.position.y, -5);

        cursor.transform.position = cursorPos;
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

        //cursor.GetComponent<GridCursorMovement>().isMoving = false;
        cursor.GetComponent<GridCursor>().currentNode = nodes[fieldPosition];
        Vector3 cursorPos = new Vector3(nodes[fieldPosition].transform.position.x, nodes[fieldPosition].transform.position.y, -5);

        cursor.transform.position = cursorPos;
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
        //cursor.GetComponent<GridCursorMovement>().isMoving = false;
        cursor.GetComponent<GridCursor>().currentNode = nodes[riggsPosition];
        Vector3 cursorPos = new Vector3(nodes[riggsPosition].transform.position.x, nodes[riggsPosition].transform.position.y, -5);

        cursor.transform.position = cursorPos;
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

        // Not sure why this needs to happen, but it does
        Engine.e.gridReference.gameObject.SetActive(true);
        Engine.e.gridReference.gameObject.SetActive(false);
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
                    cursor.GetComponent<GridCursor>().currentNode = nodes[i];
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
                    cursor.GetComponent<GridCursor>().currentNode = nodes[i];
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

