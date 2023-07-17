using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EquipDisplay : MonoBehaviour
{
    public GameObject[] charSelectionButtons;
    public bool grieveScreen, macScreen, fieldScreen, riggsScreen, solaceScreen, blueScreen;
    public GameObject[] weaponLists, armorLists;
    public InventorySlot equippedWeaponRight, equippedWeaponLeft, equippedChestArmor, equippedAccessory1, equippedAccessory2;
    public TextMeshProUGUI[] charAttackStatsReference, charDefenseStatsReference, charAttackComparisonStats, charDefenseComparisonStats;
    public TextMeshProUGUI[] charTMP;
    public GameObject[] charNewWeaponNotif;
    public bool[] charNewWeapon;
    public GameObject[] equipmentDisplays;
    public bool weaponRightInventorySet, weaponLeftInventorySet, chestArmorInventorySet, legArmorInventorySet, accessory1InventorySet, accessory2InventorySet, removing = false;
    bool pressUp, pressDown, pressRelease = false;
    public int inventoryPointerIndex = 0, vertMove = 0;


    public void SetGrieveScreen()
    {
        grieveScreen = true;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;


        if (Engine.e.party[1] != null)
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
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);

    }
    public void SetMacScreen()
    {
        grieveScreen = false;
        macScreen = true;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;

        if (Engine.e.party[0] != null)
        {
            charTMP[0].color = Color.gray;
        }

        if (Engine.e.party[2] != null)
        {
            charTMP[2].color = Color.gray;
        }
        if (Engine.e.party[3] != null)
        {
            charTMP[3].color = Color.gray;

        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);

    }
    public void SetFieldScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = true;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = false;

        if (Engine.e.party[0] != null)
        {
            charTMP[0].color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charTMP[1].color = Color.gray;

        }
        if (Engine.e.party[3] != null)
        {
            charTMP[3].color = Color.gray;
        }


        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);


    }
    public void SetRiggsScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = true;
        solaceScreen = false;
        blueScreen = false;

        if (Engine.e.party[0] != null)
        {
            charTMP[0].color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charTMP[1].color = Color.gray;

        }
        if (Engine.e.party[2] != null)
        {
            charTMP[2].color = Color.gray;

        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);

    }

    public void SetSolaceScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = true;
        blueScreen = false;

        if (Engine.e.party[0] != null)
        {
            charTMP[0].color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charTMP[1].color = Color.gray;

        }
        if (Engine.e.party[2] != null)
        {
            charTMP[2].color = Color.gray;

        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);

    }

    public void SetBlueScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;
        solaceScreen = false;
        blueScreen = true;

        if (Engine.e.party[0] != null)
        {
            charTMP[0].color = Color.gray;
        }

        if (Engine.e.party[1] != null)
        {
            charTMP[1].color = Color.gray;

        }
        if (Engine.e.party[2] != null)
        {
            charTMP[2].color = Color.gray;

        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);

    }

    public void CloseWeaponSelection()
    {
        weaponRightInventorySet = false;
        weaponLeftInventorySet = false;

        EventSystem.current.SetSelectedGameObject(null);

        weaponLists[0].SetActive(false);

        if (grieveScreen)
        {
            SetGrieveScreen();
        }
        if (macScreen)
        {
            SetMacScreen();
        }
        if (fieldScreen)
        {
            SetFieldScreen();
        }
        if (riggsScreen)
        {
            SetRiggsScreen();
        }
        if (solaceScreen)
        {
            SetSolaceScreen();
        }
        if (blueScreen)
        {
            SetBlueScreen();
        }
    }
    public void CloseChestArmorSelection()
    {

        EventSystem.current.SetSelectedGameObject(null);

        armorLists[0].SetActive(false);

        if (grieveScreen)
        {
            SetGrieveScreen();
        }
        if (macScreen)
        {
            SetMacScreen();
        }
        if (fieldScreen)
        {
            SetFieldScreen();
        }
        if (riggsScreen)
        {
            SetRiggsScreen();
        }
        if (solaceScreen)
        {
            SetSolaceScreen();
        }
        if (blueScreen)
        {
            SetBlueScreen();
        }
    }
    public void CloseAccessorySelection()
    {
        accessory1InventorySet = false;
        accessory2InventorySet = false;

        EventSystem.current.SetSelectedGameObject(null);

        armorLists[1].SetActive(false);

        if (grieveScreen)
        {
            SetGrieveScreen();
        }
        if (macScreen)
        {
            SetMacScreen();
        }
        if (fieldScreen)
        {
            SetFieldScreen();
        }
        if (riggsScreen)
        {
            SetRiggsScreen();
        }
        if (solaceScreen)
        {
            SetSolaceScreen();
        }
        if (blueScreen)
        {
            SetBlueScreen();
        }
    }

    public void ActivateArmorList(int armor)
    {
        armorLists[armor].SetActive(true);
    }
    public void DeactivateArmorList(int armor)
    {
        armorLists[armor].SetActive(false);
    }

    public void SetRemovingBool()
    {
        removing = !removing;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(equippedWeaponRight.gameObject);
    }

    public void DisplayGrieveStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[0].GetComponent<Character>().strength.ToString();
        charAttackStatsReference[1].text = Engine.e.party[0].GetComponent<Character>().intelligence.ToString();
        charAttackStatsReference[2].text = Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[6].text = Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[0].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[0].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[0].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[0].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[0].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[0].GetComponent<Character>().shadowDefense + "%";

        equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        //equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        if (Engine.e.party[0].GetComponent<Character>().weaponRight != null)
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().weaponRight.itemName;
        }
        else
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        //if (Engine.e.party[0].GetComponent<Character>().weaponLeft != null)
        //{
        //    equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().weaponLeft.itemName;
        //}
        //else
        //{
        //    equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        //}

        if (Engine.e.party[0].GetComponent<Character>().chestArmor != null)
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().chestArmor.itemName;
        }
        else
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[0].GetComponent<Character>().accessory1 != null)
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().accessory1.itemName;
        }
        else
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[0].GetComponent<Character>().accessory2 != null)
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().accessory2.itemName;
        }
        else
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        ClearNewWeaponNotif();

        if (charNewWeapon[0] == true)
        {
            charNewWeaponNotif[0].SetActive(true);
        }
    }
    public void DisplayMacStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[1].GetComponent<Character>().strength.ToString();
        charAttackStatsReference[1].text = Engine.e.party[1].GetComponent<Character>().intelligence.ToString();
        charAttackStatsReference[2].text = Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[6].text = Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[1].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[1].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[1].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[1].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[1].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[1].GetComponent<Character>().shadowDefense + "%";

        equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        if (Engine.e.party[1].GetComponent<Character>().weaponRight != null)
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[1].GetComponent<Character>().weaponRight.itemName;
        }
        else
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[1].GetComponent<Character>().chestArmor != null)
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[1].GetComponent<Character>().chestArmor.itemName;
        }
        else
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[1].GetComponent<Character>().accessory1 != null)
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[1].GetComponent<Character>().accessory1.itemName;
        }
        else
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[1].GetComponent<Character>().accessory2 != null)
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[1].GetComponent<Character>().accessory2.itemName;
        }
        else
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }
        ClearNewWeaponNotif();
        if (charNewWeapon[1] == true)
        {
            charNewWeaponNotif[1].SetActive(true);
        }
    }
    public void DisplayFieldStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[2].GetComponent<Character>().strength.ToString();
        charAttackStatsReference[1].text = Engine.e.party[2].GetComponent<Character>().intelligence.ToString();
        charAttackStatsReference[2].text = Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[6].text = Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[2].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[2].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[2].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[2].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[2].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[2].GetComponent<Character>().shadowDefense + "%";

        equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        if (Engine.e.party[2].GetComponent<Character>().weaponRight != null)
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[2].GetComponent<Character>().weaponRight.itemName;
        }
        else
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[2].GetComponent<Character>().chestArmor != null)
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[2].GetComponent<Character>().chestArmor.itemName;
        }
        else
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[2].GetComponent<Character>().accessory1 != null)
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[2].GetComponent<Character>().accessory1.itemName;
        }
        else
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[2].GetComponent<Character>().accessory2 != null)
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[2].GetComponent<Character>().accessory2.itemName;
        }
        else
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        ClearNewWeaponNotif();
        if (charNewWeapon[2] == true)
        {
            charNewWeaponNotif[2].SetActive(true);
        }
    }
    public void DisplayRiggsStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[3].GetComponent<Character>().strength.ToString();
        charAttackStatsReference[1].text = Engine.e.party[3].GetComponent<Character>().intelligence.ToString();
        charAttackStatsReference[2].text = Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[6].text = Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[3].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[3].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[3].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[3].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[3].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[3].GetComponent<Character>().shadowDefense + "%";

        equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        if (Engine.e.party[3].GetComponent<Character>().weaponRight != null)
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[3].GetComponent<Character>().weaponRight.itemName;
        }
        else
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[3].GetComponent<Character>().chestArmor != null)
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[3].GetComponent<Character>().chestArmor.itemName;
        }
        else
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[3].GetComponent<Character>().accessory1 != null)
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[3].GetComponent<Character>().accessory1.itemName;
        }
        else
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[3].GetComponent<Character>().accessory2 != null)
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[3].GetComponent<Character>().accessory2.itemName;
        }
        else
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        ClearNewWeaponNotif();
        if (charNewWeapon[3] == true)
        {
            charNewWeaponNotif[3].SetActive(true);
        }
    }

    public void DisplaySolaceStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[4].GetComponent<Character>().strength.ToString();
        charAttackStatsReference[1].text = Engine.e.party[4].GetComponent<Character>().intelligence.ToString();
        charAttackStatsReference[2].text = Engine.e.party[4].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[4].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[4].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[4].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[6].text = Engine.e.party[4].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[4].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[4].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[4].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[4].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[4].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[4].GetComponent<Character>().shadowDefense + "%";

        equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        if (Engine.e.party[4].GetComponent<Character>().weaponRight != null)
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[4].GetComponent<Character>().weaponRight.itemName;
        }
        else
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[4].GetComponent<Character>().chestArmor != null)
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[4].GetComponent<Character>().chestArmor.itemName;
        }
        else
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[4].GetComponent<Character>().accessory1 != null)
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[4].GetComponent<Character>().accessory1.itemName;
        }
        else
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[4].GetComponent<Character>().accessory2 != null)
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[4].GetComponent<Character>().accessory2.itemName;
        }
        else
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        ClearNewWeaponNotif();
        if (charNewWeapon[4] == true)
        {
            charNewWeaponNotif[4].SetActive(true);
        }
    }

    public void DisplayBlueStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[5].GetComponent<Character>().strength.ToString();
        charAttackStatsReference[1].text = Engine.e.party[5].GetComponent<Character>().intelligence.ToString();
        charAttackStatsReference[2].text = Engine.e.party[5].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[5].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[5].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[5].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[6].text = Engine.e.party[5].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[5].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[5].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[5].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[5].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[5].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[5].GetComponent<Character>().shadowDefense + "%";

        equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedWeaponLeft.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        if (Engine.e.party[5].GetComponent<Character>().weaponRight != null)
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[5].GetComponent<Character>().weaponRight.itemName;
        }
        else
        {
            equippedWeaponRight.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[5].GetComponent<Character>().chestArmor != null)
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[5].GetComponent<Character>().chestArmor.itemName;
        }
        else
        {
            equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[5].GetComponent<Character>().accessory1 != null)
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[5].GetComponent<Character>().accessory1.itemName;
        }
        else
        {
            equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        if (Engine.e.party[5].GetComponent<Character>().accessory2 != null)
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[5].GetComponent<Character>().accessory2.itemName;
        }
        else
        {
            equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = "Nothing.";
        }

        ClearNewWeaponNotif();
        if (charNewWeapon[5] == true)
        {
            charNewWeaponNotif[5].SetActive(true);
        }
    }
    public void ClearAccessoryBool()
    {
        Engine.e.equipMenuReference.accessory1InventorySet = false;
        Engine.e.equipMenuReference.accessory2InventorySet = false;
    }

    public void ClearBool()
    {
        for (int i = 0; i < charTMP.Length; i++)
        {
            charTMP[i].color = Color.white;
        }
        if (grieveScreen)
        {
            grieveScreen = !grieveScreen;
        }
        if (macScreen)
        {
            macScreen = !macScreen;
        }
        if (fieldScreen)
        {
            fieldScreen = !fieldScreen;
        }
        if (riggsScreen)
        {
            riggsScreen = !riggsScreen;
        }
        if (solaceScreen)
        {
            solaceScreen = !solaceScreen;
        }
        if (blueScreen)
        {
            blueScreen = !blueScreen;
        }
        if (removing)
        {
            removing = false;
        }
    }
    public void ClearScreen()
    {
        if (grieveScreen == true || macScreen == true || fieldScreen == true || riggsScreen == true || solaceScreen == true || blueScreen == true)
        {
            grieveScreen = false;
            macScreen = false;
            fieldScreen = false;
            riggsScreen = false;
            solaceScreen = false;
            blueScreen = false;

            for (int i = 0; i < charSelectionButtons.Length; i++)
            {
                if (Engine.e.party[i] != null)
                {
                    charSelectionButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
            }


            for (int i = 0; i < weaponLists.Length; i++)
            {
                if (Engine.e.party[i] != null)
                {
                    weaponLists[i].SetActive(false);
                }
            }
        }
    }
    public void SetHelpTextRemoveWeaponRight()
    {

        if (removing)
        {
            if (grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().weaponRight != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().weaponRight != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().weaponRight != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().weaponRight != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (solaceScreen)
            {
                if (Engine.e.party[4].GetComponent<Character>().weaponRight != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().strength - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().intelligence - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (blueScreen)
            {
                if (Engine.e.party[5].GetComponent<Character>().weaponRight != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().strength - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().intelligence - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }
        }
    }

    /*public void SetHelpTextRemoveWeaponLeft()
    {

        if (removing)
        {
            if (grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().weaponLeft != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().weaponLeft != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().weaponLeft != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }

            if (riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().weaponLeft != null)
                {
                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus).ToString();
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus).ToString();
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack).ToString();
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack).ToString();
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack).ToString();
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack).ToString();
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack).ToString();
                }
            }
        }
    }*/

    public void SetHelpTextRemoveChestArmor()
    {
        if (removing)
        {
            if (grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().chestArmor != null)
                {
                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense) + "%";
                }
            }

            if (macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().chestArmor != null)
                {
                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense) + "%";
                }
            }

            if (fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().chestArmor != null)
                {
                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense) + "%";
                }
            }

            if (riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().chestArmor != null)
                {
                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense) + "%";
                }
            }

            if (solaceScreen)
            {
                if (Engine.e.party[4].GetComponent<Character>().chestArmor != null)
                {
                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().physicalDefense - Engine.e.party[4].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().fireDefense - Engine.e.party[4].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().iceDefense - Engine.e.party[4].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().lightningDefense - Engine.e.party[4].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().waterDefense - Engine.e.party[4].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().shadowDefense - Engine.e.party[4].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense) + "%";
                }
            }

            if (blueScreen)
            {
                if (Engine.e.party[5].GetComponent<Character>().chestArmor != null)
                {
                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().physicalDefense - Engine.e.party[5].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().fireDefense - Engine.e.party[5].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().iceDefense - Engine.e.party[5].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().lightningDefense - Engine.e.party[5].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().waterDefense - Engine.e.party[5].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().shadowDefense - Engine.e.party[5].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense) + "%";
                }
            }
        }
    }
    public void SetHelpTextAccessory1()
    {

        if (removing)
        {
            if (grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().accessory1 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense) + "%";

                }
            }

            if (macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().accessory1 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().accessory1 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().accessory1 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (solaceScreen)
            {
                if (Engine.e.party[4].GetComponent<Character>().accessory1 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().strength - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().intelligence - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().physicalDefense - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().fireDefense - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().iceDefense - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().lightningDefense - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().waterDefense - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().shadowDefense - Engine.e.party[4].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (blueScreen)
            {
                if (Engine.e.party[5].GetComponent<Character>().accessory1 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().strength - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().intelligence - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().physicalDefense - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().fireDefense - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().iceDefense - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().lightningDefense - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().waterDefense - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().shadowDefense - Engine.e.party[5].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }
        }
    }
    public void SetHelpTextAccessory2()
    {
        if (removing)
        {
            if (grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().accessory2 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().accessory2 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().accessory2 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().accessory2 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (solaceScreen)
            {
                if (Engine.e.party[4].GetComponent<Character>().accessory2 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().strength - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().intelligence - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().physicalDefense - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().fireDefense - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().iceDefense - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().lightningDefense - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().waterDefense - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[4].GetComponent<Character>().shadowDefense - Engine.e.party[4].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }

            if (blueScreen)
            {
                if (Engine.e.party[5].GetComponent<Character>().accessory2 != null)
                {

                    charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().strength - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus);
                    charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().intelligence - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus);
                    charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().firePhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack);
                    charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().icePhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack);
                    charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().lightningPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack);
                    charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().waterPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack);
                    charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().shadowPhysicalAttackBonus - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack);

                    charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().physicalDefense - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor) + "%";
                    charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().fireDefense - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense) + "%";
                    charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().iceDefense - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense) + "%";
                    charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().lightningDefense - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense) + "%";
                    charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().waterDefense - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense) + "%";
                    charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[5].GetComponent<Character>().shadowDefense - Engine.e.party[5].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense) + "%";
                }
            }
        }
    }

    public void ClearRemovingHelpText()
    {
        if (removing)
        {
            charAttackComparisonStats[0].text = string.Empty;
            charAttackComparisonStats[1].text = string.Empty;
            charAttackComparisonStats[2].text = string.Empty;
            charAttackComparisonStats[3].text = string.Empty;
            charAttackComparisonStats[4].text = string.Empty;
            charAttackComparisonStats[5].text = string.Empty;
            charAttackComparisonStats[6].text = string.Empty;

            charDefenseComparisonStats[0].text = string.Empty;
            charDefenseComparisonStats[1].text = string.Empty;
            charDefenseComparisonStats[2].text = string.Empty;
            charDefenseComparisonStats[3].text = string.Empty;
            charDefenseComparisonStats[4].text = string.Empty;
            charDefenseComparisonStats[5].text = string.Empty;
        }
    }
    public void SetWeaponRightScreen()
    {
        weaponRightInventorySet = true;
    }
    /*public void SetWeaponLeftScreen()
    {
        weaponLeftInventorySet = true;
    }*/

    public void SetAccessory1Screen()
    {
        accessory1InventorySet = true;
    }

    public void SetAccessory2Screen()
    {
        accessory2InventorySet = true;
    }

    // Handles Various Menu Navigation
    void HandleInventory()
    {

        if (weaponRightInventorySet || weaponLeftInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.weaponInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.weaponInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.weaponInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.weaponRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (inventoryPointerIndex > 0)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.weaponInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.weaponRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }

        if (chestArmorInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.chestArmorInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.chestArmorInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.chestArmorInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.chestArmorRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (inventoryPointerIndex > 0)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.chestArmorInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.chestArmorRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }

            }
        }

        if (accessory1InventorySet || accessory2InventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.accessoryInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.accessoryInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.accessoryInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.accessoryRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (inventoryPointerIndex > 0)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.accessoryInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.accessoryRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }

            }
        }
    }
    public void ClearNewWeaponNotif()
    {
        for (int i = 0; i < charNewWeaponNotif.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                charNewWeaponNotif[i].SetActive(false);
            }
        }
    }

    public void ChestArmorReturnButton()
    {

        EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(chestArmorButton);
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

        if (weaponRightInventorySet || weaponLeftInventorySet || chestArmorInventorySet || legArmorInventorySet || accessory1InventorySet || accessory2InventorySet)
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

            HandleInventory();
        }
    }
}
