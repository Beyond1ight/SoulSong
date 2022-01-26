using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{


    public float skillPower;
    public float skillCost;
    public string skillName;
    public string skillDescription;
    public float skillPointReturn = 0;
    public int abilityPointCost;
    public bool physicalDps, rangedDps, selfSupport, targetSupport, targetAll;
    public bool instantCast;
    public int skillIndex;
    public bool isKnown;

    public void SpriteDamageFlash()
    {
        GetComponent<DisplayedAnimationControl>().CallDamageFlash();
    }
}
