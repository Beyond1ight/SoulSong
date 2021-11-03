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
    public InventorySlot grieveEquippedWeapon, grieveEquippedChestArmor, macEquippedWeapon, macEquippedChestArmor,
    fieldEquippedWeapon, fieldEquippedChestArmor, riggsEquippedWeapon, riggsEquippedChestArmor;
    public TextMeshProUGUI charAttackStatsReference, charDefenseStatsReference;
    public TextMeshProUGUI[] charTMP;
    public GameObject[] charNewWeaponNotif;
    public bool[] charNewWeapon;
    public GameObject[] equipmentDisplays;
    public bool grieveWeaponInventorySet, macWeaponInventorySet, fieldWeaponInventorySet, riggsWeaponInventorySet = false;
    public bool chestArmorInventorySet = false;
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
        EventSystem.current.SetSelectedGameObject(grieveEquippedWeapon.gameObject);

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
        EventSystem.current.SetSelectedGameObject(macEquippedWeapon.gameObject);

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
        EventSystem.current.SetSelectedGameObject(fieldEquippedWeapon.gameObject);


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
        EventSystem.current.SetSelectedGameObject(riggsEquippedWeapon.gameObject);

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
        equipmentDisplays[0].SetActive(true);
        equipmentDisplays[1].SetActive(false);
        equipmentDisplays[2].SetActive(false);
        equipmentDisplays[3].SetActive(false);

        charAttackStatsReference.text = string.Empty;
        charAttackStatsReference.text += "\nPhysical Attack: " + Engine.e.party[0].GetComponent<Character>().physicalDamage
        + "\n\n Fire Damage : " + Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus
        + "\n Ice Damage : " + Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus
        + "\n Lightning Damage : " + Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus
        + "\n Water Damage : " + Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus
        + "\n Shadow Damage : " + Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus;

        charDefenseStatsReference.text = string.Empty;
        charDefenseStatsReference.text += "\nPhysical Defense: " + Engine.e.party[0].GetComponent<Character>().physicalDefense + "%"
        + "\n\n Fire Defense : " + Engine.e.party[0].GetComponent<Character>().fireDefense + "%"
        + "\n Ice Defense : " + Engine.e.party[0].GetComponent<Character>().iceDefense + "%"
        + "\n Lightning Defense : " + Engine.e.party[0].GetComponent<Character>().lightningDefense + "%"
        + "\n Water Defense : " + Engine.e.party[0].GetComponent<Character>().waterDefense + "%"
        + "\n Shadow Defense : " + Engine.e.party[0].GetComponent<Character>().shadowDefense + "%";

        grieveEquippedWeapon.itemName = grieveEquippedWeapon.GetComponent<InventorySlot>().itemName;
        grieveEquippedChestArmor.itemName = grieveEquippedChestArmor.GetComponent<InventorySlot>().itemName;


        ClearNewWeaponNotif();

        if (charNewWeapon[0] == true)
        {
            charNewWeaponNotif[0].SetActive(true);
        }
    }
    public void DisplayMacStats()
    {
        equipmentDisplays[0].SetActive(false);
        equipmentDisplays[1].SetActive(true);
        equipmentDisplays[2].SetActive(false);
        equipmentDisplays[3].SetActive(false);

        charAttackStatsReference.text = string.Empty;
        charAttackStatsReference.text += "\nPhysical Attack: " + Engine.e.party[1].GetComponent<Character>().physicalDamage
        + "\n\n Fire Damage : " + Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus
        + "\n Ice Damage : " + Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus
        + "\n Lightning Damage : " + Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus
        + "\n Water Damage : " + Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus
        + "\n Shadow Damage : " + Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus;

        charDefenseStatsReference.text = string.Empty;
        charDefenseStatsReference.text += "\nPhysical Defense: " + Engine.e.party[1].GetComponent<Character>().physicalDefense + "%"
        + "\n\n Fire Defense : " + Engine.e.party[1].GetComponent<Character>().fireDefense + "%"
        + "\n Ice Defense : " + Engine.e.party[1].GetComponent<Character>().iceDefense + "%"
        + "\n Lightning Defense : " + Engine.e.party[1].GetComponent<Character>().lightningDefense + "%"
        + "\n Water Defense : " + Engine.e.party[1].GetComponent<Character>().waterDefense + "%"
        + "\n Shadow Defense : " + Engine.e.party[1].GetComponent<Character>().shadowDefense + "%";

        macEquippedWeapon.itemName = macEquippedWeapon.GetComponent<InventorySlot>().itemName;
        macEquippedChestArmor.itemName = macEquippedChestArmor.GetComponent<InventorySlot>().itemName;

        ClearNewWeaponNotif();
        if (charNewWeapon[1] == true)
        {
            charNewWeaponNotif[1].SetActive(true);
        }
    }
    public void DisplayFieldStats()
    {
        equipmentDisplays[0].SetActive(false);
        equipmentDisplays[1].SetActive(false);
        equipmentDisplays[2].SetActive(true);
        equipmentDisplays[3].SetActive(false);

        charAttackStatsReference.text = string.Empty;
        charAttackStatsReference.text += "\nPhysical Attack: " + Engine.e.party[2].GetComponent<Character>().physicalDamage
        + "\n\n Fire Damage : " + Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus
        + "\n Ice Damage : " + Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus
        + "\n Lightning Damage : " + Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus
        + "\n Water Damage : " + Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus
        + "\n Shadow Damage : " + Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus;

        charDefenseStatsReference.text = string.Empty;
        charDefenseStatsReference.text += "\nPhysical Defense: " + Engine.e.party[2].GetComponent<Character>().physicalDefense + "%"
        + "\n\n Fire Defense : " + Engine.e.party[2].GetComponent<Character>().fireDefense + "%"
        + "\n Ice Defense : " + Engine.e.party[2].GetComponent<Character>().iceDefense + "%"
        + "\n Lightning Defense : " + Engine.e.party[2].GetComponent<Character>().lightningDefense + "%"
        + "\n Water Defense : " + Engine.e.party[2].GetComponent<Character>().waterDefense + "%"
        + "\n Shadow Defense : " + Engine.e.party[2].GetComponent<Character>().shadowDefense + "%";

        fieldEquippedWeapon.itemName = fieldEquippedWeapon.GetComponent<InventorySlot>().itemName;
        fieldEquippedChestArmor.itemName = fieldEquippedChestArmor.GetComponent<InventorySlot>().itemName;

        ClearNewWeaponNotif();
        if (charNewWeapon[2] == true)
        {
            charNewWeaponNotif[2].SetActive(true);
        }
    }
    public void DisplayRiggsStats()
    {
        equipmentDisplays[0].SetActive(false);
        equipmentDisplays[1].SetActive(false);
        equipmentDisplays[2].SetActive(false);
        equipmentDisplays[3].SetActive(true);

        charAttackStatsReference.text = string.Empty;
        charAttackStatsReference.text += "\nPhysical Attack: " + Engine.e.party[3].GetComponent<Character>().physicalDamage
        + "\n\n Fire Damage : " + Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus
        + "\n Ice Damage : " + Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus
        + "\n Lightning Damage : " + Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus
        + "\n Water Damage : " + Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus
        + "\n Shadow Damage : " + Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus;

        charDefenseStatsReference.text = string.Empty;
        charDefenseStatsReference.text += "\nPhysical Defense: " + Engine.e.party[3].GetComponent<Character>().physicalDefense + "%"
        + "\n\n Fire Defense : " + Engine.e.party[3].GetComponent<Character>().fireDefense + "%"
        + "\n Ice Defense : " + Engine.e.party[3].GetComponent<Character>().iceDefense + "%"
        + "\n Lightning Defense : " + Engine.e.party[3].GetComponent<Character>().lightningDefense + "%"
        + "\n Water Defense : " + Engine.e.party[3].GetComponent<Character>().waterDefense + "%"
        + "\n Shadow Defense : " + Engine.e.party[3].GetComponent<Character>().shadowDefense + "%";

        riggsEquippedWeapon.itemName = riggsEquippedWeapon.GetComponent<InventorySlot>().itemName;
        riggsEquippedChestArmor.itemName = riggsEquippedChestArmor.GetComponent<InventorySlot>().itemName;

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
            charTMP[i].color = Color.black;
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
        || chestArmorInventorySet)
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
