using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilityStatNode : MonoBehaviour
{
    public AbilityStat node;

    public AbilityStatNode[] connectedNodes;
    public GameObject[] connectionLines;

    public bool grieveUnlocked, macUnlocked, fieldUnlocked, riggsUnlocked;
    public int nodeIndex;



    public void OnClickEvent()
    {
        bool connectionCheck = false;

        if (Engine.e.abilityScreenReference.grieveScreen)
        {
            if (node != null)
            {
                if (!grieveUnlocked)
                {
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
                        Engine.e.party[0].GetComponent<Character>().maxHealth += node.healthIncrease;
                        Engine.e.party[0].GetComponent<Character>().maxMana += node.manaIncrease;
                        Engine.e.party[0].GetComponent<Character>().maxEnergy += node.energyIncrease;
                        Engine.e.party[0].GetComponent<Character>().baseDamage += node.strengthIncrease;

                        if (node.skill != null)
                        {
                            if (!Engine.e.party[0].GetComponent<Character>().KnowsSkill(node.skill))
                            {
                                Engine.e.party[0].GetComponent<Character>().skills[node.skill.index] = node.skill;
                            }
                        }

                        if (node.drop != null)
                        {
                            if (!Engine.e.party[0].GetComponent<Character>().KnowsDrop(node.drop))
                            {
                                Engine.e.party[0].GetComponent<Character>().drops[node.drop.dropIndex] = node.drop;
                            }
                        }
                        grieveUnlocked = true;
                        Debug.Log("Unlocked!");

                        for (int i = 0; i < connectionLines.Length; i++)
                        {
                            if (connectionLines[i] != null)
                            {
                                connectionLines[i].GetComponent<Image>().color = Color.white;
                            }
                        }
                        Engine.e.abilityScreenReference.grievePosition = nodeIndex;
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

        if (Engine.e.abilityScreenReference.macScreen)
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
                        Engine.e.party[1].GetComponent<Character>().maxHealth += node.healthIncrease;
                        Engine.e.party[1].GetComponent<Character>().maxMana += node.manaIncrease;
                        Engine.e.party[1].GetComponent<Character>().maxEnergy += node.energyIncrease;
                        Engine.e.party[1].GetComponent<Character>().baseDamage += node.strengthIncrease;

                        if (node.skill != null)
                        {
                            if (!Engine.e.party[1].GetComponent<Character>().KnowsSkill(node.skill))
                            {
                                Engine.e.party[1].GetComponent<Character>().skills[node.skill.index] = node.skill;
                            }
                        }

                        if (node.drop != null)
                        {
                            if (!Engine.e.party[1].GetComponent<Character>().KnowsDrop(node.drop))
                            {
                                Engine.e.party[1].GetComponent<Character>().drops[node.drop.dropIndex] = node.drop;
                            }
                            Debug.Log("Unlocked!");

                        }
                        macUnlocked = true;
                        for (int i = 0; i < connectionLines.Length; i++)
                        {
                            if (connectionLines[i] != null)
                            {
                                connectionLines[i].GetComponent<Image>().color = Color.white;
                            }
                        }
                        Engine.e.abilityScreenReference.macPosition = nodeIndex;

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

        if (Engine.e.abilityScreenReference.fieldScreen)
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
                        Engine.e.party[2].GetComponent<Character>().maxHealth += node.healthIncrease;
                        Engine.e.party[2].GetComponent<Character>().maxMana += node.manaIncrease;
                        Engine.e.party[2].GetComponent<Character>().maxEnergy += node.energyIncrease;
                        Engine.e.party[2].GetComponent<Character>().baseDamage += node.strengthIncrease;

                        if (node.skill != null)
                        {
                            if (!Engine.e.party[2].GetComponent<Character>().KnowsSkill(node.skill))
                            {
                                Engine.e.party[2].GetComponent<Character>().skills[node.skill.index] = node.skill;
                            }
                        }

                        if (node.drop != null)
                        {
                            if (!Engine.e.party[2].GetComponent<Character>().KnowsDrop(node.drop))
                            {
                                Engine.e.party[2].GetComponent<Character>().drops[node.drop.dropIndex] = node.drop;
                            }
                        }
                        fieldUnlocked = true;
                        for (int i = 0; i < connectionLines.Length; i++)
                        {
                            if (connectionLines[i] != null)
                            {
                                connectionLines[i].GetComponent<Image>().color = Color.white;
                            }
                        }
                        Engine.e.abilityScreenReference.fieldPosition = nodeIndex;

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

            if (Engine.e.abilityScreenReference.riggsScreen)
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
                            Engine.e.party[3].GetComponent<Character>().maxHealth += node.healthIncrease;
                            Engine.e.party[3].GetComponent<Character>().maxMana += node.manaIncrease;
                            Engine.e.party[3].GetComponent<Character>().maxEnergy += node.energyIncrease;
                            Engine.e.party[3].GetComponent<Character>().baseDamage += node.strengthIncrease;

                            if (node.skill != null)
                            {
                                if (!Engine.e.party[3].GetComponent<Character>().KnowsSkill(node.skill))
                                {
                                    Engine.e.party[3].GetComponent<Character>().skills[node.skill.index] = node.skill;
                                }
                            }

                            if (node.drop != null)
                            {
                                if (!Engine.e.party[3].GetComponent<Character>().KnowsDrop(node.drop))
                                {
                                    Engine.e.party[3].GetComponent<Character>().drops[node.drop.dropIndex] = node.drop;
                                }
                            }
                            riggsUnlocked = true;
                            for (int i = 0; i < connectionLines.Length; i++)
                            {
                                if (connectionLines[i] != null)
                                {
                                    connectionLines[i].GetComponent<Image>().color = Color.white;
                                }
                            }
                            Engine.e.abilityScreenReference.riggsPosition = nodeIndex;

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