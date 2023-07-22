using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueReceiver : MonoBehaviour, INotificationReceiver
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index, questIndex, conversationCountBeforeMove;
    public float typingSpeed;
    public GameObject continueButton, continueConversationButton;
    public GameObject endButton;
    public GameObject dialogueScreen;
    bool talking = false, currentlyTalking = false;
    public bool continueConversation = false, hasBeenShown = false;


    public Quest quest;

    public string[] questSentences, questCompleteSentences;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is DialogueMarker dialogueMarker)
        {
            continueConversation = false;

            index++;
            Engine.e.inBattle = true;
            textDisplay.text = string.Empty;
            dialogueScreen.SetActive(true);
            GetComponent<PlayableDirector>().Pause();

            Talk();
        }
        else
        {
            if (notification is ContinueConversationMarker continueMarker)
            {
                continueConversation = true;

                index++;
                Engine.e.inBattle = true;
                textDisplay.text = string.Empty;
                dialogueScreen.SetActive(true);
                GetComponent<PlayableDirector>().Pause();

                Talk();
            }
        }
    }

    public void Talk()
    {

        continueButton.SetActive(false);
        continueConversationButton.SetActive(false);

        if (quest == null)
        {
            //index++;
        }
        else
        {

            //questIndex++;
        }

        talking = true;
        textDisplay.text = string.Empty;

        StartCoroutine(Dialogue());

        endButton.SetActive(false);

    }

    void Update()
    {
        if (talking)
        {
            if (quest == null)
            {
                if (textDisplay.text == sentences[index])
                {
                    if (!continueConversation)
                    {
                        continueButton.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(continueButton);
                    }
                    else
                    {
                        if (conversationCountBeforeMove != 0)
                        {
                            continueConversationButton.SetActive(true);
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(continueConversationButton);
                        }
                        else
                        {
                            continueConversation = false;
                            continueButton.SetActive(true);
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(continueButton);
                        }
                    }
                }

            }
            else
            {
                if (textDisplay.text == questSentences[questIndex] || textDisplay.text == questCompleteSentences[questIndex])
                {
                    if (!continueConversation)
                    {
                        continueButton.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(continueButton);
                    }
                    else
                    {
                        continueConversationButton.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(continueConversationButton);
                    }
                }
            }
        }
        else
        {
            continueButton.SetActive(false);
            continueConversationButton.SetActive(false);
        }
    }

    IEnumerator Dialogue()
    {
        talking = false;
        currentlyTalking = true;


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
                endButton.SetActive(true);
                //continueButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(endButton);
            }
            else
            {
                talking = true;
                currentlyTalking = false;
            }
        }
        else
        {
            if (!quest.isComplete)
            {
                foreach (char letter in questSentences[questIndex].ToCharArray())
                {
                    textDisplay.text += letter;

                    yield return new WaitForSeconds(typingSpeed);

                }
                if (questIndex == questSentences.Length - 1)
                {
                    talking = false;
                    //endTalkButton.SetActive(true);
                    continueButton.SetActive(false);
                    //EventSystem.current.SetSelectedGameObject(null);
                    //EventSystem.current.SetSelectedGameObject(endTalkButton);
                }
                else
                {
                    talking = true;
                    currentlyTalking = false;
                }
            }
            else
            {
                foreach (char letter in questCompleteSentences[questIndex].ToCharArray())
                {
                    textDisplay.text += letter;

                    yield return new WaitForSeconds(typingSpeed);

                }
                if (questIndex == questCompleteSentences.Length - 1)
                {
                    talking = false;
                    //endTalkButton.SetActive(true);
                    continueButton.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(null);
                    // EventSystem.current.SetSelectedGameObject(endTalkButton);
                }
                else
                {
                    talking = true;
                    currentlyTalking = false;
                }
            }
        }
    }

    public void ContinueConversationBool(bool _on)
    {
        continueConversation = _on;
    }

    public void NextSentence()
    {
        if (continueConversation && conversationCountBeforeMove != 0)
        {
            conversationCountBeforeMove--;
        }

        continueButton.SetActive(false);
        continueConversationButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = string.Empty;
            StartCoroutine(Dialogue());
            continueButton.SetActive(false);
        }
    }

    public void ReturnMovement()
    {
        Engine.e.inBattle = false;
    }

    public void SetAmountUntilConversationContinuesMovement(int _amount)
    {
        conversationCountBeforeMove = _amount;
    }

    public void EndDialogue()
    {
        GetComponent<PlayableDirector>().Resume();
        textDisplay.text = string.Empty;
        textDisplay.text = sentences[index];

        index = 0;
        questIndex = 0;

        dialogueScreen.SetActive(false);
        continueButton.SetActive(false);
        endButton.SetActive(false);

    }
}
