using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSkillSlot : MonoBehaviour
{
    public Skills skill;
    public int index;
    public GameObject skillName, skillDescription, scrollReference;



    public void SetHelpTextWeaponSkill()
    {
        if (skill != null)
        {
            Engine.e.helpText.text = skill.skillDescription;
        }
        else
        {
            Engine.e.helpText.text = string.Empty;
        }
    }
    public void ClearHelpTextWeaponSkill()
    {

        Engine.e.helpText.text = string.Empty;

    }
    public void OnClickEvent()
    {
        Engine.e.augmentMenuReference.weaponSkillSlotReference = index;

        Engine.e.augmentMenuReference.SelectSkill();
    }
}
