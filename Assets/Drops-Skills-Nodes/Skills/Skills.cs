using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skills : ScriptableObject
{
    public float skillPower;
    public float skillCost;
    public string skillName;
    public string skillDescription;
    public float skillPointReturn = 0;
    public int abilityPointCost;
    public bool physicalDps, rangedDps;
    public bool selfSupport;
    public bool targetSupport;
    public int skillIndex;

}
