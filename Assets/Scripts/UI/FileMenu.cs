using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FileMenu : MonoBehaviour
{
    public FileSlot[] saveSlots;

    public bool saving, loading, deleting, saveMenuSet;
    public int indexReference;
    public int saveSlotsPointerIndex = 0, vertMove = 0;
    public RectTransform saveMenuRectTransform;
    bool pressUp, pressDown, pressRelease = false;
    public GameObject newGameButton, saveConfirmButton, saveDenyButton;

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
    void HandleSaveSlots()
    {
        // "Pause Menu" Inventory
        if (saveMenuSet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (saveSlotsPointerIndex < saveSlots.Length - 1)
                {
                    saveSlotsPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);

                    if (saveSlotsPointerIndex > 5 && saveSlotsPointerIndex < saveSlots.Length)
                    {
                        saveMenuRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (saveSlotsPointerIndex > 0)
                {
                    saveSlotsPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);


                    // if (saveSlotsPointerIndex >= 5 && saveSlotsPointerIndex > 0)
                    // {
                    //     saveMenuRectTransform.offsetMax -= new Vector2(0, 30);
                    // }
                }
            }
        }
    }
    public void HandleFile()
    {
        if (saving)
        {
            saveConfirmButton.SetActive(false);
            saveDenyButton.SetActive(false);
            Engine.e.SaveGame(saveSlotsPointerIndex);
            Engine.e.helpText.text = "Save Complete!";
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);
        }

        if (loading)
        {
            saveConfirmButton.SetActive(false);
            saveDenyButton.SetActive(false);

            Engine.e.LoadGame(saveSlotsPointerIndex);

            Engine.e.helpText.text = string.Empty;
            EventSystem.current.SetSelectedGameObject(null);
            loading = false;
        }

        if (deleting)
        {
            saveConfirmButton.SetActive(false);
            saveDenyButton.SetActive(false);
            deleting = false;
            saving = true;
            saveSlots[saveSlotsPointerIndex].ClearSave();
            SaveSystem.DeleteFile(saveSlotsPointerIndex);
            Engine.e.helpText.text = "File + " + (saveSlotsPointerIndex + 1) + " Deleted.";
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);
        }
    }

    public void SaveGameCheck()
    {
        saveConfirmButton.SetActive(true);
        saveDenyButton.SetActive(true);
        Engine.e.helpText.text = "Save exists. Are you sure you want to save File " + (saveSlotsPointerIndex + 1) + "?";

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Engine.e.fileMenuReference.saveDenyButton);
    }

    public void LoadGameCheck()
    {
        saveConfirmButton.SetActive(true);
        saveDenyButton.SetActive(true);
        Engine.e.helpText.text = "Are you sure you want to load File " + (saveSlotsPointerIndex + 1) + "?";

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Engine.e.fileMenuReference.saveConfirmButton);
    }

    public void DeleteGameCheck()
    {
        saveConfirmButton.SetActive(true);
        saveDenyButton.SetActive(true);
        Engine.e.helpText.text = "Are you sure you want to delete File " + (saveSlotsPointerIndex + 1) + "?";

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Engine.e.fileMenuReference.saveConfirmButton);
    }

    public void DenySaveGame()
    {
        saveConfirmButton.SetActive(false);
        saveDenyButton.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);
        Engine.e.helpText.text = string.Empty;

    }

    public void OpenFileMenuSaving()
    {

        saveSlotsPointerIndex = 0;
        saveMenuRectTransform.offsetMax = new Vector2(0, 0);
        saving = true;
        loading = false;
        deleting = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);
    }
    public void OpenFileMenuLoading()
    {

        saveSlotsPointerIndex = 0;
        saveMenuRectTransform.offsetMax = new Vector2(0, 0);
        saving = false;
        loading = true;
        deleting = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);

    }

    public void OpenFileMenuDeleting()
    {

        saveSlotsPointerIndex = 0;
        saveMenuRectTransform.offsetMax = new Vector2(0, 0);
        saving = false;
        loading = false;
        deleting = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(saveSlots[saveSlotsPointerIndex].gameObject);

    }

    public void AppropriateBackButton()
    {
        if (loading)
        {
            Engine.e.mainMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(newGameButton);
        }
        else
        {
            Engine.e.canvasReference.GetComponent<PauseMenu>().OpenPauseMenu();
        }

        Engine.e.helpText.text = string.Empty;
        saving = false;
        loading = false;
    }

    void Update()
    {

        if (saveMenuSet)
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

            HandleSaveSlots();
        }
    }
}
