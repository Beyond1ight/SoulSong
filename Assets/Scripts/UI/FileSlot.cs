using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class FileSlot : MonoBehaviour
{
    public GameData gameData;
    public GameObject saveName, saveLvl, saveLocation;
    public int index;
    public GameObject scrollReference;

    public void AddSave(GameData _gameData)
    {
        gameData = _gameData;

        saveName.GetComponent<TMP_Text>().text = gameData.charNames[0];
        saveLvl.GetComponent<TMP_Text>().text = "Lvl: " + gameData.charLvl[0];
        saveLocation.GetComponent<TMP_Text>().text = gameData.scene;
    }

    // Currently only handles menuSet
    public void SetHelpTextFile()
    {
        if (!Engine.e.fileMenuReference.saveMenuSet)
        {
            Engine.e.fileMenuReference.saveMenuSet = true;
        }
    }

    public void ClearHelpTextFile()
    {
        if (Engine.e.fileMenuReference.saveMenuSet)
        {
            Engine.e.fileMenuReference.saveMenuSet = false;
        }
    }


    public void OnClickEvent()
    {

        if (Engine.e.fileMenuReference.saving)
        {
            if (Engine.e.ableToSave)
            {
                if (Engine.e.fileMenuReference.saveSlotsPointerIndex != 3)
                {
                    if (gameData.charNames.Length != 0)
                    {
                        Engine.e.fileMenuReference.SaveGameCheck();
                    }
                    else
                    {
                        Engine.e.fileMenuReference.HandleFile();
                    }
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            if (gameData.charNames.Length != 0)
            {
                Engine.e.fileMenuReference.LoadGameCheck();
            }
        }
    }
}
