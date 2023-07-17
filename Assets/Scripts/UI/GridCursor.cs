using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridCursor : MonoBehaviour
{
    public AbilityStatNode currentAbilityStatNode;
    public ClassSelectNode currentClassSelectNode;
    public GameObject cursorSprite, helpText;
    public bool nodeSet;

    //public AbilityStatNode[] nodeDirections;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Grid AbilityStat Node" && !Engine.e.gridReference.cursor.GetComponent<GridCursorMovement>().isMoving && !nodeSet)
        {
            currentAbilityStatNode = other.GetComponent<AbilityStatNode>();

            Vector3 cursorPos = new Vector3(currentAbilityStatNode.transform.position.x, currentAbilityStatNode.transform.position.y, -5);


            transform.position = cursorPos;
            cursorSprite.transform.localPosition = new Vector2(-1.3f, 0);
            nodeSet = true;
            helpText.SetActive(true);
        }
        else
        {
            if (other.tag == "Grid Class Node" && !Engine.e.gridReference.cursor.GetComponent<GridCursorMovement>().isMoving && !nodeSet)
            {
                currentClassSelectNode = other.GetComponent<ClassSelectNode>();

                Vector3 cursorPos = new Vector3(currentClassSelectNode.transform.position.x, currentClassSelectNode.transform.position.y, -5);


                transform.position = cursorPos;
                cursorSprite.transform.localPosition = new Vector2(-1.3f, 0);
                nodeSet = true;
                helpText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Grid AbilityStat Node" || other.tag == "Grid Class Node" && Engine.e.gridReference.cursor.GetComponent<GridCursorMovement>().isMoving && nodeSet)
        {
            currentAbilityStatNode = null;
            currentClassSelectNode = null;
            cursorSprite.transform.localPosition = new Vector2(0, 0);
            nodeSet = false;
            helpText.SetActive(false);

        }
    }

    public void SetGridPerspective()
    {
        if (Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize == 20000f)
        {
            Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 30000f;
            GetComponent<GridCursorMovement>().speed = 300f;
        }
        else
        {
            if (Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize == 30000f)
            {
                Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 300f;
                GetComponent<GridCursorMovement>().speed = 300f;
            }
            else
            {
                if (Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize == 300f)
                {
                    Engine.e.gridReference.centerOfGridPerspective.gameObject.SetActive(true);
                    Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 301f;
                    if (Engine.e.gridReference.centerOfGridPerspective.m_Lens.OrthographicSize != 2000f)
                    {
                        Engine.e.gridReference.centerOfGridPerspective.m_Lens.OrthographicSize = 2000f;
                    }
                    cursorSprite.SetActive(false);
                    Engine.e.gridReference.helpTextParentObj.SetActive(false);
                    GetComponent<GridCursorMovement>().speed = 0f;
                }
                else
                {
                    if (Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize == 301f)
                    {
                        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 20000f;
                        Engine.e.gridReference.centerOfGridPerspective.gameObject.SetActive(false);
                        GetComponent<GridCursorMovement>().speed = 200f;
                        cursorSprite.SetActive(true);
                        Engine.e.gridReference.helpTextParentObj.SetActive(true);

                    }
                }
            }
        }
    }


    public void DisplayNodeInformation()
    {
        if (currentAbilityStatNode != null)
        {
            if (currentAbilityStatNode.node != null)
            {
                if (currentAbilityStatNode.node.nodeName == string.Empty)
                {
                    if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != "Name not set.")
                        helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Name not set.";
                }
                else
                {
                    if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != currentAbilityStatNode.node.nodeName + " - " + currentAbilityStatNode.node.nodeDescription)
                    {
                        if (currentAbilityStatNode != null)
                        {
                            helpText.GetComponentInChildren<TextMeshProUGUI>().text = currentAbilityStatNode.node.nodeName + " - " + currentAbilityStatNode.node.nodeDescription;
                        }
                    }
                }
            }
        }
        else
        {
            if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != "Node not set.")
                helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Node not set.";
        }

        if (currentClassSelectNode != null)
        {
            if (currentClassSelectNode.node != null)
            {
                if (currentClassSelectNode.node.className == string.Empty)
                {
                    if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != "Name not set.")
                        helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Name not set.";
                }
                else
                {
                    if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != currentClassSelectNode.node.className + " - " + currentClassSelectNode.node.classDescription)
                    {
                        if (currentClassSelectNode != null)
                        {
                            helpText.GetComponentInChildren<TextMeshProUGUI>().text = currentClassSelectNode.node.className + " - " + currentClassSelectNode.node.classDescription;
                        }
                    }
                }
            }
        }
        else
        {
            if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != "Class not set.")
                helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Class not set.";
        }
    }


    public void ClearNodeInformation()
    {
        if (currentAbilityStatNode == null || currentClassSelectNode == null && helpText.GetComponentInChildren<TextMeshProUGUI>().text != string.Empty)
        {
            helpText.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
    }
    public void OnClickEvent()
    {
        if (currentAbilityStatNode != null && !Engine.e.gridReference.abilitiesListDisplayed)
        {
            currentAbilityStatNode.OnClickEvent();
        }
        else
        {
            if (currentClassSelectNode != null && !Engine.e.gridReference.abilitiesListDisplayed)
            {
                currentClassSelectNode.OnClickEvent();
            }
        }
    }
}
