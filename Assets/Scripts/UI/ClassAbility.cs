using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Node", menuName = "ClassAbility", order = 0)]
public class ClassAbility : ScriptableObject
{
    public string nodeName, nodeDescription;
    public Skills skill;
}
