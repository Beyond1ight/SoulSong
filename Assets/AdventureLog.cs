using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AdventureLog : MonoBehaviour
{
    public Quest[] questLog, completedQuestLog;
    public QuestSlot[] questSlots;
    public QuestCompleteSlot[] completedQuestSlots;
    public TextMeshProUGUI questDescription;
    public bool adventureLogScreenSet, adventureLogScreenCompletedQuestsSet;
    public int indexReference;
    public int questLogPointerIndex = 0, vertMove = 0;
    public RectTransform questLogRectTransform, completedQuestLogRectTransform;
    bool pressUp, pressDown, pressRelease = false;
    public GameObject currentQuestList, completedQuestList;

    public void AddQuestToAdventureLog(Quest _quest)
    {
        for (int i = 0; i < questLog.Length; i++)
        {
            if (questLog[i] == null)
            {
                questLog[i] = _quest;
                _quest.inAdventureLog = true;
                questSlots[i].AddQuest(_quest);
                break;
            }
        }
    }

    public void AddQuestToCompleteQuestLog(Quest _quest)
    {
        for (int i = 0; i < questLog.Length; i++)
        {
            if (questLog[i] == _quest)
            {
                questLog[i] = null;
                questSlots[i].ClearSlot();
                break;
            }
        }

        for (int i = 0; i < completedQuestLog.Length; i++)
        {
            if (completedQuestLog[i] == null)
            {
                completedQuestLog[i] = _quest;
                completedQuestSlots[i].AddQuest(_quest);
                break;
            }
        }
    }

    public void OpenAdventureLogMenu()
    {
        currentQuestList.SetActive(true);
        completedQuestList.SetActive(false);

        questLogPointerIndex = 0;
        questLogRectTransform.offsetMax = new Vector2(0, 0);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(questSlots[questLogPointerIndex].gameObject);

    }
    public void OpenAdventureLogCompletedQuestsList()
    {
        currentQuestList.SetActive(false);
        completedQuestList.SetActive(true);

        questLogPointerIndex = 0;
        completedQuestLogRectTransform.offsetMax = new Vector2(0, 0);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(completedQuestSlots[questLogPointerIndex].gameObject);

    }


    void HandleInventory()
    {
        // "Pause Menu" Inventory
        if (adventureLogScreenSet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;

                if (questLogPointerIndex < questLog.Length)
                {
                    questLogPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(questSlots[questLogPointerIndex].gameObject);

                    if (questLogPointerIndex > 5 && questLogPointerIndex < questLog.Length)
                    {
                        questLogRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (questLogPointerIndex > 0)
                {
                    questLogPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(questSlots[questLogPointerIndex].gameObject);


                    if (questLogPointerIndex >= 5 && questLogPointerIndex > 0)
                    {
                        questLogRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }

        if (adventureLogScreenCompletedQuestsSet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;

                if (questLogPointerIndex < questLog.Length)
                {
                    questLogPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(completedQuestSlots[questLogPointerIndex].gameObject);

                    if (questLogPointerIndex > 5 && questLogPointerIndex < completedQuestLog.Length)
                    {
                        completedQuestLogRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (questLogPointerIndex > 0)
                {
                    questLogPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(completedQuestSlots[questLogPointerIndex].gameObject);


                    if (questLogPointerIndex >= 5 && questLogPointerIndex > 0)
                    {
                        completedQuestLogRectTransform.offsetMax -= new Vector2(0, 30);
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

    void Update()
    {

        if (adventureLogScreenSet || adventureLogScreenCompletedQuestsSet)
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