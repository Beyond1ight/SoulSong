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

    public Weapon[] weapons;
    public ChestArmor[] chestArmor;
    public Accessory[] accessories;

    public int grieveWeaponTotal, macWeaponTotal, fieldWeaponTotal, riggsWeaponTotal, solaceWeaponTotal, blueWeaponTotal;
    public int chestArmorTotal;
    public InventorySlot[] itemInventorySlots, chestArmorInventorySlots, accessoryInventorySlots, weaponInventorySlots;
    public bool inventoryScreenSet, battleScreenInventorySet;

    public int indexReference;
    public int inventoryPointerIndex = 0, vertMove = 0;
    public RectTransform partyInventoryRectTransform, battleItemsRectTransform, weaponRectTransform,
    chestArmorRectTransform, accessoryRectTransform;

    bool pressUp, pressDown, pressRelease = false;
    public GameObject confirmDropCheck, confirmDropYesButton;

    public void AddItemToInventory(Item item)
    {
        //int firstNull = -1;
        bool continueAdd = true;


        if (item.GetComponent<Weapon>())
        {

            for (int i = 0; i < chestArmor.Length; i++)
            {
                if (weapons[i] == null)
                {
                    weapons[i] = item.GetComponent<Weapon>();
                    weaponInventorySlots[i].AddItem(item);
                    break;
                }
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

        if (item.GetComponent<Accessory>())
        {
            for (int i = 0; i < accessories.Length; i++)
            {
                if (accessories[i] == null)
                {
                    accessories[i] = item.GetComponent<Accessory>();
                    accessoryInventorySlots[i].AddItem(item);
                    //chestArmorTotal++;
                    break;
                }
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


        if (item.GetComponent<Weapon>())
        {
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (weaponInventorySlots[i].item == item)
                {
                    weaponInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    weapons[i] = null;
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

        if (item.GetComponent<Accessory>())
        {
            for (int i = 0; i < accessoryInventorySlots.Length; i++)
            {
                if (accessoryInventorySlots[i].GetComponent<InventorySlot>().item == item)
                {
                    accessoryInventorySlots[i].GetComponent<InventorySlot>().ClearSlot();
                    accessories[i] = null;
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
        if (!Engine.e.equipMenuReference.removing)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[0].SetActive(true);
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().inventoryPointerIndex = 0;
            accessoryRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(weaponInventorySlots[inventoryPointerIndex].gameObject);
        }
        else
        {
            if (Engine.e.equipMenuReference.weaponRightInventorySet)
            {
                if (Engine.e.equipMenuReference.grieveScreen)
                {
                    if (Engine.e.party[0].GetComponent<Grieve>().weaponRight != null)
                    {
                        Engine.e.party[0].GetComponent<Grieve>().RemoveWeaponRight();
                        Engine.e.equipMenuReference.weaponRightInventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.macScreen)
                {
                    if (Engine.e.party[1].GetComponent<Mac>().weaponRight != null)
                    {
                        Engine.e.party[1].GetComponent<Mac>().RemoveWeaponRight();
                        Engine.e.equipMenuReference.weaponRightInventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.fieldScreen)
                {
                    if (Engine.e.party[2].GetComponent<Field>().weaponRight != null)
                    {
                        Engine.e.party[2].GetComponent<Field>().RemoveWeaponRight();
                        Engine.e.equipMenuReference.weaponRightInventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.riggsScreen)
                {
                    if (Engine.e.party[3].GetComponent<Riggs>().weaponRight != null)
                    {
                        Engine.e.party[3].GetComponent<Riggs>().RemoveWeaponRight();
                        Engine.e.equipMenuReference.weaponRightInventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.solaceScreen)
                {
                    if (Engine.e.party[4].GetComponent<Solace>().weaponRight != null)
                    {
                        Engine.e.party[4].GetComponent<Solace>().RemoveWeaponRight();
                        Engine.e.equipMenuReference.weaponRightInventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.blueScreen)
                {
                    if (Engine.e.party[5].GetComponent<Blue>().weaponRight != null)
                    {
                        Engine.e.party[5].GetComponent<Blue>().RemoveWeaponRight();
                        Engine.e.equipMenuReference.weaponRightInventorySet = false;
                    }
                }
            }
            /*else
            {
                if (Engine.e.equipMenuReference.weaponLeftInventorySet)
                {
                    if (Engine.e.equipMenuReference.grieveScreen)
                    {
                        if (Engine.e.party[0].GetComponent<Grieve>().weaponLeft != null)
                        {
                            Engine.e.party[0].GetComponent<Grieve>().RemoveWeaponLeft();
                            Engine.e.equipMenuReference.weaponLeftInventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.macScreen)
                    {
                        if (Engine.e.party[1].GetComponent<Mac>().weaponLeft != null)
                        {
                            Engine.e.party[1].GetComponent<Mac>().RemoveWeaponLeft();
                            Engine.e.equipMenuReference.weaponLeftInventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.fieldScreen)
                    {
                        if (Engine.e.party[2].GetComponent<Field>().weaponLeft != null)
                        {
                            Engine.e.party[2].GetComponent<Field>().RemoveWeaponLeft();
                            Engine.e.equipMenuReference.weaponLeftInventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.riggsScreen)
                    {
                        if (Engine.e.party[3].GetComponent<Riggs>().weaponLeft != null)
                        {
                            Engine.e.party[3].GetComponent<Riggs>().RemoveWeaponLeft();
                            Engine.e.equipMenuReference.weaponLeftInventorySet = false;
                        }
                    }
                }
            }*/
        }
    }

    public void OpenChestArmorInventory()
    {
        if (!Engine.e.equipMenuReference.removing)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().ActivateArmorList(0);
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().inventoryPointerIndex = 0;
            chestArmorRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(chestArmorInventorySlots[inventoryPointerIndex].gameObject);
        }
        else
        {
            if (Engine.e.equipMenuReference.grieveScreen)
            {
                if (Engine.e.party[0].GetComponent<Character>().chestArmor != null)
                {
                    Engine.e.party[0].GetComponent<Grieve>().RemoveChestArmor();
                }
            }
            if (Engine.e.equipMenuReference.macScreen)
            {
                if (Engine.e.party[1].GetComponent<Character>().chestArmor != null)
                {
                    Engine.e.party[1].GetComponent<Mac>().RemoveChestArmor();
                }
            }
            if (Engine.e.equipMenuReference.fieldScreen)
            {
                if (Engine.e.party[2].GetComponent<Character>().chestArmor != null)
                {
                    Engine.e.party[2].GetComponent<Field>().RemoveChestArmor();
                }
            }
            if (Engine.e.equipMenuReference.riggsScreen)
            {
                if (Engine.e.party[3].GetComponent<Character>().chestArmor != null)
                {
                    Engine.e.party[3].GetComponent<Riggs>().RemoveChestArmor();
                }
            }
            if (Engine.e.equipMenuReference.solaceScreen)
            {
                if (Engine.e.party[4].GetComponent<Character>().chestArmor != null)
                {
                    Engine.e.party[4].GetComponent<Solace>().RemoveChestArmor();
                }
            }
            if (Engine.e.equipMenuReference.blueScreen)
            {
                if (Engine.e.party[5].GetComponent<Character>().chestArmor != null)
                {
                    Engine.e.party[5].GetComponent<Blue>().RemoveChestArmor();
                }
            }
        }
    }

    public void OpenAccessoryInventory()
    {
        if (!Engine.e.equipMenuReference.removing)
        {
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().ActivateArmorList(1);
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().inventoryPointerIndex = 0;
            accessoryRectTransform.offsetMax = new Vector2(0, 0);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(accessoryInventorySlots[inventoryPointerIndex].gameObject);
        }
        else
        {
            if (Engine.e.equipMenuReference.accessory1InventorySet)
            {
                if (Engine.e.equipMenuReference.grieveScreen)
                {
                    if (Engine.e.party[0].GetComponent<Grieve>().accessory1 != null)
                    {
                        Engine.e.party[0].GetComponent<Grieve>().RemoveAccessory1();
                        Engine.e.equipMenuReference.accessory1InventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.macScreen)
                {
                    if (Engine.e.party[1].GetComponent<Mac>().accessory1 != null)
                    {
                        Engine.e.party[1].GetComponent<Mac>().RemoveAccessory1();
                        Engine.e.equipMenuReference.accessory1InventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.fieldScreen)
                {
                    if (Engine.e.party[2].GetComponent<Field>().accessory1 != null)
                    {
                        Engine.e.party[2].GetComponent<Field>().RemoveAccessory1();
                        Engine.e.equipMenuReference.accessory1InventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.riggsScreen)
                {
                    if (Engine.e.party[3].GetComponent<Riggs>().accessory1 != null)
                    {
                        Engine.e.party[3].GetComponent<Riggs>().RemoveAccessory1();
                        Engine.e.equipMenuReference.accessory1InventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.solaceScreen)
                {
                    if (Engine.e.party[4].GetComponent<Solace>().accessory1 != null)
                    {
                        Engine.e.party[4].GetComponent<Solace>().RemoveAccessory1();
                        Engine.e.equipMenuReference.accessory1InventorySet = false;
                    }
                }
                if (Engine.e.equipMenuReference.blueScreen)
                {
                    if (Engine.e.party[5].GetComponent<Blue>().accessory1 != null)
                    {
                        Engine.e.party[5].GetComponent<Blue>().RemoveAccessory1();
                        Engine.e.equipMenuReference.accessory1InventorySet = false;
                    }
                }
            }
            else
            {
                if (Engine.e.equipMenuReference.accessory2InventorySet)
                {
                    if (Engine.e.equipMenuReference.grieveScreen)
                    {
                        if (Engine.e.party[0].GetComponent<Grieve>().accessory2 != null)
                        {
                            Engine.e.party[0].GetComponent<Grieve>().RemoveAccessory2();
                            Engine.e.equipMenuReference.accessory2InventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.macScreen)
                    {
                        if (Engine.e.party[1].GetComponent<Mac>().accessory2 != null)
                        {
                            Engine.e.party[1].GetComponent<Mac>().RemoveAccessory2();
                            Engine.e.equipMenuReference.accessory2InventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.fieldScreen)
                    {
                        if (Engine.e.party[2].GetComponent<Field>().accessory2 != null)
                        {
                            Engine.e.party[2].GetComponent<Field>().RemoveAccessory2();
                            Engine.e.equipMenuReference.accessory2InventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.riggsScreen)
                    {
                        if (Engine.e.party[3].GetComponent<Riggs>().accessory2 != null)
                        {
                            Engine.e.party[3].GetComponent<Riggs>().RemoveAccessory2();
                            Engine.e.equipMenuReference.accessory2InventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.solaceScreen)
                    {
                        if (Engine.e.party[4].GetComponent<Solace>().accessory2 != null)
                        {
                            Engine.e.party[4].GetComponent<Solace>().RemoveAccessory2();
                            Engine.e.equipMenuReference.accessory2InventorySet = false;
                        }
                    }
                    if (Engine.e.equipMenuReference.blueScreen)
                    {
                        if (Engine.e.party[5].GetComponent<Blue>().accessory2 != null)
                        {
                            Engine.e.party[5].GetComponent<Blue>().RemoveAccessory2();
                            Engine.e.equipMenuReference.accessory2InventorySet = false;
                        }
                    }
                }
            }
        }
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


