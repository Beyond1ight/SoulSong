using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop", menuName = "Drops")]
public class Drops : ScriptableObject
{
    public string dropType;
    public string dropName;
    public string dropDescription;
    public float dropCost;
    public float dropPower;
    public float dotDmg;
    public float experiencePoints;
    public bool dps, support, targetAll;
    public int dropValue;
    public int dropIndex;
    public bool isKnown = false;
    public AnimationClip animationClip;


}
