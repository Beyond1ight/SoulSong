using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Merchant : MonoBehaviour
{
    public Quest quest;
    public TextMeshProUGUI textDisplay, storeTextDisplay, partyGAmount, storeHelpText;
    public string shopDialogue;
    public string[] sentences;
    public string[] questSentences;
    private int index;
    private int questIndex;
    public float typingSpeed;
    public GameObject talkButton, endTalkButton, continueButton, endButton;
    public GameObject shopButton, sellButton;
    public GameObject dialogueScreen, saleSelection;
    public GameObject confirmPurchase, denyPurchase;
    private bool talking = false;
    public bool buysAll, buysItems, buysWeapons, buysArmor, buysDrops = false;
    public MerchantInventorySlot[] inventorySlots;
    public List<Item> inventoryList;
    public bool storeInventoryScreenSet = false;
    public GameObject dialogueFirstButton, storeFirstButton;
    public Item itemReference;
    bool pressUp, pressDown = false;
    public int inventoryPointerIndex = 0, vertMove = 0;
    public RectTransform storeInventoryRectTransform;

    void Awake()
    {
        if (quest != null)
        {
            questSentences = new string[quest.questDialogue.Length];
        }

        for (int i = 0; i < questSentences.Length; i++)
        {
            questSentences[i] = quest.questDialogue[i];
        }

        for (int i = 0; i < inventoryList.Count; i++)
        {
            inventorySlots[i].AddItem(inventoryList[i]);
            inventorySlots[i].item.itemIndex = i;
        }
    }

    void Update()
    {
        if (GetComponent<NPC>().playerInRange && !Engine.e.inBattle)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 0"))
            {
                talking = true;
                Engine.e.inBattle = true;
                textDisplay.text = string.Empty;
                dialogueScreen.SetActive(true);
                Engine.e.interactionPopup.SetActive(false);
                StartCoroutine(Greeting());

                Engine.e.currentMerchant = this;

            }
        }

        if (talking)
        {
            if (quest == null)
            {
                if (textDisplay.text == sentences[index])
                {
                    continueButton.SetActive(true);
                }
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton);
            }
            else
            {
                if (textDisplay.text == questSentences[questIndex])
                {
                    continueButton.SetActive(true);
                }
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton);
            }
        }
        else
        {
            continueButton.SetActive(false);
        }

        if (storeInventoryScreenSet)
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
    IEnumerator Greeting()
    {
        foreach (char letter in sentences[0].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }

        talking = false;
        shopButton.SetActive(true);
        sellButton.SetActive(true);
        talkButton.SetActive(true);
        endButton.SetActive(true);

        continueButton.SetActive(false);
        OpenDialogueMenu();

    }

    IEnumerator Type()
    {
        talking = false;

        if (quest == null)
        {
            foreach (char letter in sentences[index].ToCharArray())
            {
                textDisplay.text += letter;

                yield return new WaitForSeconds(typingSpeed);

            }
            if (index == sentences.Length - 1)
            {
                talking = false;
                endTalkButton.SetActive(true);
                continueButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(endTalkButton);
            }
            else
                talking = true;

        }
        else
        {
            foreach (char letter in questSentences[questIndex].ToCharArray())
            {
                textDisplay.text += letter;

                yield return new WaitForSeconds(typingSpeed);

            }
            if (questIndex == questSentences.Length - 1)
            {
                talking = false;
                endTalkButton.SetActive(true);
                continueButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(endTalkButton);
            }
            else
                talking = true;

        }
    }


    public void Talk()
    {
        continueButton.SetActive(false);

        if (quest == null)
        {
            index++;
        }
        else
        {

            questIndex++;

        }

        talking = true;
        textDisplay.text = string.Empty;

        StartCoroutine(Type());

        endButton.SetActive(false);
        talkButton.SetActive(false);
        shopButton.SetActive(false);
        sellButton.SetActive(false);


    }
    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (quest == null)
        {
            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = string.Empty;
                StartCoroutine(Type());
                continueButton.SetActive(false);
            }
        }
        else
        {
            if (questIndex < questSentences.Length - 1)
            {
                questIndex++;
                textDisplay.text = string.Empty;
                StartCoroutine(Type());
                continueButton.SetActive(false);
            }
        }
    }

    public void EndTalk()
    {
        textDisplay.text = string.Empty;
        index = 0;
        questIndex = 0;
        talking = false;

        if (quest != null)
        {
            Engine.e.adventureLogReference.AddQuestToAdventureLog(quest);
        }
        StartCoroutine(Greeting());

        continueButton.SetActive(false);
        endTalkButton.SetActive(false);
    }
    public void EndDialogue()
    {
        textDisplay.text = string.Empty;
        textDisplay.text = sentences[index];

        index = 0;
        questIndex = 0;

        dialogueScreen.SetActive(false);
        endButton.SetActive(false);
        shopButton.SetActive(false);
        talkButton.SetActive(false);
        continueButton.SetActive(false);
        Engine.e.inBattle = false;
        Engine.e.interactionPopup.SetActive(true);

    }

    public void DisplaySaleSelection()
    {
        storeTextDisplay.text = string.Empty;
        storeTextDisplay.text = shopDialogue;
        dialogueScreen.SetActive(false);
        saleSelection.SetActive(true);
        partyGAmount.text = "G: " + Engine.e.partyMoney;

        OpenStoreMenu();

    }

    public void CloseSaleSelection()
    {
        saleSelection.SetActive(false);
        dialogueScreen.SetActive(true);
        storeTextDisplay.text = string.Empty;

        OpenDialogueMenu();

    }

    public void BuyItemCheck(Item _item)
    {
        if (Engine.e.partyMoney >= _item.itemValue)
        {
            storeTextDisplay.text = string.Empty;
            storeTextDisplay.text = "Are you sure you want to purchase the " + _item.itemName + " " + "for " + _item.itemValue + "G?";

            itemReference = _item;

            confirmPurchase.SetActive(true);
            denyPurchase.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(confirmPurchase);

            /* for (int i = 0; i < grieveWeaponStats.Length; i++)
             {
                 grieveWeaponStats[i].text = string.Empty;
             }
             grieveWeaponStats[0].text = Engine.e.grieveGameWeapons[index].GetComponent<GrieveWeapons>().itemName;
             grieveWeaponStats[1].text = "\nPhysical Attack: " + Engine.e.grieveGameWeapons[index].GetComponent<GrieveWeapons>().physicalAttack.ToString();
             grieveWeaponStats[2].text = "\nFire Damage: " + Engine.e.grieveGameWeapons[index].GetComponent<GrieveWeapons>().fireAttack.ToString();
             grieveWeaponStats[3].text = "\nWater Damage: " + Engine.e.grieveGameWeapons[index].GetComponent<GrieveWeapons>().waterAttack.ToString();
             grieveWeaponStats[4].text = "\nLightning Damage: " + Engine.e.grieveGameWeapons[index].GetComponent<GrieveWeapons>().lightningAttack.ToString();
             grieveWeaponStats[5].text = "\nShadow Damage: " + Engine.e.grieveGameWeapons[index].GetComponent<GrieveWeapons>().shadowAttack.ToString();
 */
            int amountOwned = 0;


            if (_item.stackable)
            {
                amountOwned = _item.numberHeld;
            }
            else
            {
                foreach (Item item in Engine.e.partyInventoryReference.partyInventory)
                {
                    if (item == _item)
                    {
                        amountOwned++;
                    }
                }
            }
        }

        /* grieveWeaponStats[6].text = "Amount owned: " + amountOwned;


         grieveCurrentWStats[0].text = Engine.e.party[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().itemName;
         grieveCurrentWStats[1].text = "\nPhysical Attack: " + Engine.e.party[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().physicalAttack.ToString();
         grieveCurrentWStats[2].text = "\nFire Damage: " + Engine.e.party[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().fireAttack.ToString();
         grieveCurrentWStats[3].text = "\nWater Damage: " + Engine.e.party[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().waterAttack.ToString();
         grieveCurrentWStats[4].text = "\nLightning Damage: " + Engine.e.party[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().lightningAttack.ToString();
         grieveCurrentWStats[5].text = "\nShadow Damage: " + Engine.e.party[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().shadowAttack.ToString();
         chosenCharacter = string.Empty;
         chosenCharacter = "Grieve";
     }
         else
         {
             storeTextDisplay.text = string.Empty;
             storeTextDisplay.text = "You don't have enough money!";
             for (int i = 0; i<grieveWeaponStats.Length; i++)
             {
                 grieveWeaponStats[i].text = string.Empty;
             }
 grieveWeaponBackButton.SetActive(true);
 OpenGrieveWeaponMenu();
         }*/
    }

    public void BuyItem()
    {
        if (Engine.e.partyMoney >= itemReference.itemValue)
        {
            Engine.e.partyMoney -= itemReference.itemValue;
            Engine.e.partyInventoryReference.AddItemToInventory(itemReference);
            storeTextDisplay.text = string.Empty;
            storeTextDisplay.text = "Transaction complete!";
            partyGAmount.text = "G: " + Engine.e.partyMoney;

            itemReference = null;
            confirmPurchase.SetActive(false);
            denyPurchase.SetActive(false);

            OpenStoreMenu();

        }
    }

    public void SellItemCheck(Item _item)
    {
        bool moveOn = false;

        if (_item.resaleValue > 0)
        {
            if (_item.itemType == "Item")
            {
                if (buysAll || buysItems)
                {
                    {
                        moveOn = true;
                    }
                }
            }

            if (_item.itemType == "Weapon")
            {
                if (buysAll || buysWeapons)
                {
                    moveOn = true;
                }
            }

            if (_item.itemType == "Armor")
            {

                if (buysAll || buysArmor)
                {
                    moveOn = true;
                }
            }
            if (_item.itemType == "Drop")
            {
                if (buysAll || buysDrops)
                {
                    moveOn = true;
                }
            }

            if (moveOn)
            {
                itemReference = _item;

                Engine.e.storeDialogueReference.text = "I'll buy your " + _item.itemName + " " + "for " + _item.resaleValue + "G.";

                Engine.e.shopSellButtons[0].SetActive(true);
                Engine.e.shopSellButtons[1].SetActive(true);

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(Engine.e.shopSellButtons[0].gameObject);
            }
            else
            {
                Engine.e.storeDialogueReference.text = "Can't buy that.";
                return;
            }
        }
        else
        {
            Engine.e.storeDialogueReference.text = "Can't buy that.";
            return;
        }
    }

    public void SellItem()
    {
        Engine.e.partyMoney += itemReference.resaleValue;
        Engine.e.partyInventoryReference.SubtractItemFromInventory(itemReference);
        storeTextDisplay.text = string.Empty;
        Engine.e.storeDialogueReference.text = "Transaction complete!";
        partyGAmount.text = "G: " + Engine.e.partyMoney;

        itemReference = null;

        OpenInventoryMenu();
    }

    public void DenyPurchase()
    {
        confirmPurchase.SetActive(false);
        denyPurchase.SetActive(false);

        storeTextDisplay.text = string.Empty;
        storeTextDisplay.text = shopDialogue;

        itemReference = null;
        OpenStoreMenu();
    }

    public void Selling()
    {
        Engine.e.selling = true;
        dialogueScreen.SetActive(false);
        Engine.e.inventoryMenuReference.SetActive(true);
        Engine.e.partyInventoryReference.OpenInventoryMenu();

    }

    public void OpenDialogueMenu()
    {
        Engine.e.storeDialogueReference.text = string.Empty;
        dialogueScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(dialogueFirstButton);

        storeInventoryScreenSet = false;

    }

    public void OpenStoreMenu()
    {
        storeInventoryScreenSet = true;
        inventoryPointerIndex = 0;
        saleSelection.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(inventorySlots[0].gameObject);

    }
    public void OpenInventoryMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Engine.e.partyInventoryReference.itemInventorySlots[0].gameObject);
    }

    void HandleInventory()
    {
        // "Store Inventory" 
        if (storeInventoryScreenSet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < inventorySlots.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(inventorySlots[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 5 && inventoryPointerIndex < inventorySlots.Length)
                    {
                        storeInventoryRectTransform.offsetMax -= new Vector2(0, -30);
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
                    EventSystem.current.SetSelectedGameObject(inventorySlots[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 5 && inventoryPointerIndex > 0)
                    {
                        storeInventoryRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }
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
}

