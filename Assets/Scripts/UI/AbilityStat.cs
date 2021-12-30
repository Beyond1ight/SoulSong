using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Node", menuName = "AbilityStat", order = 0)]
public class AbilityStat : ScriptableObject
{
    public string nodeName, nodeDescription;
    public float nodeCost, healthIncrease, manaIncrease, energyIncrease, strengthIncrease;
    public Skills skill;
    public Drops drop;

}
