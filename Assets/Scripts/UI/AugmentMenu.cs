using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AugmentMenu : MonoBehaviour
{
    public int weaponSkillSlotReference, weaponPointerIndex, skillPointerIndex = 0, vertMove = 0;
    bool pressUp, pressDown, pressRelease = false;
    public GameObject[] weaponSkillSlots, skillSlots;
    public GameObject weaponReference, skillNameReference, skillDescriptionReference;
    public bool grieveScreen, macScreen, fieldScreen, riggsScreen, solaceScreen, blueScreen, selectingWeapon, selectingSkill, removingSkill;

    public void SetGrieveScreen()
    {
        grieveScreen = true;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;

        weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        if (Engine.e.playableCharacters[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.playableCharacters[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().itemName + " - " + Engine.e.playableCharacters[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount + " Slots";
        }
        else
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = "Nothing Equipped.";
        }

        for (int i = 0; i < Engine.e.playableCharacters[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount; i++)
        {
            if (Engine.e.playableCharacters[0].equippedSkills[i] != null)
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skill = Engine.e.playableCharacters[0].equippedSkills[i];
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[0].equippedSkills[i].skillName;

            }
            else
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";
            }

            weaponSkillSlots[i].SetActive(true);

        }

        for (int i = 0; i < Engine.e.playableCharacters[0].GetComponent<Character>().skills.Length; i++)
        {
            if (Engine.e.playableCharacters[0].GetComponent<Character>().skills[i] != null)
            {
                skillSlots[i].GetComponent<SkillSlot>().skill = Engine.e.playableCharacters[0].GetComponent<Character>().skills[i];
                skillSlots[i].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[0].skills[i].skillName;
                //skillSlots[i].GetComponent<SkillSlot>().skillDescription.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[0].skills[i].skillDescription;

                skillSlots[i].SetActive(true);
            }
        }
        /*if (Engine.e.party[1] != null)
        {
            charTMP[1].color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charTMP[2].color = Color.gray;
        }
        if (Engine.e.party[3] != null)
        {
            charTMP[3].color = Color.gray;
        }*/

        selectingWeapon = true;
        selectingSkill = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(weaponReference);

    }
    public void SetMacScreen()
    {
        grieveScreen = false;
        macScreen = true;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;

        weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        if (Engine.e.playableCharacters[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.playableCharacters[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().itemName + " - " + Engine.e.playableCharacters[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount + " Slots";
        }
        else
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = "Nothing Equipped.";
        }

        for (int i = 0; i < Engine.e.playableCharacters[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount; i++)
        {
            if (Engine.e.playableCharacters[1].equippedSkills[i] != null)
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skill = Engine.e.playableCharacters[1].equippedSkills[i];
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[1].equippedSkills[i].skillName;

            }
            else
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";
            }

            weaponSkillSlots[i].SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponSkillSlots[1].gameObject);
        }

        for (int i = 0; i < Engine.e.playableCharacters[1].GetComponent<Character>().skills.Length; i++)
        {
            if (Engine.e.playableCharacters[1].GetComponent<Character>().skills[i] != null)
            {
                skillSlots[i].GetComponent<SkillSlot>().skill = Engine.e.playableCharacters[1].GetComponent<Character>().skills[i];
                skillSlots[i].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[1].skills[i].skillName;

                skillSlots[i].SetActive(true);
            }
        }

        selectingWeapon = true;
        selectingSkill = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(weaponReference);
    }

    public void SetFieldScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = true;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;

        weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        if (Engine.e.playableCharacters[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.playableCharacters[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().itemName + " - " + Engine.e.playableCharacters[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount + " Slots";
        }
        else
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = "Nothing Equipped.";
        }

        for (int i = 0; i < Engine.e.playableCharacters[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount; i++)
        {
            if (Engine.e.playableCharacters[2].equippedSkills[i] != null)
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skill = Engine.e.playableCharacters[2].equippedSkills[i];
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[2].equippedSkills[i].skillName;

            }
            else
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";
            }

            weaponSkillSlots[i].SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponSkillSlots[2].gameObject);
        }

        for (int i = 0; i < Engine.e.playableCharacters[2].GetComponent<Character>().skills.Length; i++)
        {
            if (Engine.e.playableCharacters[2].GetComponent<Character>().skills[i] != null)
            {
                skillSlots[i].GetComponent<SkillSlot>().skill = Engine.e.playableCharacters[2].GetComponent<Character>().skills[i];
                skillSlots[i].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[2].skills[i].skillName;

                skillSlots[i].SetActive(true);
            }
        }

        selectingWeapon = true;
        selectingSkill = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(weaponReference);

    }

    public void SetRiggsScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = true;
        solaceScreen = false;
        blueScreen = false;

        weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        if (Engine.e.playableCharacters[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.playableCharacters[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().itemName + " - " + Engine.e.playableCharacters[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount + " Slots";
        }
        else
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = "Nothing Equipped.";
        }

        for (int i = 0; i < Engine.e.playableCharacters[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount; i++)
        {
            if (Engine.e.playableCharacters[3].equippedSkills[i] != null)
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skill = Engine.e.playableCharacters[3].equippedSkills[i];
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[3].equippedSkills[i].skillName;

            }
            else
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";
            }

            weaponSkillSlots[i].SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponSkillSlots[3].gameObject);
        }

        for (int i = 0; i < Engine.e.playableCharacters[3].GetComponent<Character>().skills.Length; i++)
        {
            if (Engine.e.playableCharacters[3].GetComponent<Character>().skills[i] != null)
            {
                skillSlots[i].GetComponent<SkillSlot>().skill = Engine.e.playableCharacters[3].GetComponent<Character>().skills[i];
                skillSlots[i].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[3].skills[i].skillName;

                skillSlots[i].SetActive(true);
            }
        }

        selectingWeapon = true;
        selectingSkill = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(weaponReference);

    }

    public void SetSolaceScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = true;
        blueScreen = false;

        weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        if (Engine.e.playableCharacters[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.playableCharacters[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().itemName + " - " + Engine.e.playableCharacters[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount + " Slots";
        }
        else
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = "Nothing Equipped.";
        }

        for (int i = 0; i < Engine.e.playableCharacters[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount; i++)
        {
            if (Engine.e.playableCharacters[4].equippedSkills[i] != null)
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skill = Engine.e.playableCharacters[4].equippedSkills[i];
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[4].equippedSkills[i].skillName;

            }
            else
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";
            }

            weaponSkillSlots[i].SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponSkillSlots[4].gameObject);
        }

        for (int i = 0; i < Engine.e.playableCharacters[4].GetComponent<Character>().skills.Length; i++)
        {
            if (Engine.e.playableCharacters[4].GetComponent<Character>().skills[i] != null)
            {
                skillSlots[i].GetComponent<SkillSlot>().skill = Engine.e.playableCharacters[4].GetComponent<Character>().skills[i];
                skillSlots[i].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[4].skills[i].skillName;

                skillSlots[i].SetActive(true);
            }
        }
        selectingWeapon = true;
        selectingSkill = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(weaponReference);
    }

    public void SetBlueScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = true;

        weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        if (Engine.e.playableCharacters[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.playableCharacters[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().itemName + " - " + Engine.e.playableCharacters[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount + " Slots";
        }
        else
        {
            weaponReference.GetComponentInChildren<TextMeshProUGUI>().text = "Nothing Equipped.";
        }

        for (int i = 0; i < Engine.e.playableCharacters[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().skillAmount; i++)
        {
            if (Engine.e.playableCharacters[5].equippedSkills[i] != null)
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skill = Engine.e.playableCharacters[5].equippedSkills[i];
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[5].equippedSkills[i].skillName;

            }
            else
            {
                weaponSkillSlots[i].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";
            }

            weaponSkillSlots[i].SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponSkillSlots[5].gameObject);
        }

        for (int i = 0; i < Engine.e.playableCharacters[5].GetComponent<Character>().skills.Length; i++)
        {
            if (Engine.e.playableCharacters[5].GetComponent<Character>().skills[i] != null)
            {
                skillSlots[i].GetComponent<SkillSlot>().skill = Engine.e.playableCharacters[5].GetComponent<Character>().skills[i];
                skillSlots[i].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[5].skills[i].skillName;

                skillSlots[i].SetActive(true);
            }
        }

        selectingWeapon = true;
        selectingSkill = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(weaponReference);

    }
    public void SelectSkill()
    {
        skillPointerIndex = 0;
        selectingSkill = true;
        removingSkill = false;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(skillSlots[skillPointerIndex].gameObject);
    }

    public void AugmentSkill(Skills _skill)
    {
        if (grieveScreen)
        {
            if (weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill != null)
            {
                weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill.equipping = false;
                weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill.StatChange(0);
            }

            // check if there is a skill in slot, and if so, remove and update stats
            if (skillSlots[skillPointerIndex].GetComponent<SkillSlot>().skill != null)  // Equipping Skill
            {
                _skill.equipping = true;
                Engine.e.playableCharacters[0].equippedSkills[weaponSkillSlotReference] = _skill;

                weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill = _skill;
                weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = Engine.e.playableCharacters[0].equippedSkills[weaponSkillSlotReference].skillName;

                skillSlots[skillPointerIndex].GetComponent<SkillSlot>().skill = null;
                skillSlots[skillPointerIndex].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";

                _skill.StatChange(0);

            }
            else
            {
                if (weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill != null)
                {
                    skillSlots[skillPointerIndex].GetComponent<SkillSlot>().skill = weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill;
                    skillSlots[skillPointerIndex].GetComponent<SkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill.skillName;

                    weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skill = null;
                    weaponSkillSlots[weaponSkillSlotReference].GetComponent<WeaponSkillSlot>().skillName.GetComponent<TextMeshProUGUI>().text = "-";

                    Engine.e.playableCharacters[0].equippedSkills[weaponSkillSlotReference] = null; // Removing Skill

                }
            }

            if (macScreen)
            {
                Engine.e.playableCharacters[1].equippedSkills[weaponSkillSlotReference] = _skill;
                _skill.StatChange(1);

            }
            if (fieldScreen)
            {
                Engine.e.playableCharacters[2].equippedSkills[weaponSkillSlotReference] = _skill;
                _skill.StatChange(2);

            }
            if (riggsScreen)
            {
                Engine.e.playableCharacters[3].equippedSkills[weaponSkillSlotReference] = _skill;
                _skill.StatChange(3);

            }
            if (solaceScreen)
            {
                Engine.e.playableCharacters[4].equippedSkills[weaponSkillSlotReference] = _skill;
                _skill.StatChange(4);

            }
            if (blueScreen)
            {
                Engine.e.playableCharacters[5].equippedSkills[weaponSkillSlotReference] = _skill;
                _skill.StatChange(5);

            }

            selectingSkill = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponSkillSlots[weaponPointerIndex].gameObject);
            //weaponSkillSlotReference = 0;
        }
    }

    void HandleWeaponSlots()
    {
        if (selectingWeapon)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponReference.gameObject);
            weaponPointerIndex = -1;
        }

        if (!selectingSkill && !selectingWeapon)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (weaponPointerIndex < weaponSkillSlots.Length)
                {
                    weaponPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(weaponSkillSlots[weaponPointerIndex].gameObject);

                    if (weaponPointerIndex > 5 && weaponPointerIndex < weaponSkillSlots.Length)
                    {
                        //Engine.e.partyInventoryReference.weaponRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (weaponPointerIndex > 0)
                {
                    weaponPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(weaponSkillSlots[weaponPointerIndex].gameObject);


                    if (skillPointerIndex >= 5 && skillPointerIndex > 0)
                    {
                        //Engine.e.partyInventoryReference.weaponRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }

        if (selectingSkill && !selectingWeapon)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (skillPointerIndex < skillSlots.Length)
                {
                    skillPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(skillSlots[skillPointerIndex].gameObject);

                    if (skillPointerIndex > 5 && skillPointerIndex < weaponSkillSlots.Length)
                    {
                        //Engine.e.partyInventoryReference.weaponRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (skillPointerIndex > 0)
                {
                    skillPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(skillSlots[skillPointerIndex].gameObject);


                    if (skillPointerIndex >= 5 && skillPointerIndex > 0)
                    {
                        //Engine.e.partyInventoryReference.weaponRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }
    }

    public void SetSelectWeaponBool(bool _selectingWeapon)
    {
        selectingWeapon = _selectingWeapon;
    }

    void PressDown()
    {
        pressDown = true;
        vertMove = 1;
    }
    void ReleaseDown()
    {
        pressDown = false;
        vertMove = 0;
    }
    void PressUp()
    {
        pressUp = true;
        vertMove = -1;
    }
    void ReleaseUp()
    {
        pressUp = false;
        vertMove = 0;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.S))
        {
            PressDown();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ReleaseDown();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PressUp();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ReleaseUp();
        }
        if (pressDown && !pressUp)
        {
            vertMove = 1;
        }
        if (!pressDown && pressUp)
        {
            vertMove = -1;
        }

        HandleWeaponSlots();

    }
}
