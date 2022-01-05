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
        if (other.tag == "Grid Node" && !Engine.e.abilityScreenReference.cursor.GetComponent<GridCursorMovement>().isMoving && !nodeSet)
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
        if (other.tag == "Grid Node" && Engine.e.abilityScreenReference.cursor.GetComponent<GridCursorMovement>().isMoving && nodeSet)
        {
            currentNode = null;
            cursorSprite.transform.localPosition = new Vector2(0, 0);
            nodeSet = false;
            helpText.SetActive(false);
            //helpText.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        }
    }

    public void SetGridPerspective()
    {
        Debug.Log("Test");

        if (Engine.e.abilityScreenReference.gridPerspective.m_Lens.OrthographicSize == 30)
        {
            Debug.Log("Test2");

            Engine.e.abilityScreenReference.gridPerspective.m_Lens.OrthographicSize = 65;
            GetComponent<GridCursorMovement>().speed = 48f;
        }
        else
        {
            if (Engine.e.abilityScreenReference.gridPerspective.m_Lens.OrthographicSize == 65)
            {
                Engine.e.abilityScreenReference.gridPerspective.m_Lens.OrthographicSize = 100;
                GetComponent<GridCursorMovement>().speed = 78f;
            }
            else
            {
                if (Engine.e.abilityScreenReference.gridPerspective.m_Lens.OrthographicSize == 100)
                {
                    Engine.e.abilityScreenReference.gridPerspective.m_Lens.OrthographicSize = 30;
                    GetComponent<GridCursorMovement>().speed = 28f;
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
                    helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Name not set.";
                }
                else
                {
                    helpText.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.node.nodeName + " - " + currentNode.node.nodeDescription;
                }
            }
            else
            {
                helpText.GetComponentInChildren<TextMeshProUGUI>().text = "Node not set.";
            }
        }
    }
    public void OnClickEvent()
    {
        if (currentNode != null)
        {
            currentNode.OnClickEvent();
        }
    }
}
