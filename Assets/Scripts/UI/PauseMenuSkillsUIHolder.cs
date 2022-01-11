using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSkillsUIHolder : MonoBehaviour
{
    public Skills skill;

    public void OnClickEvent()
    {
        if (Engine.e.gridReference.gridDisplayed)
        {
            Engine.e.gridReference.abilitiesList.SetActive(false);
            Engine.e.gridReference.abilitiesListDisplayed = false;
            Engine.e.gridReference.WarpToSkillNode(skill);
        }
    }
}
