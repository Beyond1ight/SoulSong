using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridCursor : MonoBehaviour
{
    public AbilityStatNode currentNode;
    public GameObject cursorSprite, helpText;
    public bool nodeSet;
    //public AbilityStatNode[] nodeDirections;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Grid Node" && !Engine.e.gridReference.cursor.GetComponent<GridCursorMovement>().isMoving && !nodeSet)
        {
            currentNode = other.GetComponent<AbilityStatNode>();

            Vector3 cursorPos = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, -5);


            transform.position = cursorPos;
            cursorSprite.transform.localPosition = new Vector2(-1, 0);
            nodeSet = true;
            helpText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Grid Node" && Engine.e.gridReference.cursor.GetComponent<GridCursorMovement>().isMoving && nodeSet)
        {
            currentNode = null;
            cursorSprite.transform.localPosition = new Vector2(0, 0);
            nodeSet = false;
            helpText.SetActive(false);

        }
    }

    public void SetGridPerspective()
    {
        if (Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize == 150f)
        {
            Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 200f;
            GetComponent<GridCursorMovement>().speed = 200f;
        }
        else
        {
            if (Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize == 200f)
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
                        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 150f;
                        Engine.e.gridReference.centerOfGridPerspective.gameObject.SetActive(false);
                        GetComponent<GridCursorMovement>().speed = 100f;
                        cursorSprite.SetActive(true);
                        Engine.e.gridReference.helpTextParentObj.SetActive(true);

                    }
                }
            }
        }
    }


    public void DisplayNodeInformation()
    {
        if (currentNode != null)
        {
            if (currentNode.node != null)
            {
                if (currentNode.node.nodeName == string.Empty)
                {
                    if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != "Name not set.")
                        helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Name not set.";
                }
                else
                {
                    if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != currentNode.node.nodeName + " - " + currentNode.node.nodeDescription)
                        helpText.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.node.nodeName + " - " + currentNode.node.nodeDescription;
                }
            }
            else
            {
                if (helpText.GetComponentInChildren<TextMeshProUGUI>().text != "Node not set.")
                    helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Node not set.";
            }
        }
    }

    public void ClearNodeInformation()
    {
        if (currentNode == null && helpText.GetComponentInChildren<TextMeshProUGUI>().text != string.Empty)
        {
            helpText.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
    }
    public void OnClickEvent()
    {
        if (currentNode != null && !Engine.e.gridReference.abilitiesListDisplayed)
        {
            currentNode.OnClickEvent();
        }
    }
}
