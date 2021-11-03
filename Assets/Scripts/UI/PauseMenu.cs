using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI textReference;
    public bool mainMenu;
    public bool atPauseMenu;
    public TextMeshProUGUI[] activePartyMembersText;
    public TextMeshProUGUI pauseMenuTextReference;
    public TextMeshProUGUI[] partyPauseMenuNamesTextReference;
    public TextMeshProUGUI[] partyPauseMenuHPStatsTextReference, partyPauseMenuMPStatsTextReference, partyPauseMenuENRStatsTextReference, itemMenuPartyStatsHPTextReference, partyPauseMenuAPStatsTextReference;
    public GameObject[] pauseMenuButtons;
    public GameObject grieveWeaponFirstButton, grieveWeaponSelection, macWeaponFirstButton, macWeaponSelection, fieldWeaponFirstButton, fieldWeaponSelection, riggsWeaponFirstButton, riggsWeaponSelection;
    public TextMeshProUGUI partyMoneyDisplay;
    public TextMeshProUGUI timeOfDayDisplay;
    public TextMeshProUGUI partyLocationDisplay;
    public GameObject pauseMenu, equipMenu, inventoryMenu, mainMenuScreen, dropsMenu, itemMenu, abilitiesMenu;
    public GameObject chestArmorSelection, chestArmorFirstButton;

    // Controller Support UI
    // Menu(s) First Choice
    public GameObject pauseFirstButton, equipFirstButton, mainMenuFirstButton, inventoryFirstButton, itemInventoryFirstButton, dropsInventoryFirstButton, abilitiesFirstButton, abilityScreenSkillsFirstButton;
    public GameObject arrangeIndex1Grieve,
    arrangeIndex2Grieve, arrangeIndex2Mac,
    arrangeIndex3Grieve, arrangeIndex3Mac, arrangeIndex3Field;
    public GameObject equipWeaponFirst, equipChestArmorFirst, equipChestArmorCharFirst;
    public bool shoppingArmor = false, shoppingWeapons = false, shoppingDrops = false, shoppingItems = false;
    public TextMeshProUGUI battleModeReference;



    void Start()
    {
        OpenMainMenu();
        isPaused = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 3"))
        {
            if (Engine.e.inBattle == false)
            {
                if (isPaused == false && !Engine.e.mainMenu.gameObject.activeSelf)
                {
                    Pause();

                    partyMoneyDisplay.text = "G: " + Engine.e.partyMoney;
                    timeOfDayDisplay.text = "Time: " + Mathf.Round(Engine.e.timeOfDay);
                    if (Engine.e.battleModeActive)
                    {
                        battleModeReference.text = string.Empty;
                        battleModeReference.text = "Battle Mode: Active";
                    }
                    else
                    {
                        battleModeReference.text = string.Empty;
                        battleModeReference.text = "Battle Mode: Wait";
                    }

                    DisplayGrievePartyText();
                    DisplayMacPartyText();
                    DisplayFieldPartyText();
                    DisplayRiggsPartyText();

                    Engine.e.DisplayGrieveInventoryStats();
                    Engine.e.DisplayMacInventoryStats();
                    Engine.e.DisplayFieldInventoryStats();
                    Engine.e.DisplayRiggsInventoryStats();

                    DisplayActiveParty1Text();
                    DisplayActiveParty2Text();
                    DisplayActiveParty3Text();

                    if (Engine.e.inRange)
                    {
                        Engine.e.interactionPopup.SetActive(false);
                    }
                }
                else
                {
                    Resume();

                    if (Engine.e.inRange)
                    {
                        Engine.e.interactionPopup.SetActive(true);
                    }
                }
            }
        }
    }

    public void DisplayGrievePartyText()
    {
        partyPauseMenuNamesTextReference[0].text = string.Empty;
        partyPauseMenuNamesTextReference[0].text += Engine.e.party[0].GetComponent<Character>().characterName
        + " Lvl. " + Engine.e.party[0].GetComponent<Character>().lvl;

        partyPauseMenuHPStatsTextReference[0].text = string.Empty;
        partyPauseMenuHPStatsTextReference[0].text += "HP: " + Engine.e.party[0].GetComponent<Character>().currentHealth + " / " + Engine.e.party[0].GetComponent<Character>().maxHealth;

        partyPauseMenuMPStatsTextReference[0].text = string.Empty;
        partyPauseMenuMPStatsTextReference[0].text += "MP: " + Engine.e.party[0].GetComponent<Character>().currentMana + " / " + Engine.e.party[0].GetComponent<Character>().maxMana;

        partyPauseMenuENRStatsTextReference[0].text = string.Empty;
        partyPauseMenuENRStatsTextReference[0].text += "ENR: " + Engine.e.party[0].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[0].GetComponent<Character>().maxEnergy;

        if (Engine.e.party[0].GetComponent<Grieve>().lvl >= 20)
        {
            partyPauseMenuAPStatsTextReference[0].text = string.Empty;
            partyPauseMenuAPStatsTextReference[0].text += Engine.e.party[0].GetComponent<Character>().availableSkillPoints.ToString();
        }
    }

    public void DisplayMacPartyText()
    {
        if (Engine.e.party[1] != null)
        {
            partyPauseMenuNamesTextReference[1].text = string.Empty;
            partyPauseMenuNamesTextReference[1].text += Engine.e.party[1].GetComponent<Character>().characterName
            + " Lvl. " + Engine.e.party[1].GetComponent<Character>().lvl;

            partyPauseMenuHPStatsTextReference[1].text = string.Empty;
            partyPauseMenuHPStatsTextReference[1].text += "HP: " + Engine.e.party[1].GetComponent<Character>().currentHealth + " / " + Engine.e.party[1].GetComponent<Character>().maxHealth;

            partyPauseMenuMPStatsTextReference[1].text = string.Empty;
            partyPauseMenuMPStatsTextReference[1].text += "MP: " + Engine.e.party[1].GetComponent<Character>().currentMana + " / " + Engine.e.party[1].GetComponent<Character>().maxMana;

            partyPauseMenuENRStatsTextReference[1].text = string.Empty;
            partyPauseMenuENRStatsTextReference[1].text += "ENR: " + Engine.e.party[1].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[1].GetComponent<Character>().maxEnergy;

            if (Engine.e.party[1].GetComponent<Mac>().lvl >= 20)
            {
                partyPauseMenuAPStatsTextReference[1].text = string.Empty;
                partyPauseMenuAPStatsTextReference[1].text += Engine.e.party[1].GetComponent<Character>().availableSkillPoints.ToString();
            }
        }
    }

    public void DisplayFieldPartyText()
    {
        if (Engine.e.party[2] != null)
        {
            partyPauseMenuNamesTextReference[2].text = string.Empty;
            partyPauseMenuNamesTextReference[2].text += Engine.e.party[2].GetComponent<Character>().characterName
            + " Lvl. " + Engine.e.party[2].GetComponent<Character>().lvl;

            partyPauseMenuHPStatsTextReference[2].text = string.Empty;
            partyPauseMenuHPStatsTextReference[2].text += "HP: " + Engine.e.party[2].GetComponent<Character>().currentHealth + " / " + Engine.e.party[2].GetComponent<Character>().maxHealth;

            partyPauseMenuMPStatsTextReference[2].text = string.Empty;
            partyPauseMenuMPStatsTextReference[2].text += "MP: " + Engine.e.party[2].GetComponent<Character>().currentMana + " / " + Engine.e.party[2].GetComponent<Character>().maxMana;

            partyPauseMenuENRStatsTextReference[2].text = string.Empty;
            partyPauseMenuENRStatsTextReference[2].text += "ENR: " + Engine.e.party[2].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[2].GetComponent<Character>().maxEnergy;

            if (Engine.e.party[2].GetComponent<Field>().lvl >= 20)
            {
                partyPauseMenuAPStatsTextReference[2].text = string.Empty;
                partyPauseMenuAPStatsTextReference[2].text += Engine.e.party[2].GetComponent<Character>().availableSkillPoints.ToString();
            }
        }
    }

    public void DisplayRiggsPartyText()
    {
        if (Engine.e.party[3] != null)
        {
            partyPauseMenuNamesTextReference[3].text = string.Empty;
            partyPauseMenuNamesTextReference[3].text += Engine.e.party[3].GetComponent<Character>().characterName
            + " Lvl. " + Engine.e.party[3].GetComponent<Character>().lvl;

            partyPauseMenuHPStatsTextReference[3].text = string.Empty;
            partyPauseMenuHPStatsTextReference[3].text += "HP: " + Engine.e.party[3].GetComponent<Character>().currentHealth + " / " + Engine.e.party[3].GetComponent<Character>().maxHealth;

            partyPauseMenuMPStatsTextReference[3].text = string.Empty;
            partyPauseMenuMPStatsTextReference[3].text += "MP: " + Engine.e.party[3].GetComponent<Character>().currentMana + " / " + Engine.e.party[3].GetComponent<Character>().maxMana;

            partyPauseMenuENRStatsTextReference[3].text = string.Empty;
            partyPauseMenuENRStatsTextReference[3].text += "ENR: " + Engine.e.party[3].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[3].GetComponent<Character>().maxEnergy;

            if (Engine.e.party[3].GetComponent<Riggs>().lvl >= 20)
            {
                partyPauseMenuAPStatsTextReference[3].text = string.Empty;
                partyPauseMenuAPStatsTextReference[3].text += Engine.e.party[3].GetComponent<Character>().availableSkillPoints.ToString();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        atPauseMenu = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        atPauseMenu = true;

        OpenPauseMenu();
    }

    public void DisplayActiveParty1Text()
    {
        activePartyMembersText[0].text = Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;
    }

    public void DisplayActiveParty2Text()
    {
        if (Engine.e.activeParty.activeParty[1] != null)
            activePartyMembersText[1].text = Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;
    }

    public void DisplayActiveParty3Text()
    {
        if (Engine.e.activeParty.activeParty[2] != null)
            activePartyMembersText[2].text = Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;
    }

    public void DisplayActivePartyText()
    {
        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
                activePartyMembersText[i].text = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().characterName;
        }
    }

    public void ClearActivePartyText()
    {
        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
            activePartyMembersText[i].text = string.Empty;
    }

    public void OpenPauseMenu()
    {
        if (!Engine.e.selling)
        {
            pauseMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            atPauseMenu = true;
            Engine.e.partyInventoryReference.inventoryPointerIndex = 0;
            //Engine.e.partyInventoryReference.inventoryScreenSet = false;

            Engine.e.partyInventoryReference.partyInventoryRectTransform.offsetMax -= new Vector2(0, 0);

            Engine.e.partyInventoryReference.grieveWeaponsRectTransform.offsetMax -= new Vector2(0, 0);
            Engine.e.partyInventoryReference.macWeaponsRectTransform.offsetMax -= new Vector2(0, 0);
            Engine.e.partyInventoryReference.fieldWeaponsRectTransform.offsetMax -= new Vector2(0, 0);
            Engine.e.partyInventoryReference.riggsWeaponsRectTransform.offsetMax -= new Vector2(0, 0);

            Engine.e.partyInventoryReference.chestArmorRectTransform.offsetMax -= new Vector2(0, 0);

        }
    }

    public void OpenEquipMenu()
    {
        equipMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equipFirstButton);

        if (Engine.e.party[1] != null)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charSelectionButtons[1].SetActive(true);
        }
        if (Engine.e.party[2] != null)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charSelectionButtons[2].SetActive(true);
        }
        if (Engine.e.party[3] != null)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charSelectionButtons[3].SetActive(true);
        }

        atPauseMenu = false;
    }


    public void OpenAbilitiesMenu()
    {
        abilitiesMenu.SetActive(true);
        Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().SetSkills();
        Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().SetDrops();
        Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().DisplayAP();

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(abilitiesFirstButton);

        atPauseMenu = false;
    }

    public void OpenSkillsMenu()
    {

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(abilityScreenSkillsFirstButton);

        atPauseMenu = false;
    }

    public void OpenGrieveWeaponSelection()
    {
        grieveWeaponSelection.SetActive(true);
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charNewWeapon[0] = false;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charNewWeaponNotif[0].SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        // if (Engine.e.grieveWeapons.Count == 0)
        // {
        EventSystem.current.SetSelectedGameObject(grieveWeaponFirstButton);
        // }
        // else
        // {
        //     EventSystem.current.SetSelectedGameObject(Engine.e.grieveWeapons[0].GetComponent<GrieveWeapons>().inventoryButtonLogic);
        //}

        atPauseMenu = false;
    }

    public void OpenMacWeaponSelection()
    {
        macWeaponSelection.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(macWeaponFirstButton);


        atPauseMenu = false;
    }

    public void OpenFieldWeaponSelection()
    {
        fieldWeaponSelection.SetActive(true);


        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(fieldWeaponFirstButton);


        atPauseMenu = false;
    }

    public void OpenRiggsWeaponSelection()
    {
        riggsWeaponSelection.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(riggsWeaponFirstButton);


        atPauseMenu = false;
    }


    public void OpenMainMenu()
    {
        mainMenuScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        atPauseMenu = false;
    }
    public void ArrangeButton1()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(arrangeIndex1Grieve);

    }
    public void ArrangeButton2()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (Engine.e.activeParty.activeParty[0].GetComponent<Grieve>())
        {
            EventSystem.current.SetSelectedGameObject(arrangeIndex2Mac);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(arrangeIndex2Grieve);
        }
    }

    public void ArrangeButton3()
    {
        bool grieveInActive = false;
        bool macInActive = false;

        EventSystem.current.SetSelectedGameObject(null);
        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
            {
                if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName == "Grieve")
                {
                    grieveInActive = true;
                }
                if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().characterName == "Mac")
                {
                    macInActive = true;
                }

                if (grieveInActive)
                {
                    if (macInActive)
                    {
                        EventSystem.current.SetSelectedGameObject(arrangeIndex3Field);

                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(arrangeIndex3Mac);
                    }
                }
                else
                {

                    EventSystem.current.SetSelectedGameObject(arrangeIndex3Grieve);
                }
            }
        }
    }

    public void ChangeBattleMode()
    {
        if (Engine.e.battleModeActive)
        {
            Engine.e.battleModeActive = false;
            battleModeReference.text = string.Empty;
            battleModeReference.text = "Battle Mode: Wait";
        }
        else
        {
            Engine.e.battleModeActive = true;
            battleModeReference.text = string.Empty;
            battleModeReference.text = "Battle Mode: Active";
        }
    }

    public void EquipMenuWeaponFirst()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equipWeaponFirst);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}