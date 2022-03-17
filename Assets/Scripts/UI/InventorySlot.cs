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
        float physicalComparisonCalculation = 0f;
        float fireComparisonCalculation = 0f;
        float iceComparisonCalculation = 0f;
        float lightningComparisonCalculation = 0f;
        float waterComparisonCalculation = 0f;
        float shadowComparisonCalculation = 0f;

        if (!Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = true;
        }

        if (item != null)
        {
            Engine.e.helpText.text = item.itemDescription;

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().chestArmor != null)
                {
                    physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
                }
                else
                {
                    physicalComparisonCalculation = item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense + physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense + fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense + iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense + lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense + waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense + shadowComparisonCalculation) + "%";
                }
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().chestArmor != null)
                {
                    physicalComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = Engine.e.party[1].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
                }
                else
                {
                    physicalComparisonCalculation = item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense + physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense + fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense + iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense + lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense + waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense + shadowComparisonCalculation) + "%";
                }
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().chestArmor != null)
                {
                    physicalComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = Engine.e.party[2].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
                }
                else
                {
                    physicalComparisonCalculation = item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense + physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense + fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense + iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense + lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense + waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense + shadowComparisonCalculation) + "%";
                }
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().chestArmor != null)
                {
                    physicalComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().physicalArmor - item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().fireDefense - item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().iceDefense - item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().lightningDefense - item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().waterDefense - item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = Engine.e.party[3].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().shadowDefense - item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - shadowComparisonCalculation) + "%";
                }
                else
                {
                    physicalComparisonCalculation = item.GetComponent<ChestArmor>().physicalArmor;
                    fireComparisonCalculation = item.GetComponent<ChestArmor>().fireDefense;
                    iceComparisonCalculation = item.GetComponent<ChestArmor>().iceDefense;
                    lightningComparisonCalculation = item.GetComponent<ChestArmor>().lightningDefense;
                    waterComparisonCalculation = item.GetComponent<ChestArmor>().waterDefense;
                    shadowComparisonCalculation = item.GetComponent<ChestArmor>().shadowDefense;

                    Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense + physicalComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense + fireComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense + iceComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense + lightningComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense + waterComparisonCalculation) + "%";
                    Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense + shadowComparisonCalculation) + "%";
                }
            }
        }
        else
        {
            Engine.e.helpText.text = string.Empty;
        }
    }

    public void SetHelpTextAccessory()
    {
        float physicalAttComparisonCalculation = 0;
        float intelligenceComparisonCalculation = 0;
        float fireAttComparisonCalculation = 0;
        float iceAttComparisonCalculation = 0;
        float lightningAttComparisonCalculation = 0;
        float waterAttComparisonCalculation = 0;
        float shadowAttComparisonCalculation = 0;

        float physicalDefComparisonCalculation = 0;
        float fireDefComparisonCalculation = 0;
        float iceDefComparisonCalculation = 0;
        float lightningDefComparisonCalculation = 0;
        float waterDefComparisonCalculation = 0;
        float shadowDefComparisonCalculation = 0;

        if (item != null)
        {
            Engine.e.helpText.text = item.itemDescription;

            if (Engine.e.equipMenuReference.accessory1InventorySet)
            {
                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
                {
                    if (Engine.e.party[0].GetComponent<Character>().accessory1 != null)
                    {
                        physicalAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                        fireAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                        iceAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                        lightningAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                        waterAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                        shadowAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                        physicalDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                        fireDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                        iceDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                        lightningDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                        waterDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                        shadowDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();
                    }
                    else
                    {
                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                    }

                }

                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
                {
                    if (Engine.e.party[1].GetComponent<Character>().accessory1 != null)
                    {
                        physicalAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                        fireAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                        iceAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                        lightningAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                        waterAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                        shadowAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                        physicalDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                        fireDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                        iceDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                        lightningDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                        waterDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                        shadowDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();

                    }
                    else
                    {
                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                    }
                }

                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
                {
                    if (Engine.e.party[2].GetComponent<Character>().accessory1 != null)
                    {
                        physicalAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                        fireAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                        iceAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                        lightningAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                        waterAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                        shadowAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                        physicalDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                        fireDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                        iceDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                        lightningDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                        waterDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                        shadowDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();
                    }
                    else
                    {
                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                    }
                }


                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
                {
                    if (Engine.e.party[3].GetComponent<Character>().accessory1 != null)
                    {
                        physicalAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                        fireAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                        iceAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                        lightningAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                        waterAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                        shadowAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                        physicalDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                        fireDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                        iceDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                        lightningDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                        waterDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                        shadowDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory1.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();

                    }
                    else
                    {
                        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                    }
                }
            }
            else
            {
                if (Engine.e.equipMenuReference.accessory2InventorySet)
                {
                    if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
                    {
                        if (Engine.e.party[0].GetComponent<Character>().accessory2 != null)
                        {
                            physicalAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                            fireAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                            iceAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                            lightningAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                            waterAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                            shadowAttComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                            physicalDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                            fireDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                            iceDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                            lightningDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                            waterDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                            shadowDefComparisonCalculation = Engine.e.party[0].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();
                        }
                        else
                        {
                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                        }
                    }

                    if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
                    {
                        if (Engine.e.party[1].GetComponent<Character>().accessory2 != null)
                        {
                            physicalAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                            fireAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                            iceAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                            lightningAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                            waterAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                            shadowAttComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                            physicalDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                            fireDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                            iceDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                            lightningDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                            waterDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                            shadowDefComparisonCalculation = Engine.e.party[1].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();
                        }
                        else
                        {
                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                        }
                    }

                    if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
                    {
                        if (Engine.e.party[2].GetComponent<Character>().accessory2 != null)
                        {
                            physicalAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                            fireAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                            iceAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                            lightningAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                            waterAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                            shadowAttComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                            physicalDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                            fireDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                            iceDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                            lightningDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                            waterDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                            shadowDefComparisonCalculation = Engine.e.party[2].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();
                        }
                        else
                        {
                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                        }
                    }

                    if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
                    {
                        if (Engine.e.party[3].GetComponent<Character>().accessory2 != null)
                        {
                            physicalAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().strengthBonus - item.GetComponent<Accessory>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().intelligenceBonus - item.GetComponent<Accessory>().intelligenceBonus;
                            fireAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireAttack - item.GetComponent<Accessory>().fireAttack;
                            iceAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceAttack - item.GetComponent<Accessory>().iceAttack;
                            lightningAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningAttack - item.GetComponent<Accessory>().lightningAttack;
                            waterAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterAttack - item.GetComponent<Accessory>().waterAttack;
                            shadowAttComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowAttack - item.GetComponent<Accessory>().shadowAttack;

                            physicalDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().physicalArmor - item.GetComponent<Accessory>().physicalArmor;
                            fireDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().fireDefense - item.GetComponent<Accessory>().fireDefense;
                            iceDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().iceDefense - item.GetComponent<Accessory>().iceDefense;
                            lightningDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().lightningDefense - item.GetComponent<Accessory>().lightningDefense;
                            waterDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().waterDefense - item.GetComponent<Accessory>().waterDefense;
                            shadowDefComparisonCalculation = Engine.e.party[3].GetComponent<Character>().accessory2.GetComponent<Accessory>().shadowDefense - item.GetComponent<Accessory>().shadowDefense;

                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense - physicalDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense - fireDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense - iceDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense - lightningDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense - waterDefComparisonCalculation) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense - shadowDefComparisonCalculation) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - physicalAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - fireAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - iceAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - lightningAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - waterAttComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - shadowAttComparisonCalculation).ToString();
                        }
                        else
                        {
                            Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().physicalDefense + item.GetComponent<Accessory>().physicalArmor) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().fireDefense + item.GetComponent<Accessory>().fireDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().iceDefense + item.GetComponent<Accessory>().iceDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningDefense + item.GetComponent<Accessory>().lightningDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterDefense + item.GetComponent<Accessory>().waterDefense) + "%";
                            Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowDefense + item.GetComponent<Accessory>().shadowDefense) + "%";

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength + item.GetComponent<Accessory>().strengthBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence + item.GetComponent<Accessory>().intelligenceBonus).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus + item.GetComponent<Accessory>().fireAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus + item.GetComponent<Accessory>().iceAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus + item.GetComponent<Accessory>().lightningAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus + item.GetComponent<Accessory>().waterAttack).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus + item.GetComponent<Accessory>().shadowAttack).ToString();
                        }
                    }
                }
            }
        }
        else
        {
            Engine.e.helpText.text = string.Empty;
        }
    }


    public void SetHelpTextWeapon()
    {
        float physicalComparisonCalculation = 0f;
        float intelligenceComparisonCalculation = 0f;
        float fireComparisonCalculation = 0f;
        float iceComparisonCalculation = 0f;
        float lightningComparisonCalculation = 0f;
        float waterComparisonCalculation = 0f;
        float shadowComparisonCalculation = 0f;

        if (Engine.e.equipMenuReference.weaponRightInventorySet)
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
            {
                if (item != null)
                {
                    if (Engine.e.party[0].GetComponent<Character>().weaponRight != null)
                    {
                        physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                        if (item.GetComponent<Weapon>().twoHand && Engine.e.party[0].GetComponent<Character>().weaponLeft != null)
                        {
                            physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                            fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                            iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                            lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                            waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                            shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;
                        }

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                    }
                    else
                    {
                        physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                    }
                }
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
            {
                if (item != null)
                {
                    if (Engine.e.party[1].GetComponent<Character>().weaponRight != null)
                    {
                        physicalComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                    }
                    else
                    {
                        physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                    }
                }
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
            {
                if (item != null)
                {
                    if (Engine.e.party[2].GetComponent<Character>().weaponRight != null)
                    {
                        physicalComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                    }
                    else
                    {
                        physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                    }
                }
            }

            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
            {
                if (item != null)
                {
                    if (Engine.e.party[3].GetComponent<Character>().weaponRight != null)
                    {
                        physicalComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                    }
                    else
                    {
                        physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                        intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                        fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                        iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                        lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                        waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                        shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                    }
                }
            }
        }
        else
        {
            if (Engine.e.equipMenuReference.weaponLeftInventorySet)
            {
                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
                {
                    if (item != null)
                    {
                        if (Engine.e.party[0].GetComponent<Character>().weaponLeft != null)
                        {
                            physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                            fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                            iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                            lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                            waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                            shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                        }
                        else
                        {
                            if (Engine.e.party[0].GetComponent<Character>().weaponRight != null && Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().twoHand)
                            {
                                physicalComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = Engine.e.party[0].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

                            }
                            else
                            {

                                physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().intelligence + intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[0].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                            }
                        }
                    }
                }

                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
                {
                    if (item != null)
                    {
                        if (Engine.e.party[1].GetComponent<Character>().weaponLeft != null)
                        {
                            physicalComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                            fireComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                            iceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                            lightningComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                            waterComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                            shadowComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                        }
                        else
                        {
                            if (Engine.e.party[1].GetComponent<Character>().weaponRight != null && Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().twoHand)
                            {
                                physicalComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = Engine.e.party[1].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

                            }
                            else
                            {
                                physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[1].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                            }
                        }
                    }
                }

                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
                {
                    if (item != null)
                    {
                        if (Engine.e.party[2].GetComponent<Character>().weaponLeft != null)
                        {
                            physicalComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                            fireComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                            iceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                            lightningComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                            waterComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                            shadowComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                        }
                        else
                        {
                            if (Engine.e.party[2].GetComponent<Character>().weaponRight != null && Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().twoHand)
                            {
                                physicalComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = Engine.e.party[2].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

                            }
                            else
                            {
                                physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[2].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                            }
                        }
                    }
                }

                if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
                {
                    if (item != null)
                    {
                        if (Engine.e.party[3].GetComponent<Character>().weaponLeft != null)
                        {
                            physicalComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                            intelligenceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                            fireComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                            iceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                            lightningComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                            waterComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                            shadowComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponLeft.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                            Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                            Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();
                        }
                        else
                        {
                            if (Engine.e.party[3].GetComponent<Character>().weaponRight != null && Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().twoHand)
                            {
                                physicalComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().strengthBonus - item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().intelligenceBonus - item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().fireAttack - item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().iceAttack - item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().lightningAttack - item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().waterAttack - item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = Engine.e.party[3].GetComponent<Character>().weaponRight.GetComponent<Weapon>().shadowAttack - item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength - physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus - fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus - iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus - lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus - waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus - shadowComparisonCalculation).ToString();

                            }
                            else
                            {
                                physicalComparisonCalculation = item.GetComponent<Weapon>().strengthBonus;
                                intelligenceComparisonCalculation = item.GetComponent<Weapon>().intelligenceBonus;
                                fireComparisonCalculation = item.GetComponent<Weapon>().fireAttack;
                                iceComparisonCalculation = item.GetComponent<Weapon>().iceAttack;
                                lightningComparisonCalculation = item.GetComponent<Weapon>().lightningAttack;
                                waterComparisonCalculation = item.GetComponent<Weapon>().waterAttack;
                                shadowComparisonCalculation = item.GetComponent<Weapon>().shadowAttack;

                                Engine.e.equipMenuReference.charAttackComparisonStats[0].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().strength + physicalComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[1].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().intelligence - intelligenceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[2].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().firePhysicalAttackBonus + fireComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[3].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().icePhysicalAttackBonus + iceComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[4].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().lightningPhysicalAttackBonus + lightningComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[5].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().waterPhysicalAttackBonus + waterComparisonCalculation).ToString();
                                Engine.e.equipMenuReference.charAttackComparisonStats[6].text = " ->  " + (Engine.e.party[3].GetComponent<Character>().shadowPhysicalAttackBonus + shadowComparisonCalculation).ToString();
                            }
                        }
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
    public void ClearHelpTextAccessory()
    {

        Engine.e.helpText.text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = string.Empty;

        Engine.e.equipMenuReference.charDefenseComparisonStats[0].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[1].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[2].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[3].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[4].text = string.Empty;
        Engine.e.equipMenuReference.charDefenseComparisonStats[5].text = string.Empty;
    }

    public void ClearHelpTextWeapon()
    {

        Engine.e.helpText.text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[0].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[1].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[2].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[3].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[4].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[5].text = string.Empty;
        Engine.e.equipMenuReference.charAttackComparisonStats[6].text = string.Empty;
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
                    if (item.useableOutOfBattle)
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
                }
                else
                {
                    Engine.e.partyInventoryReference.indexReference = index;

                    if (item != null)
                    {
                        if (item.itemType == "Weapon")
                        {

                            item.GetComponent<Weapon>().EquipWeapon();

                            ClearHelpTextWeapon();

                        }
                        else
                        {

                            if (item.itemType == "Armor")
                            {
                                item.GetComponent<ChestArmor>().DisplayArmorEquipCharacterTargets();

                                ClearHelpTextChestArmor();


                            }
                            else
                            {

                                if (item.itemType == "Accessory")
                                {
                                    item.GetComponent<Accessory>().DisplayAccessoryEquipCharacterTargets();

                                    ClearHelpTextAccessory();
                                }
                            }
                        }
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

