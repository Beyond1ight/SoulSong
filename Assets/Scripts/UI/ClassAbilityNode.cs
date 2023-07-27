using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassAbilityNode : MonoBehaviour
{
    public ClassAbility node;
    public int classExperienceRequired;
    public AbilityStatNode[] connectedNodes;
    public GameObject[] connectionLines;

    public bool grieveUnlocked, macUnlocked, fieldUnlocked, riggsUnlocked, solaceUnlocked, blueUnlocked;
    public int nodeIndex;
    public bool skillNode;


    private void Start()
    {
        if (node != null)
        {
            if (node.skill != null)
            {
                node.nodeName = node.skill.skillName;
                node.nodeDescription = node.skill.skillDescription;
            }
        }
    }
}
