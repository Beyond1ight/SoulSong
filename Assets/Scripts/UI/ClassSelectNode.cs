using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class ClassSelectNode : MonoBehaviour
{
    public Class node;

    public ClassSelectNode[] connectedNodes;
    public GameObject progressBar;

    public bool grieveUnlocked, macUnlocked, fieldUnlocked, riggsUnlocked;
    public int nodeIndex;


    private void Start()
    {
        if (node != null)
        {
            if (node.skill != null)
            {
                //node.className = node.skill.skillName;
                //node.classDescription = node.skill.skillDescription;
            }
        }
    }

    public void OnClickEvent()
    {
        bool connectionCheck = false;

        if (Engine.e.gridReference.grieveScreen)
        {
            if (node != null)
            {
                if (!grieveUnlocked)
                {
                    Engine.e.playableCharacters[0].classCompleted[nodeIndex] = true;
                    Engine.e.playableCharacters[0].currentClass = node.className;

                    for (int i = 0; i < connectedNodes.Length; i++)
                    {
                        if (connectedNodes[i].grieveUnlocked)
                        {
                            connectionCheck = true;
                            break;
                        }
                    }

                    if (connectionCheck)
                    {

                        if (node.skill != null)
                        {
                            //if (!Engine.e.party[0].GetComponent<Character>().KnowsSkill(node.skill))
                            //{
                            //    Engine.e.party[0].GetComponent<Character>().skills[node.skill.skillIndex] = node.skill;
                            //}
                        }

                        grieveUnlocked = true;
                        Debug.Log("Unlocked!");

                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }

        if (Engine.e.gridReference.macScreen)
        {
            if (node != null)
            {
                if (!macUnlocked)
                {
                    for (int i = 0; i < connectedNodes.Length; i++)
                    {
                        if (connectedNodes[i].macUnlocked)
                        {
                            connectionCheck = true;
                            break;
                        }
                    }

                    if (connectionCheck)
                    {

                        if (node.skill != null)
                        {

                        }

                        macUnlocked = true;

                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }

        if (Engine.e.gridReference.fieldScreen)
        {
            if (node != null)
            {
                if (!fieldUnlocked)
                {
                    for (int i = 0; i < connectedNodes.Length; i++)
                    {
                        if (connectedNodes[i].fieldUnlocked)
                        {
                            connectionCheck = true;
                            break;
                        }
                    }

                    if (connectionCheck)
                    {

                        if (node.skill != null)
                        {

                        }


                        fieldUnlocked = true;


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

            if (Engine.e.gridReference.riggsScreen)
            {
                if (node != null)
                {
                    if (!riggsUnlocked)
                    {
                        for (int i = 0; i < connectedNodes.Length; i++)
                        {
                            if (connectedNodes[i].riggsUnlocked)
                            {
                                connectionCheck = true;
                                break;
                            }
                        }

                        if (connectionCheck)
                        {

                            if (node.skill != null)
                            {

                            }

                            Engine.e.gridReference.riggsPosition = nodeIndex;

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
        }
    }
}
