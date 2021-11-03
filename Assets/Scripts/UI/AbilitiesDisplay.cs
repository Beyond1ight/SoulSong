using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AbilitiesDisplay : MonoBehaviour
{
    public GameObject[] charSelectionButtons;
    public bool grieveScreen, macScreen, fieldScreen, riggsScreen;
    public GameObject[] skillsButtons, fireDropsButtons, iceDropsButtons, lightningDropsButtons, waterDropsButtons, shadowDropsButtons, holyDropsButtons;
    public bool knowsAllFireDrops = false, knowsAllIceDrops = false, knowsAllLightningDrops = false, knowsAllWaterDrops = false, knowsAllShadowDrops = false, knowsAllHolyDrops = false;
    public GameObject confirmSkillAcquisition;
    public TextMeshProUGUI confirmSkillSentence;
    public TextMeshProUGUI[] apDisplay;
    public int tier;

    public void UnlockSkillChoiceCheck(int _tier)
    {
        string apCost = string.Empty;

        if (Engine.e.gameSkills[_tier] != null)
        {
            apCost = "Costs " + Engine.e.gameSkills[_tier].abilityPointCost + "AP";
        }
        else
        {
            return;
        }

        if (grieveScreen)
        {
            Character grieve = Engine.e.party[0].GetComponent<Grieve>();
            if (grieve.lvl > 20)
            {
                if (grieve.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (grieve.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = string.Empty;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Grieve? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
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
            else
            {
                return;
            }
        }

        if (macScreen)
        {
            Character mac = Engine.e.party[1].GetComponent<Mac>();

            if (mac.lvl > 20)
            {
                if (mac.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (mac.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Mac? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
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
            else
            {
                return;
            }
        }

        if (fieldScreen)
        {
            Character field = Engine.e.party[2].GetComponent<Field>();

            if (field.lvl > 20)
            {
                if (field.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (field.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Field? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
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
            else
            {
                return;
            }
        }

        if (riggsScreen)
        {
            Character riggs = Engine.e.party[3].GetComponent<Riggs>();

            if (riggs.lvl > 20)
            {
                if (riggs.availableSkillPoints >= Engine.e.gameSkills[_tier].abilityPointCost)
                {
                    if (riggs.skills[_tier] == null)
                    {
                        confirmSkillAcquisition.SetActive(true);
                        tier = _tier;
                        confirmSkillSentence.text = "Unlock " + Engine.e.gameSkills[_tier].skillName + " for Riggs? " + apCost;
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(confirmSkillAcquisition);
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
            else
            {
                return;
            }
        }
    }

    public void UnlockSkillChoice()
    {
        if (Engine.e.gameSkills[tier] != null)
        {
            if (grieveScreen)
            {
                Character grieve = Engine.e.party[0].GetComponent<Grieve>();

                grieve.skills[tier] = Engine.e.gameSkills[tier];
                grieve.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                grieve.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;

            }

            if (macScreen)
            {
                Character mac = Engine.e.party[1].GetComponent<Mac>();

                mac.skills[tier] = Engine.e.gameSkills[tier];
                mac.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                mac.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;
            }

            if (fieldScreen)
            {
                Character field = Engine.e.party[2].GetComponent<Field>();

                field.skills[tier] = Engine.e.gameSkills[tier];
                field.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                field.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;

            }

            if (riggsScreen)
            {
                Character riggs = Engine.e.party[3].GetComponent<Riggs>();

                riggs.skills[tier] = Engine.e.gameSkills[tier];
                riggs.availableSkillPoints -= Engine.e.gameSkills[tier].abilityPointCost;
                riggs.skillTotal++;
                skillsButtons[tier].GetComponentInChildren<TMP_Text>().color = Color.black;

            }

            confirmSkillSentence.text = string.Empty;
            confirmSkillAcquisition.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(skillsButtons[tier]);

            DisplayAP();
        }
        else
        {
            return;
        }
    }

    public void DenySkillUnlock()
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(skillsButtons[tier]);
        confirmSkillAcquisition.SetActive(false);
        confirmSkillSentence.text = string.Empty;
        tier = -1;
    }

    public void SetSkills()
    {
        Character grieve = Engine.e.party[0].GetComponent<Grieve>();
        Character mac = null;
        Character field = null;
        Character riggs = null;

        if (Engine.e.party[1] != null)
        {
            mac = Engine.e.party[1].GetComponent<Mac>();
        }
        if (Engine.e.party[2] != null)
        {
            field = Engine.e.party[2].GetComponent<Field>();
        }
        if (Engine.e.party[3] != null)
        {
            riggs = Engine.e.party[3].GetComponent<Riggs>();
        }

        if (grieve.skillTotal <= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if (grieve.skills[i] != null)
                {
                    if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (grieve.skills[i] != null)
                        {
                            skillsButtons[i].GetComponentInChildren<TMP_Text>().text = grieve.skills[i].skillName;
                        }
                    }
                }
            }
        }

        if (mac != null)
        {
            if (mac.skillTotal <= 5)
            {
                for (int i = 5; i < 10; i++)
                {
                    if (mac.skills[i] != null)
                    {
                        if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                        {
                            skillsButtons[i].GetComponentInChildren<TMP_Text>().text = mac.skills[i].skillName;
                        }
                    }
                }
            }
        }

        if (field != null)
        {
            if (field.skillTotal <= 5)
            {
                for (int i = 10; i < 15; i++)
                {
                    if (field.skills[i] != null)
                    {
                        if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                        {
                            skillsButtons[i].GetComponentInChildren<TMP_Text>().text = field.skills[i].skillName;
                        }
                    }
                }
            }
        }

        if (riggs != null)
        {
            if (riggs.skillTotal <= 5)
            {
                for (int i = 15; i < 20; i++)
                {
                    if (riggs.skills[i] != null)
                    {
                        if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                        {
                            skillsButtons[i].GetComponentInChildren<TMP_Text>().text = riggs.skills[i].skillName;
                        }
                    }
                }
            }
        }

        tier = -1;
    }

    public void SetDrops()
    {

        for (int i = 0; i < 10; i++)
        {
            if (!knowsAllFireDrops)
            {
                if (Engine.e.fireDrops[i] != null)
                {
                    if (fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.fireDrops[i].isKnown)
                        {
                            fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.fireDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllIceDrops)
            {
                if (Engine.e.iceDrops[i] != null)
                {
                    if (iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.iceDrops[i].isKnown)
                        {
                            iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.iceDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllLightningDrops)
            {
                if (Engine.e.lightningDrops[i] != null)
                {
                    if (lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.lightningDrops[i].isKnown)
                        {
                            lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.lightningDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllWaterDrops)
            {
                if (Engine.e.waterDrops[i] != null)
                {
                    if (waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.waterDrops[i].isKnown)
                        {
                            waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.waterDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllShadowDrops)
            {
                if (Engine.e.shadowDrops[i] != null)
                {
                    if (shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.shadowDrops[i].isKnown)
                        {
                            shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.shadowDrops[i].dropName;
                        }
                    }
                }
            }

            if (!knowsAllHolyDrops)
            {
                if (Engine.e.holyDrops[i] != null)
                {
                    if (holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text == "-")
                    {
                        if (Engine.e.holyDrops[i].isKnown)
                        {
                            holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text = Engine.e.holyDrops[i].dropName;
                        }
                    }
                }
            }
        }
    }

    public void SetGrieveScreen()
    {
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        grieveScreen = true;

        if (Engine.e.party[1] != null)
        {
            charSelectionButtons[1].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charSelectionButtons[2].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[3] != null)
        {
            charSelectionButtons[3].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        for (int i = 0; i < skillsButtons.Length; i++)
        {
            if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().skills[i] == null)
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().fireDrops[i] == null)
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().iceDrops[i] == null)
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().lightningDrops[i] == null)
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().waterDrops[i] == null)
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().shadowDrops[i] == null)
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[0].GetComponent<Grieve>().holyDrops[i] == null)
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }
        }
    }

    public void SetMacScreen()
    {
        grieveScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        macScreen = true;

        if (Engine.e.party[0] != null)
        {
            charSelectionButtons[0].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charSelectionButtons[2].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[3] != null)
        {
            charSelectionButtons[3].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }


        for (int i = 0; i < skillsButtons.Length; i++)
        {
            if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().skills[i] == null)
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().fireDrops[i] == null)
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().iceDrops[i] == null)
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().lightningDrops[i] == null)
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().waterDrops[i] == null)
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().shadowDrops[i] == null)
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[1].GetComponent<Mac>().holyDrops[i] == null)
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }
        }
    }

    public void SetFieldScreen()
    {
        grieveScreen = false;
        macScreen = false;
        riggsScreen = false;
        fieldScreen = true;

        if (Engine.e.party[0] != null)
        {
            charSelectionButtons[0].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charSelectionButtons[1].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[3] != null)
        {
            charSelectionButtons[3].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }


        for (int i = 0; i < skillsButtons.Length; i++)
        {
            if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().skills[i] == null)
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().fireDrops[i] == null)
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().iceDrops[i] == null)
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().lightningDrops[i] == null)
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().waterDrops[i] == null)
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().shadowDrops[i] == null)
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[2].GetComponent<Field>().holyDrops[i] == null)
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }
        }
    }

    public void SetRiggsScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = true;

        if (Engine.e.party[0] != null)
        {
            charSelectionButtons[0].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charSelectionButtons[1].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charSelectionButtons[2].GetComponentInChildren<TMP_Text>().color = Color.gray;
        }

        for (int i = 0; i < skillsButtons.Length; i++)
        {
            if (skillsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().skills[i] == null)
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (fireDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().fireDrops[i] == null)
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (iceDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().iceDrops[i] == null)
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().lightningDrops[i] == null)
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (waterDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().waterDrops[i] == null)
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().shadowDrops[i] == null)
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            if (holyDropsButtons[i].GetComponentInChildren<TMP_Text>().text != "-")
            {
                if (Engine.e.party[3].GetComponent<Riggs>().holyDrops[i] == null)
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.gray;
                }
                else
                {
                    holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }
        }
    }

    public void ClearScreen()
    {
        if (grieveScreen == true || macScreen == true || fieldScreen == true || riggsScreen == true)
        {
            grieveScreen = false;
            macScreen = false;
            fieldScreen = false;
            riggsScreen = false;


            for (int i = 0; i < charSelectionButtons.Length; i++)
            {
                if (Engine.e.party[i] != null)
                {
                    charSelectionButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }

            for (int i = 0; i < skillsButtons.Length; i++)
            {

                skillsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
            }

            for (int i = 0; i < 10; i++)
            {

                fireDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                iceDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                lightningDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                waterDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                shadowDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;

                holyDropsButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
            }
        }

        tier = -1;
    }

    public void DisplayAP()
    {
        for (int i = 0; i < apDisplay.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                if (Engine.e.party[i].GetComponent<Character>().lvl >= 20)
                {
                    apDisplay[i].text = Engine.e.party[i].GetComponent<Character>().availableSkillPoints.ToString();
                }
            }
        }
    }
}

