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

    public void ClearSave()
    {
        gameData = null;

        saveName.GetComponent<TMP_Text>().text = string.Empty;
        saveLvl.GetComponent<TMP_Text>().text = string.Empty;
        saveLocation.GetComponent<TMP_Text>().text = string.Empty;
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

        if (Engine.e.fileMenuReference.loading)
        {
            if (gameData.charNames.Length != 0)
            {
                Engine.e.fileMenuReference.LoadGameCheck();
            }
            else
            {
                return;
            }
        }

        if (Engine.e.fileMenuReference.deleting)
        {
            if (gameData.charNames.Length != 0)
            {
                Engine.e.fileMenuReference.DeleteGameCheck();
            }
            else
            {
                return;
            }
        }
    }
}
