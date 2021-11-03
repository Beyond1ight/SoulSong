using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public GameObject itemName, itemType, itemCount;
    public int index;
    public GameObject scrollReference;

    public void AddItem(Item _item)
    {
        item = _item;
        itemName.GetComponent<TMP_Text>().text = _item.itemName;
        itemType.GetComponent<TMP_Text>().text = _item.itemType;
        _item.numberHeld++;
        UpdateHeld();
    }
    public void AddItemSorting(Item _item)
    {
        item = _item;
        itemName.GetComponent<TMP_Text>().text = _item.itemName;
        itemType.GetComponent<TMP_Text>().text = _item.itemType;
        UpdateHeld();
    }
    public void UpdateHeld()
    {
        if (item.stackable)
        {
            itemCount.GetComponent<TMP_Text>().text = ": " + item.numberHeld.ToString();
        }
        else
        {
            itemCount.GetComponent<TMP_Text>().text = string.Empty;
        }
    }

    public void SetHelpTextInventory()
    {
        if (!Engine.e.partyInventoryReference.inventoryScreenSet)
        {
            Engine.e.partyInventoryReference.inventoryScreenSet = true;
            //Engine.e.partyInventoryReference.inventoryPointerIndex = index;
        }

        if (item != null)
        {
            Engine.e.helpText.text = item.itemDescription;
        }
        else
        {
            Engine.e.helpText.text = string.Empty;
        }
    }

    public void SetHelpTextChestArmor()
    {
        if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = true;
        }

        if (item != null)
        {
            Engine.e.helpText.text = item.itemDescription;
        }
        else
        {
            Engine.e.helpText.text = string.Empty;
        }
    }

    public void SetHelpTextWeapons()
    {
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveWeaponInventorySet = true;
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet = true;
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet = true;
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsWeaponInventorySet = true;
            }
        }

        if (item != null)
        {
            Engine.e.helpText.text = item.itemDescription;
        }
        else
        {
            Engine.e.helpText.text = string.Empty;
        }
    }

    public void ClearHelpTextInventory()
    {
        if (Engine.e.partyInventoryReference.inventoryScreenSet)
        {
            Engine.e.partyInventoryReference.inventoryScreenSet = false;
        }
        Engine.e.helpText.text = string.Empty;

    }

    public void ClearHelpTextChestArmor()
    {
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = false;
        }
        Engine.e.helpText.text = string.Empty;

    }

    public void ClearHelpTextWeapons()
    {
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveWeaponInventorySet = false;
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet = false;
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet = false;
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsWeaponInventorySet = false;
            }
        }


        Engine.e.helpText.text = string.Empty;

    }

    public void ClearSlot()
    {

        item = null;
        itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        itemType.GetComponent<TMP_Text>().text = string.Empty;
        itemCount.GetComponentInChildren<TMP_Text>().text = string.Empty;

    }



    public void OnClickEvent()
    {
        if (item != null)
        {
            if (!Engine.e.selling)
            {
                if (Engine.e.partyInventoryReference.inventoryScreenSet)
                {
                    if (item.itemType == "Item" || item.itemType == "Drop")
                    {
                        item.UseItemCheck();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Engine.e.partyInventoryReference.indexReference = index;

                    if (item.itemType == "Weapon")
                    {

                        if (item.GetComponent<GrieveWeapons>())
                        {
                            item.GetComponent<GrieveWeapons>().EquipGrieveWeapon();
                        }
                        if (item.GetComponent<MacWeapons>())
                        {
                            item.GetComponent<MacWeapons>().EquipMacWeapon();
                        }
                        if (item.GetComponent<FieldWeapons>())
                        {
                            item.GetComponent<FieldWeapons>().EquipFieldWeapon();
                        }
                        if (item.GetComponent<RiggsWeapons>())
                        {
                            item.GetComponent<RiggsWeapons>().EquipRiggsWeapon();
                        }
                    }

                    if (item.itemType == "Armor")
                    {
                        item.GetComponent<ChestArmor>().DisplayArmorEquipCharacterTargets();
                    }
                }
            }
            else
            {
                Engine.e.currentMerchant.SellItemCheck(item);
            }
        }
        else
        {
            return;
        }
    }
}

