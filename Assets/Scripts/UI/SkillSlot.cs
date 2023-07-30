using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Skills skill;
    public int index;
    public GameObject skillName, skillDescription, scrollReference;

    public void SetHelpTextSkill()
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

    public void ClearHelpTextSkill()
    {

        Engine.e.helpText.text = string.Empty;

    }

    public void OnClickEvent()
    {
        Engine.e.augmentMenuReference.AugmentSkill(skill);
    }
}
