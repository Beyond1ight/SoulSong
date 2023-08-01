using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using AutoLetterbox;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public bool atPauseMenu;
    public TextMeshProUGUI[] activePartyMembersText;
    public TextMeshProUGUI[] partyPauseMenuNamesTextReference;
    public TextMeshProUGUI[] partyPauseMenuHPStatsTextReference, partyPauseMenuMPStatsTextReference, partyPauseMenuENRStatsTextReference, partyPauseMenuAPStatsTextReference;
    public GameObject grieveWeaponFirstButton, grieveWeaponSelection, macWeaponFirstButton, macWeaponSelection, fieldWeaponFirstButton, fieldWeaponSelection, riggsWeaponFirstButton, riggsWeaponSelection,
    solaceWeaponFirstButton, solaceWeaponSelection, blueWeaponFirstButton, blueWeaponSelection;
    public TextMeshProUGUI partyMoneyDisplay;
    public TextMeshProUGUI timeOfDayDisplay;
    public TextMeshProUGUI partyLocationDisplay;
    public GameObject pauseMenu, equipMenu, mainMenuScreen, gridMenu, augmentMenu, adventureLogMenu, fileMenu, statusMenu;

    // Controller Support UI
    // Menu(s) First Choice
    public GameObject[] mainMenuButtons;
    public GameObject pauseFirstButton, equipFirstButton;
    public GameObject arrangeIndex1Grieve,
    arrangeIndex2Grieve, arrangeIndex2Mac,
    arrangeIndex3Grieve, arrangeIndex3Mac, arrangeIndex3Field;
    public GameObject[] gridCharSelect, augmentCharSelect;
    public GameObject equipWeaponFirst, equipChestArmorCharFirst, augmentMenuFirstButton, optionsMenuFirstButton;
    public bool shoppingArmor = false, shoppingWeapons = false, shoppingDrops = false, shoppingItems = false;
    public TextMeshProUGUI battleModeReference;
    public GameUI uiReference;
    bool globalLightOnBeforePause;

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
                    DisplaySolacePartyText();
                    DisplayBluePartyText();

                    Engine.e.DisplayGrieveInventoryStats();
                    Engine.e.DisplayMacInventoryStats();
                    Engine.e.DisplayFieldInventoryStats();
                    Engine.e.DisplayRiggsInventoryStats();
                    Engine.e.DisplaySolaceInventoryStats();
                    Engine.e.DisplayBlueInventoryStats();

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

    public void DisplaySolacePartyText()
    {
        if (Engine.e.party[4] != null)
        {
            partyPauseMenuNamesTextReference[4].text = string.Empty;
            partyPauseMenuNamesTextReference[4].text += Engine.e.party[4].GetComponent<Character>().characterName
            + " Lvl. " + Engine.e.party[4].GetComponent<Character>().lvl;

            partyPauseMenuHPStatsTextReference[4].text = string.Empty;
            partyPauseMenuHPStatsTextReference[4].text += "HP: " + Engine.e.party[4].GetComponent<Character>().currentHealth + " / " + Engine.e.party[4].GetComponent<Character>().maxHealth;

            partyPauseMenuMPStatsTextReference[4].text = string.Empty;
            partyPauseMenuMPStatsTextReference[4].text += "MP: " + Engine.e.party[4].GetComponent<Character>().currentMana + " / " + Engine.e.party[4].GetComponent<Character>().maxMana;

            partyPauseMenuENRStatsTextReference[4].text = string.Empty;
            partyPauseMenuENRStatsTextReference[4].text += "ENR: " + Engine.e.party[4].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[4].GetComponent<Character>().maxEnergy;

            if (Engine.e.party[4].GetComponent<Solace>().lvl >= 20)
            {
                partyPauseMenuAPStatsTextReference[4].text = string.Empty;
                partyPauseMenuAPStatsTextReference[4].text += Engine.e.party[4].GetComponent<Character>().availableSkillPoints.ToString();
            }
        }
    }

    public void DisplayBluePartyText()
    {
        if (Engine.e.party[5] != null)
        {
            partyPauseMenuNamesTextReference[5].text = string.Empty;
            partyPauseMenuNamesTextReference[5].text += Engine.e.party[5].GetComponent<Character>().characterName
            + " Lvl. " + Engine.e.party[5].GetComponent<Character>().lvl;

            partyPauseMenuHPStatsTextReference[5].text = string.Empty;
            partyPauseMenuHPStatsTextReference[5].text += "HP: " + Engine.e.party[5].GetComponent<Character>().currentHealth + " / " + Engine.e.party[5].GetComponent<Character>().maxHealth;

            partyPauseMenuMPStatsTextReference[5].text = string.Empty;
            partyPauseMenuMPStatsTextReference[5].text += "MP: " + Engine.e.party[5].GetComponent<Character>().currentMana + " / " + Engine.e.party[5].GetComponent<Character>().maxMana;

            partyPauseMenuENRStatsTextReference[5].text = string.Empty;
            partyPauseMenuENRStatsTextReference[5].text += "ENR: " + Engine.e.party[5].GetComponent<Character>().currentEnergy + " / " + Engine.e.party[5].GetComponent<Character>().maxEnergy;

            if (Engine.e.party[5].GetComponent<Blue>().lvl >= 20)
            {
                partyPauseMenuAPStatsTextReference[5].text = string.Empty;
                partyPauseMenuAPStatsTextReference[5].text += Engine.e.party[5].GetComponent<Character>().availableSkillPoints.ToString();
            }
        }
    }

    void Resume()
    {
        if (globalLightOnBeforePause)
        {
            Engine.e.GetComponent<Light2D>().enabled = true;
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        atPauseMenu = false;
    }

    void Pause()
    {
        globalLightOnBeforePause = Engine.e.indoorLighting;
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
            //Engine.e.partyInventoryReference.inventoryPointerIndex = 0;
            //Engine.e.partyInventoryReference.inventoryScreenSet = false;

            //Engine.e.partyInventoryReference.partyInventoryRectTransform.offsetMax -= new Vector2(0, 0);

            //Engine.e.partyInventoryReference.grieveWeaponsRectTransform.offsetMax -= new Vector2(0, 0);
            // Engine.e.partyInventoryReference.macWeaponsRectTransform.offsetMax -= new Vector2(0, 0);
            // Engine.e.partyInventoryReference.fieldWeaponsRectTransform.offsetMax -= new Vector2(0, 0);
            // Engine.e.partyInventoryReference.riggsWeaponsRectTransform.offsetMax -= new Vector2(0, 0);

            // Engine.e.partyInventoryReference.chestArmorRectTransform.offsetMax -= new Vector2(0, 0);

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
        if (Engine.e.party[4] != null)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charSelectionButtons[4].SetActive(true);
        }
        if (Engine.e.party[5] != null)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().charSelectionButtons[5].SetActive(true);
        }

        atPauseMenu = false;
    }

    public void OpenAugmentMenu()
    {
        augmentMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(augmentMenuFirstButton);

        atPauseMenu = false;

    }

    public void OpenFileMenuSaving()
    {
        fileMenu.SetActive(true);
        pauseMenu.SetActive(false);

        Engine.e.fileMenuReference.OpenFileMenuSaving();

    }

    public void OpenFileMenuLoading()
    {
        fileMenu.SetActive(true);
        pauseMenu.SetActive(false);

        Engine.e.fileMenuReference.OpenFileMenuLoading();
    }

    public void OpenAdventureLogMenu()
    {
        adventureLogMenu.SetActive(true);

        Engine.e.adventureLogReference.OpenAdventureLogMenu();
    }

    public void OpenStatusMenu()
    {
        statusMenu.SetActive(true);

        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                Engine.e.statusMenuReference.charSelectionButtons[i].SetActive(true);
            }
        }

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.statusMenuReference.charSelectionButtons[0]);
    }

    public void OpenAugmentMenuCharSelect()
    {
        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                augmentCharSelect[i].SetActive(true);
            }
        }

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(augmentCharSelect[0].gameObject);
    }

    public void OpenAugmentMenuGrieve()
    {

        for (int i = 0; i < augmentCharSelect.Length; i++)
        {
            augmentCharSelect[i].SetActive(false);
        }

        //Engine.e.augmentMenuReference.SetSelectWeaponBool(true);
        Engine.e.augmentMenuReference.SetGrieveScreen();
        augmentMenu.SetActive(true);

    }

    public void OpenAugmentMenuMac()
    {
        for (int i = 0; i < augmentCharSelect.Length; i++)
        {
            augmentCharSelect[i].SetActive(false);
        }

        Engine.e.augmentMenuReference.SetMacScreen();
        augmentMenu.SetActive(true);
    }

    public void OpenAugmentMenuField()
    {
        for (int i = 0; i < augmentCharSelect.Length; i++)
        {
            augmentCharSelect[i].SetActive(false);
        }

        Engine.e.augmentMenuReference.SetFieldScreen();
        augmentMenu.SetActive(true);
    }

    public void OpenAugmentMenuRiggs()
    {
        for (int i = 0; i < augmentCharSelect.Length; i++)
        {
            augmentCharSelect[i].SetActive(false);
        }

        Engine.e.augmentMenuReference.SetRiggsScreen();
        augmentMenu.SetActive(true);
    }

    public void OpenAugmentenuSolace()
    {
        for (int i = 0; i < augmentCharSelect.Length; i++)
        {
            augmentCharSelect[i].SetActive(false);
        }

        Engine.e.augmentMenuReference.SetSolaceScreen();
        augmentMenu.SetActive(true);
    }

    public void OpenAugmentenuBlue()
    {
        for (int i = 0; i < augmentCharSelect.Length; i++)
        {
            augmentCharSelect[i].SetActive(false);
        }

        Engine.e.augmentMenuReference.SetBlueScreen();
        augmentMenu.SetActive(true);
    }

    public void OpenGridMenuCharSelect()
    {
        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                gridCharSelect[i].SetActive(true);
            }
        }

        /*for (int i = 0; i < Engine.e.gridReference.dropsButtons.Length; i++)
        {
            if (Engine.e.gridReference.dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop != null)
            {
                if (Engine.e.gridReference.dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop.isKnown)
                {
                    if (Engine.e.gridReference.dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text != Engine.e.gridReference.dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop.dropName)
                    {
                        Engine.e.gridReference.dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.gridReference.dropsButtons[i].GetComponent<PauseMenuDropsUIHolder>().drop.dropName;
                    }
                }
            }
        }

        for (int i = 0; i < Engine.e.gridReference.skillButtons.Length; i++)
        {
            if (Engine.e.gridReference.skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill != null)
            {
                if (Engine.e.gridReference.skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill.isKnown)
                {
                    if (Engine.e.gridReference.skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text != Engine.e.gridReference.skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill.skillName)
                    {
                        Engine.e.gridReference.skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.gridReference.skillButtons[i].GetComponent<PauseMenuSkillsUIHolder>().skill.skillName;
                    }
                }
            }
        }*/

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(gridCharSelect[0].gameObject);


    }


    public void OpenGridMenuGrieve()
    {
        Engine.e.GetComponent<Light2D>().enabled = false;

        for (int i = 0; i < gridCharSelect.Length; i++)
        {
            gridCharSelect[i].SetActive(false);
        }

        uiReference.PlayTransition();

        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 6000f;

        gridMenu.SetActive(true);
        Engine.e.gridReference.SetGrieveScreen();
        //Engine.e.abilityScreenReference.DisplayCharSelection();
        Engine.e.canvasReference.SetActive(false);
        Engine.e.gridReference.gridDisplayed = true;
        //Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().SetSkills();
        // Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().SetDrops();

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.gridReference.cursor);
        Time.timeScale = 1f;
        atPauseMenu = false;
    }

    public void OpenGridMenuMac()
    {
        Engine.e.GetComponent<Light2D>().enabled = false;

        for (int i = 0; i < gridCharSelect.Length; i++)
        {
            gridCharSelect[i].SetActive(false);
        }

        uiReference.PlayTransition();

        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 6000f;

        Engine.e.gridReference.SetMacScreen();

        gridMenu.SetActive(true);
        Engine.e.canvasReference.SetActive(false);
        Engine.e.gridReference.gridDisplayed = true;

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.gridReference.cursor);
        Time.timeScale = 1f;
        atPauseMenu = false;
    }

    public void OpenGridMenuField()
    {
        Engine.e.GetComponent<Light2D>().enabled = false;

        for (int i = 0; i < gridCharSelect.Length; i++)
        {
            gridCharSelect[i].SetActive(false);
        }
        uiReference.PlayTransition();

        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 6000f;

        Engine.e.gridReference.SetFieldScreen();

        gridMenu.SetActive(true);
        Engine.e.canvasReference.SetActive(false);
        Engine.e.gridReference.gridDisplayed = true;

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.gridReference.cursor);
        Time.timeScale = 1f;
        atPauseMenu = false;
    }

    public void OpenGridMenuRiggs()
    {
        Engine.e.GetComponent<Light2D>().enabled = false;

        for (int i = 0; i < gridCharSelect.Length; i++)
        {
            gridCharSelect[i].SetActive(false);
        }

        uiReference.PlayTransition();

        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 6000f;

        Engine.e.gridReference.SetRiggsScreen();

        gridMenu.SetActive(true);
        Engine.e.canvasReference.SetActive(false);
        Engine.e.gridReference.gridDisplayed = true;

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.gridReference.cursor);
        Time.timeScale = 1f;
        atPauseMenu = false;
    }

    public void OpenGridMenuSolace()
    {
        Engine.e.GetComponent<Light2D>().enabled = false;

        for (int i = 0; i < gridCharSelect.Length; i++)
        {
            gridCharSelect[i].SetActive(false);
        }

        Engine.e.gridReference.SetSolaceScreen();
        uiReference.PlayTransition();

        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 6000f;

        gridMenu.SetActive(true);
        Engine.e.canvasReference.SetActive(false);
        Engine.e.gridReference.gridDisplayed = true;

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.gridReference.cursor);
        Time.timeScale = 1f;
        atPauseMenu = false;
    }
    public void OpenGridMenuBlue()
    {
        Engine.e.GetComponent<Light2D>().enabled = false;

        for (int i = 0; i < gridCharSelect.Length; i++)
        {
            gridCharSelect[i].SetActive(false);
        }

        Engine.e.gridReference.SetBlueScreen();
        uiReference.PlayTransition();

        Engine.e.gridReference.gridPerspective.m_Lens.OrthographicSize = 6000f;

        gridMenu.SetActive(true);
        Engine.e.canvasReference.SetActive(false);
        Engine.e.gridReference.gridDisplayed = true;

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Engine.e.gridReference.cursor);
        Time.timeScale = 1f;
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
    public void OpenSolaceWeaponSelection()
    {
        solaceWeaponSelection.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(solaceWeaponFirstButton);


        atPauseMenu = false;
    }
    public void OpenBlueWeaponSelection()
    {
        blueWeaponSelection.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(blueWeaponFirstButton);


        atPauseMenu = false;
    }

    public void OpenOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);


        atPauseMenu = false;
    }
    public void OpenMainMenu()
    {
        mainMenuScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        if (Engine.e.saveExists)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuButtons[1]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(mainMenuButtons[0]);

        }
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

    public void SwitchCamRatioSixteenByNine()
    {
        Vector2 sixteenByNine = new Vector2(16, 9);

        Engine.e.cameraRatioReference.GetComponent<ForceCameraRatio>().ratio = sixteenByNine;
    }
    public void SwitchCamRatioTwentyOneByNine()
    {
        Vector2 twentyOneByNine = new Vector2(21, 9);

        Engine.e.cameraRatioReference.GetComponent<ForceCameraRatio>().ratio = twentyOneByNine;

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

    public void ClearAugmentsReference()
    {
        Engine.e.augmentMenuReference.ClearAugments();
    }

}