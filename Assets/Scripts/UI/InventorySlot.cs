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

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
            {
                float physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                float fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                float iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                float lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                float waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                float shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
            {
                float physicalComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                float fireComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                float iceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                float lightningComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                float waterComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                float shadowComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
            {
                float physicalComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                float fireComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                float iceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                float lightningComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                float waterComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                float shadowComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
            {
                float physicalComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                float fireComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                float iceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                float lightningComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                float waterComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                float shadowComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
            }
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

            if (item != null)
            {
                float physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().physicalAttack - item.GetComponent<GrieveWeapons>().physicalAttack;
                float fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().fireAttack - item.GetComponent<GrieveWeapons>().fireAttack;
                float iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().iceAttack - item.GetComponent<GrieveWeapons>().iceAttack;
                float lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().lightningAttack - item.GetComponent<GrieveWeapons>().lightningAttack;
                float waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().waterAttack - item.GetComponent<GrieveWeapons>().waterAttack;
                float shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().shadowAttack - item.GetComponent<GrieveWeapons>().shadowAttack;

                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDamage - physicalComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet = true;
            }

            if (item != null)
            {
                float physicalComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().physicalAttack - item.GetComponent<MacWeapons>().physicalAttack;
                float fireComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().fireAttack - item.GetComponent<MacWeapons>().fireAttack;
                float iceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().iceAttack - item.GetComponent<MacWeapons>().iceAttack;
                float lightningComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().lightningAttack - item.GetComponent<MacWeapons>().lightningAttack;
                float waterComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().waterAttack - item.GetComponent<MacWeapons>().waterAttack;
                float shadowComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().shadowAttack - item.GetComponent<MacWeapons>().shadowAttack;

                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDamage - physicalComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet = true;
            }

            if (item != null)
            {
                float physicalComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().physicalAttack - item.GetComponent<FieldWeapons>().physicalAttack;
                float fireComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().fireAttack - item.GetComponent<FieldWeapons>().fireAttack;
                float iceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().iceAttack - item.GetComponent<FieldWeapons>().iceAttack;
                float lightningComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().lightningAttack - item.GetComponent<FieldWeapons>().lightningAttack;
                float waterComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().waterAttack - item.GetComponent<FieldWeapons>().waterAttack;
                float shadowComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().shadowAttack - item.GetComponent<FieldWeapons>().shadowAttack;

                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDamage - physicalComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
        {
            if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsWeaponInventorySet)
            {
                Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsWeaponInventorySet = true;
            }

            if (item != null)
            {
                float physicalComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().physicalAttack - item.GetComponent<RiggsWeapons>().physicalAttack;
                float fireComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().fireAttack - item.GetComponent<RiggsWeapons>().fireAttack;
                float iceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().iceAttack - item.GetComponent<RiggsWeapons>().iceAttack;
                float lightningComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().lightningAttack - item.GetComponent<RiggsWeapons>().lightningAttack;
                float waterComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().waterAttack - item.GetComponent<RiggsWeapons>().waterAttack;
                float shadowComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().shadowAttack - item.GetComponent<RiggsWeapons>().shadowAttack;

                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDamage - physicalComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

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
        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = string.Empty;
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
        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = string.Empty;
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

                        ClearHelpTextWeapons();

                    }

                    if (item.itemType == "Armor")
                    {
                        item.GetComponent<ChestArmor>().DisplayArmorEquipCharacterTargets();

                        ClearHelpTextChestArmor();

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

