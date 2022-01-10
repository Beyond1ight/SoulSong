using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class PartyInventory : MonoBehaviour
{
    public Item[] partyInventory;
    public GrieveWeapons[] grieveWeapons;
    public MacWeapons[] macWeapons;
    public FieldWeapons[] fieldWeapons;
    public RiggsWeapons[] riggsWeapons;
    public ChestArmor[] chestArmor;

    public int grieveWeaponTotal, macWeaponTotal, fieldWeaponTotal, riggsWeaponTotal;
    public int chestArmorTotal;
    public InventorySlot[] itemInventorySlots, chestArmorInventorySlots, grieveWeaponInventorySlots, macWeaponInventorySlots, fieldWeaponInventorySlots, riggsWeaponInventorySlots;
    public bool inventoryScreenSet, battleScreenInventorySet;

    public int indexReference;
    public int inventoryPointerIndex = 0, vertMove = 0;
    public RectTransform partyInventoryRectTransform, battleItemsRectTransform, grieveWeaponsRectTransform, macWeaponsRectTransform, fieldWeaponsRectTransform, riggsWeaponsRectTransform,
    chestArmorRectTransform;

    bool pressUp, pressDown, pressRelease = false;
    public GameObject confirmDropCheck, confirmDropYesButton;

    public void AddItemToInventory(Item item)
    {
        //int firstNull = -1;
        bool continueAdd = true;

        if (item.GetComponent<GrieveWeapons>())
        {
            if (grieveWeaponTotal < 10)
            {
                for (int i = 0; i < grieveWeapons.Length; i++)
                {
                    if (grieveWeapons[i] == null)
                    {
                        grieveWeapons[i] = item.GetComponent<GrieveWeapons>();
                        grieveWeaponInventorySlots[i].AddItem(item);
                        grieveWeaponTotal++;
                        break;
                    }
                }
            }
            else
            {
                continueAdd = false;
            }
        }

        if (item.GetComponent<MacWeapons>())
        {
            if (macWeaponTotal < 10)
            {
                for (int i = 0; i < macWeapons.Length; i++)
                {
                    if (macWeapons[i] == null)
                    {
                        macWeapons[i] = item.GetComponent<MacWeapons>();
                        macWeaponInventorySlots[i].AddItem(item);
                        macWeaponTotal++;
                        break;
                    }
                }
            }
            else
            {
                continueAdd = false;
            }
        }

        if (item.GetComponent<FieldWeapons>())
        {
            if (fieldWeaponTotal < 10)
            {
                for (int i = 0; i < fieldWeapons.Length; i++)
                {
                    if (fieldWeapons[i] == null)
                    {
                        fieldWeapons[i] = item.GetComponent<FieldWeapons>();
                        fieldWeaponInventorySlots[i].AddItem(item);
                        fieldWeaponTotal++;
                        break;
                    }
                }
            }
            else
            {
                continueAdd = false;
            }
        }

        if (item.GetComponent<RiggsWeapons>())
        {
            if (riggsWeaponTotal < 10)
            {
                for (int i = 0; i < riggsWeapons.Length; i++)
                {
                    if (riggsWeapons[i] == null)
                    {
                        riggsWeapons[i] = item.GetComponent<RiggsWeapons>();
                        riggsWeaponInventorySlots[i].AddItem(item);
                        riggsWeaponTotal++;
                        break;
                    }
                }
            }
            else
            {
                continueAdd = false;
            }
        }

        if (item.GetComponent<ChestArmor>())
        {
            if (chestArmorTotal < 20)
            {
                for (int i = 0; i < chestArmor.Length; i++)
                {
                    if (chestArmor[i] == null)
                    {
                        chestArmor[i] = item.GetComponent<ChestArmor>();
                        chestArmorInventorySlots[i].AddItem(item);
                        chestArmorTotal++;
                        break;
                    }
                }
            }
            else
            {
                continueAdd = false;
            }
        }


        if (continueAdd)
        {
            for (int i = 0; i < partyInventory.Length; i++)
            {
                if (partyInventory[i] == item)
                {
                    if (item.stackable && partyInventory[i].numberHeld < 99)
                    {
                        partyInventory[i].numberHeld++;
                        itemInventorySlots[i].UpdateHeld();
                        break;
                    }
                    else
                    {
                        if (partyInventory[i] == null)
                        {
                            partyInventory[i] = item;
                            itemInventorySlots[i].AddItem(item);

                            break;
                        }
                    }
                }

                else
                {
                    if (partyInventory[i] == null)
                    {
                        partyInventory[i] = item;
                        itemInventorySlots[i].AddItem(item);
                        break;
                    }
                }
            }

        }
        if (Engine.e.inBattle)
        {
            if (item.itemType == "Item")
                AddStolenItemToBattleInventory(item);
        }
    }

    public void AddItemsToBattleInventory()
    {

        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                if (partyInventory[i].itemType == "Item")
                {
                    Engine.e.battleSystem.battleItems[i].AddItem(partyInventory[i]);
                }
            }
        }
    }

    public void AddStolenItemToBattleInventory(Item _item)
    {
        for (int i = 0; i < Engine.e.battleSystem.battleItems.Length; i++)
        {
            if (Engine.e.battleSystem.battleItems[i].item == _item)
            {
                if (Engine.e.battleSystem.battleItems[i].item.numberHeld < 99)
                {
                    Engine.e.battleSystem.battleItems[i].item.numberHeld++;
                    Engine.e.battleSystem.battleItems[i].UpdateHeld();
                    break;
                }
                else
                {
                    Engine.e.battleSystem.battleHelpReference.text = "Carrying too many " + _item.itemName + "'s!";
                    break;
                }
            }
            else
            {
                if (Engine.e.battleSystem.battleItems[i].item == null)
                {
                    Engine.e.battleSystem.battleItems[i].AddItem(_item);
                    break;
                }
            }
        }
    }

    public void ReturnToInventory()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(itemInventorySlots[inventoryPointerIndex].gameObject);
        }
    }

    public void ConfirmDropCheck()
    {
        confirmDropCheck.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(confirmDropYesButton);

        Engine.e.helpText.text = "Teach " + Engine.e.playableCharacters[Engine.e.characterBeingTargeted].GetComponent<Character>().characterName + " " + Engine.e.itemToBeUsed.itemName + "?";
    }

    public void DismissDropConfirm()
    {
        confirmDropCheck.SetActive(false);
        Engine.e.helpText.text = string.Empty;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(itemInventorySlots[inventoryPointerIndex].gameObject);
    }
    // Removes a specific consumable item from the party's inventory.
    public void SubtractItemFromInventory(Item item)
    {

        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] == item)
            {
                if (item.stackable)
                {
                    partyInventory[i].GetComponent<Item>().numberHeld--;
                    itemInventorySlots[i].itemCount.GetComponent<TMP_Text>().text = ": " + partyInventory[i].GetComponent<Item>().numberHeld.ToString();
                    if (partyInventory[i].GetComponent<Item>().numberHeld <= 0)
                    {
                        //Destroy(partyInventory[i].GetComponent<Item>().inventoryButtonLogic);
                        itemInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                        partyInventory[i] = null;
                        break;
                    }
                }
                else
                {
                    itemInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    partyInventory[i] = null;
                    break;
                }
            }
        }

        if (Engine.e.inBattle)
        {
            for (int i = 0; i < Engine.e.battleSystem.battleItems.Length; i++)
            {
                if (Engine.e.battleSystem.battleItems[i].item == item)
                {
                    if (item.stackable)
                    {
                        Engine.e.battleSystem.battleItems[i].itemCount.GetComponent<TMP_Text>().text = Engine.e.battleSystem.battleItems[i].item.numberHeld.ToString();

                        if (Engine.e.battleSystem.battleItems[i].item.numberHeld <= 0)
                        {

                            Engine.e.battleSystem.battleItems[i].ClearSlot();
                            Engine.e.battleSystem.battleItems[i].item = null;
                            break;
                        }
                    }
                    else
                    {
                        Engine.e.battleSystem.battleItems[i].ClearSlot();
                        Engine.e.battleSystem.battleItems[i].item = null;
                        break;
                    }
                }
            }
        }


        if (item.GetComponent<GrieveWeapons>())
        {
            for (int i = 0; i < grieveWeaponInventorySlots.Length; i++)
            {
                if (grieveWeaponInventorySlots[i].item == item)
                {
                    grieveWeaponInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    grieveWeapons[i] = null;
                    break;
                }
            }
        }

        if (item.GetComponent<MacWeapons>())
        {
            for (int i = 0; i < macWeaponInventorySlots.Length; i++)
            {
                if (macWeaponInventorySlots[i].GetComponent<InventorySlot>().item == item)
                {
                    macWeaponInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    macWeapons[i] = null;
                    break;
                }
            }
        }

        if (item.GetComponent<FieldWeapons>())
        {
            for (int i = 0; i < fieldWeaponInventorySlots.Length; i++)
            {
                if (fieldWeaponInventorySlots[i].GetComponent<InventorySlot>().item == item)
                {
                    fieldWeaponInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    fieldWeapons[i] = null;
                    break;
                }
            }
        }

        if (item.GetComponent<RiggsWeapons>())
        {
            for (int i = 0; i < riggsWeaponInventorySlots.Length; i++)
            {
                if (riggsWeaponInventorySlots[i].GetComponent<InventorySlot>().item == item)
                {
                    riggsWeaponInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    riggsWeapons[i] = null;
                    break;
                }
            }
        }

        if (item.GetComponent<ChestArmor>())
        {
            for (int i = 0; i < chestArmorInventorySlots.Length; i++)
            {
                if (chestArmorInventorySlots[i].GetComponent<InventorySlot>().item == item)
                {
                    chestArmorInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    chestArmor[i] = null;
                    break;
                }
            }
        }
    }

    public void OpenInventoryMenu()
    {
        if (!Engine.e.inBattle || Engine.e.selling)
        {
            inventoryPointerIndex = 0;
            partyInventoryRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(itemInventorySlots[inventoryPointerIndex].gameObject);

        }
        else
        {
            Engine.e.battleSystem.inventoryPointerIndex = 0;
            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(Engine.e.battleSystem.battleItems[inventoryPointerIndex].gameObject);
        }
    }

    public void OpenWeaponInventory()
    {

        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().inventoryPointerIndex = 0;

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
        {
            grieveWeaponsRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(grieveWeaponInventorySlots[inventoryPointerIndex].gameObject);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
        {
            macWeaponsRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(macWeaponInventorySlots[inventoryPointerIndex].gameObject);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
        {
            fieldWeaponsRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(fieldWeaponInventorySlots[inventoryPointerIndex].gameObject);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
        {
            riggsWeaponsRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(riggsWeaponInventorySlots[inventoryPointerIndex].gameObject);
        }
    }

    public void OpenChestArmorInventory()
    {
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().ActivateArmorList(0);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().inventoryPointerIndex = 0;
        chestArmorRectTransform.offsetMax = new Vector2(0, 0);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(chestArmorInventorySlots[inventoryPointerIndex].gameObject);
    }


    // Handles Various Menu Navigation
    void HandleInventory()
    {
        // "Pause Menu" Inventory
        if (inventoryScreenSet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < partyInventory.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(itemInventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < partyInventory.Length)
                    {
                        partyInventoryRectTransform.offsetMax -= new Vector2(0, -30);
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
                    EventSystem.current.SetSelectedGameObject(itemInventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        partyInventoryRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }
    }

    public void SortInventoryByName()
    {
        List<Item> sortedList = new List<Item>();
        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                sortedList.Add(partyInventory[i]);
                partyInventory[i] = null;
                itemInventorySlots[i].ClearSlot();
            }
        }

        sortedList = sortedList.OrderBy(item => item.itemName).ToList();

        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] != null)
            {
                partyInventory[i] = sortedList[i];
                itemInventorySlots[i].AddItemSorting(partyInventory[i]);
            }
        }

        inventoryPointerIndex = 0;
        OpenInventoryMenu();
    }

    public void SortInventoryByType()
    {
        List<Item> sortedList = new List<Item>();
        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                sortedList.Add(partyInventory[i]);
                partyInventory[i] = null;
                itemInventorySlots[i].ClearSlot();
            }
        }

        sortedList = sortedList.OrderBy(item => item.itemType).ToList();

        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] != null)
            {
                partyInventory[i] = sortedList[i];
                itemInventorySlots[i].AddItemSorting(partyInventory[i]);
            }
        }

        inventoryPointerIndex = 0;
        OpenInventoryMenu();
    }

    public void SortInventoryByWeapon()
    {
        List<Item> sortedList = new List<Item>();
        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                sortedList.Add(partyInventory[i]);
                partyInventory[i] = null;
                itemInventorySlots[i].ClearSlot();
            }
        }

        sortedList = sortedList.OrderBy(item => !item.weapon).ToList();

        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] != null)
            {
                partyInventory[i] = sortedList[i];
                itemInventorySlots[i].AddItemSorting(partyInventory[i]);
            }
        }

        inventoryPointerIndex = 0;
        OpenInventoryMenu();
    }

    public void SortInventoryByChestArmor()
    {
        List<Item> sortedList = new List<Item>();
        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                sortedList.Add(partyInventory[i]);
                partyInventory[i] = null;
                itemInventorySlots[i].ClearSlot();
            }
        }

        sortedList = sortedList.OrderBy(item => !item.chestArmor).ToList();

        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] != null)
            {
                partyInventory[i] = sortedList[i];
                itemInventorySlots[i].AddItemSorting(partyInventory[i]);
            }
        }

        inventoryPointerIndex = 0;
        OpenInventoryMenu();
    }

    public void SortInventoryByDrop()
    {
        List<Item> sortedList = new List<Item>();
        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                sortedList.Add(partyInventory[i]);
                partyInventory[i] = null;
                itemInventorySlots[i].ClearSlot();
            }
        }

        sortedList = sortedList.OrderBy(item => !item.drop).ToList();

        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] != null)
            {
                partyInventory[i] = sortedList[i];
                itemInventorySlots[i].AddItemSorting(partyInventory[i]);
            }
        }

        inventoryPointerIndex = 0;
        OpenInventoryMenu();
    }

    // "Consumable Item" (excludes Drops)
    public void SortInventoryByItem()
    {
        List<Item> sortedList = new List<Item>();
        for (int i = 0; i < partyInventory.Length; i++)
        {
            if (partyInventory[i] != null)
            {
                sortedList.Add(partyInventory[i]);
                partyInventory[i] = null;
                itemInventorySlots[i].ClearSlot();
            }
        }

        sortedList = sortedList.OrderBy(item => !item.consumable).ToList();

        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] != null)
            {
                partyInventory[i] = sortedList[i];
                itemInventorySlots[i].AddItemSorting(partyInventory[i]);
            }
        }

        inventoryPointerIndex = 0;
        OpenInventoryMenu();
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

        if (inventoryScreenSet)
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


