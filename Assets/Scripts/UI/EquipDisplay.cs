using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EquipDisplay : MonoBehaviour
{
    public GameObject[] charSelectionButtons;
    public bool grieveScreen, macScreen, fieldScreen, riggsScreen;
    public GameObject[] weaponLists, armorLists;
    public InventorySlot equippedWeapon, equippedChestArmor, equippedAccessory1, equippedAccessory2;
    public TextMeshProUGUI[] charAttackStatsReference, charDefenseStatsReference, charAttackComparisonStats, charDefenseComparisonStats;
    public TextMeshProUGUI[] charTMP;
    public GameObject[] charNewWeaponNotif;
    public bool[] charNewWeapon;
    public GameObject[] equipmentDisplays;
    public bool grieveWeaponInventorySet, macWeaponInventorySet, fieldWeaponInventorySet, riggsWeaponInventorySet = false;
    public bool chestArmorInventorySet, accessory1InventorySet, accessory2InventorySet = false;
    bool pressUp, pressDown, pressRelease = false;
    public int inventoryPointerIndex = 0, vertMove = 0;


    public void SetGrieveScreen()
    {
        grieveScreen = true;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = false;


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
        EventSystem.current.SetSelectedGameObject(equippedWeapon.gameObject);

    }
    public void SetMacScreen()
    {
        grieveScreen = false;
        macScreen = true;
        fieldScreen = false;
        riggsScreen = false;

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
        EventSystem.current.SetSelectedGameObject(equippedWeapon.gameObject);

    }
    public void SetFieldScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = true;
        riggsScreen = false;

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
        EventSystem.current.SetSelectedGameObject(equippedWeapon.gameObject);


    }
    public void SetRiggsScreen()
    {
        grieveScreen = false;
        macScreen = false;
        fieldScreen = false;
        riggsScreen = true;

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
        EventSystem.current.SetSelectedGameObject(equippedWeapon.gameObject);

    }

    public void ActivateWeaponList()
    {
        if (grieveScreen)
        {
            weaponLists[0].SetActive(true);
        }
        if (macScreen)
        {
            weaponLists[1].SetActive(true);
        }
        if (fieldScreen)
        {
            weaponLists[2].SetActive(true);
        }
        if (riggsScreen)
        {
            weaponLists[3].SetActive(true);
        }
        Engine.e.partyInventoryReference.OpenWeaponInventory();
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
    }

    public void ActivateArmorList(int armor)
    {
        armorLists[armor].SetActive(true);
    }
    public void DeactivateArmorList(int armor)
    {
        armorLists[armor].SetActive(false);
    }

    public void DisplayGrieveStats()
    {

        charAttackStatsReference[0].text = Engine.e.party[0].GetComponent<Character>().physicalDamage.ToString();
        charAttackStatsReference[1].text = Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[2].text = Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[0].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[0].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[0].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[0].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[0].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[0].GetComponent<Character>().shadowDefense + "%";

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().weapon.itemName;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[0].GetComponent<Character>().chestArmor.itemName;

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

        charAttackStatsReference[0].text = Engine.e.party[1].GetComponent<Character>().physicalDamage.ToString();
        charAttackStatsReference[1].text = Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[2].text = Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[1].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[1].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[1].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[1].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[1].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[1].GetComponent<Character>().shadowDefense + "%";

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[1].GetComponent<Character>().weapon.itemName;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[1].GetComponent<Character>().chestArmor.itemName;

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

        charAttackStatsReference[0].text = Engine.e.party[2].GetComponent<Character>().physicalDamage.ToString();
        charAttackStatsReference[1].text = Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[2].text = Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[2].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[2].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[2].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[2].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[2].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[2].GetComponent<Character>().shadowDefense + "%";

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[2].GetComponent<Character>().weapon.itemName;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[2].GetComponent<Character>().chestArmor.itemName;

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

        charAttackStatsReference[0].text = Engine.e.party[3].GetComponent<Character>().physicalDamage.ToString();
        charAttackStatsReference[1].text = Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus.ToString();
        charAttackStatsReference[2].text = Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus.ToString();
        charAttackStatsReference[3].text = Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus.ToString();
        charAttackStatsReference[4].text = Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus.ToString();
        charAttackStatsReference[5].text = Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus.ToString();

        charDefenseStatsReference[0].text = Engine.e.party[3].GetComponent<Character>().physicalDefense + "%";
        charDefenseStatsReference[1].text = Engine.e.party[3].GetComponent<Character>().fireDefense + "%";
        charDefenseStatsReference[2].text = Engine.e.party[3].GetComponent<Character>().iceDefense + "%";
        charDefenseStatsReference[3].text = Engine.e.party[3].GetComponent<Character>().lightningDefense + "%";
        charDefenseStatsReference[4].text = Engine.e.party[3].GetComponent<Character>().waterDefense + "%";
        charDefenseStatsReference[5].text = Engine.e.party[3].GetComponent<Character>().shadowDefense + "%";

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory1.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        equippedAccessory2.itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;

        equippedWeapon.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[3].GetComponent<Character>().weapon.itemName;
        equippedChestArmor.itemName.GetComponentInChildren<TMP_Text>().text = Engine.e.party[3].GetComponent<Character>().chestArmor.itemName;

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


            for (int i = 0; i < weaponLists.Length; i++)
            {
                if (Engine.e.party[i] != null)
                {
                    weaponLists[i].SetActive(false);
                }
            }
        }
    }

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

        // "Grieve Weapons Menu" Inventory
        if (grieveWeaponInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.grieveWeaponInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.grieveWeaponInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.grieveWeaponInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.grieveWeaponsRectTransform.offsetMax -= new Vector2(0, -30);
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
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.grieveWeaponInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.grieveWeaponsRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }

        if (macWeaponInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.macWeaponInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.macWeaponInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.macWeaponInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.macWeaponsRectTransform.offsetMax -= new Vector2(0, -30);
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
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.macWeaponInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.macWeaponsRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }

        if (fieldWeaponInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.fieldWeaponInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.fieldWeaponInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.fieldWeaponInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.fieldWeaponsRectTransform.offsetMax -= new Vector2(0, -30);
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
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.fieldWeaponInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.fieldWeaponsRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }

        if (riggsWeaponInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.partyInventoryReference.riggsWeaponInventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.riggsWeaponInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < Engine.e.partyInventoryReference.riggsWeaponInventorySlots.Length)
                    {
                        Engine.e.partyInventoryReference.riggsWeaponsRectTransform.offsetMax -= new Vector2(0, -30);
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
                    EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.riggsWeaponInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.riggsWeaponsRectTransform.offsetMax -= new Vector2(0, 30);
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

        if (grieveWeaponInventorySet || macWeaponInventorySet || fieldWeaponInventorySet || riggsWeaponInventorySet
        || chestArmorInventorySet || accessory1InventorySet || accessory2InventorySet)
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
