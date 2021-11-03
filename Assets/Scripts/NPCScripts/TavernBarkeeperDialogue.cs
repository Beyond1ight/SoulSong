using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TavernBarkeeperDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI storeTextDisplay;
    public TextMeshProUGUI partyGAmount;
    public int roomFee;
    public string shopDialogue;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject roomButton;
    public GameObject continueButton;
    public GameObject shopButton;
    public GameObject talkButton;
    public GameObject endTalkButton;
    public GameObject endButton;
    public GameObject dialogueScreen;
    public GameObject saleSelection;
    public GameObject confirmFoodPurchase;
    public GameObject denyPurchase;
    private bool talking = false;
    private int tempIndex;
    public TextMeshProUGUI[] itemStats;
    public GameObject foodBackButton;
    public GameObject rentRoomNo;
    public string scene;
    public string sceneUnload;
    public bool loaded;
    public bool unloaded;
    public Animator transition;
    public GameObject toLocation;
    public GameObject activeParty2Location;
    public GameObject activeParty3Location;
    public GameObject[] sleepUntilButtons;
    public GameObject dialogueFirstButton;

    void Update()
    {
        if (GetComponent<NPC>().playerInRange && !Engine.e.inBattle)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 0"))
            {
                Engine.e.interactionPopup.SetActive(false);
                talking = true;
                Engine.e.inBattle = true;
                textDisplay.text = string.Empty;
                dialogueScreen.SetActive(true);

                StartCoroutine(Greeting());

                Engine.e.currentTavern = this;
                partyGAmount.text = string.Empty;
                partyGAmount.text = "G: " + Engine.e.partyMoney;
            }
        }

        if (talking)
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
            continueButton.SetActive(false);
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
        roomButton.SetActive(true);
        shopButton.SetActive(true);
        talkButton.SetActive(true);
        endButton.SetActive(true);

        continueButton.SetActive(false);
        OpenDialogueMenu();
    }

    IEnumerator Type()
    {
        talking = false;
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

    public void RoomCheck()
    {
        //continueButton.SetActive(false);
        //index++;
        //talking = true;
        if (Engine.e.partyMoney >= roomFee)
        {
            textDisplay.text = string.Empty;
            textDisplay.text = "A room is " + roomFee + "G to rent. Would you like one?";
            EventSystem.current.SetSelectedGameObject(null);
            //StartCoroutine(Type());
            if (Engine.e.timeOfDay <= 400f || Engine.e.timeOfDay >= 650f)
            {
                sleepUntilButtons[1].SetActive(true);
                EventSystem.current.SetSelectedGameObject(sleepUntilButtons[1]);

            }
            else
            {
                sleepUntilButtons[0].SetActive(true);
                EventSystem.current.SetSelectedGameObject(sleepUntilButtons[0]);

            }

            rentRoomNo.SetActive(true);
            endButton.SetActive(false);
            talkButton.SetActive(false);
            shopButton.SetActive(false);
        }
        else
        {
            textDisplay.text = string.Empty;
            textDisplay.text = "Sorry, you don't have enough money.";
            OpenDialogueMenu();
        }
    }
    void LoadIntoScene()
    {
        loaded = true;
        unloaded = false;
        transition.SetTrigger("Start");
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        Engine.e.UnloadScene(sceneUnload);
        Engine.e.inBattle = false;
        Engine.e.activeParty.transform.position = new Vector3(toLocation.transform.position.x, toLocation.transform.position.y);
        Engine.e.activePartyMember2.transform.position = new Vector3(activeParty2Location.transform.position.x, activeParty2Location.transform.position.y);
        Engine.e.activePartyMember3.transform.position = new Vector3(activeParty3Location.transform.position.x, activeParty3Location.transform.position.y);
    }

    public void RoomConfirmNight()
    {
        Engine.e.partyMoney -= roomFee;
        textDisplay.text = string.Empty;
        SleepingUntilMorning();
    }

    public void SleepingUntilMorning()
    {
        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                Engine.e.party[i].GetComponent<Character>().currentHealth = Engine.e.party[i].GetComponent<Character>().maxHealth + 50;
                Engine.e.party[i].GetComponent<Character>().currentMana = Engine.e.party[i].GetComponent<Character>().maxMana + 30;
                Engine.e.party[i].GetComponent<Character>().currentEnergy = Engine.e.party[i].GetComponent<Character>().maxEnergy + 25;

                Engine.e.timeOfDay = 750f;
            }
        }

        LoadIntoScene();

    }

    public void RoomConfirmDay()
    {
        Engine.e.partyMoney -= roomFee;
        textDisplay.text = string.Empty;
        SleepingUntilNight();
    }

    public void SleepingUntilNight()
    {
        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                Engine.e.party[i].GetComponent<Character>().currentHealth = Engine.e.party[i].GetComponent<Character>().maxHealth + 50;
                Engine.e.party[i].GetComponent<Character>().currentMana = Engine.e.party[i].GetComponent<Character>().maxMana + 30;
                Engine.e.party[i].GetComponent<Character>().currentEnergy = Engine.e.party[i].GetComponent<Character>().maxEnergy + 25;

                Engine.e.timeOfDay = 400f;
            }
        }

        LoadIntoScene();

    }

    public void Talk()
    {
        continueButton.SetActive(false);
        index++;
        talking = true;
        textDisplay.text = string.Empty;

        StartCoroutine(Type());

        roomButton.SetActive(false);
        endButton.SetActive(false);
        talkButton.SetActive(false);
        shopButton.SetActive(false);


    }
    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = string.Empty;
            StartCoroutine(Type());
            continueButton.SetActive(false);
        }
    }

    public void EndTalk()
    {
        textDisplay.text = string.Empty;
        index = 0;
        talking = false;
        StartCoroutine(Greeting());

        continueButton.SetActive(false);
        endTalkButton.SetActive(false);
        OpenDialogueMenu();

    }
    public void EndDialogue()
    {
        textDisplay.text = string.Empty;
        textDisplay.text = sentences[index];
        index = 0;
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
        // OpenStoreMenu();

    }

    public void CloseSaleSelection()
    {
        saleSelection.SetActive(false);
        dialogueScreen.SetActive(true);
        storeTextDisplay.text = string.Empty;
        OpenDialogueMenu();

    }

    public void DenyPurchase()
    {
        denyPurchase.SetActive(false);
        storeTextDisplay.text = string.Empty;
        storeTextDisplay.text = shopDialogue;

    }
    public void DenyRoom()
    {

        rentRoomNo.SetActive(false);
        sleepUntilButtons[0].SetActive(false);
        sleepUntilButtons[1].SetActive(false);
        textDisplay.text = string.Empty;
        textDisplay.text = "No problem, maybe some food or drink then?";
        roomButton.SetActive(true);
        talkButton.SetActive(true);
        endButton.SetActive(true);
        shopButton.SetActive(true);
        OpenDialogueMenu();
    }

    public void OpenDialogueMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(dialogueFirstButton);
    }
}
