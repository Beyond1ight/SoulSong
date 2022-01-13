using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public static class SaveSystem
{
    public static void SaveGame(Engine gameManager, int saveSlot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game" + saveSlot + ".save";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(gameManager);

        Engine.e.fileMenuReference.saveSlots[saveSlot].AddSave(gameData);
        formatter.Serialize(stream, gameData);
        stream.Close();

        Debug.Log("Saved!");
    }

    public static void CheckFilesForDisplay()
    {

        for (int i = 0; i < Engine.e.fileMenuReference.saveSlots.Length; i++)
        {
            string path = Application.persistentDataPath + "/game" + i + ".save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameData gameData = formatter.Deserialize(stream) as GameData;
                stream.Close();

                Engine.e.fileMenuReference.saveSlots[i].AddSave(gameData);
                Engine.e.fileMenuReference.saveSlots[i].saveName.GetComponent<TMP_Text>().text = gameData.charNames[0];
                Engine.e.fileMenuReference.saveSlots[i].saveLvl.GetComponent<TMP_Text>().text = "Lvl: " + gameData.charLvl[0].ToString();
                Engine.e.fileMenuReference.saveSlots[i].saveLocation.GetComponent<TMP_Text>().text = gameData.scene;

                Engine.e.saveExists = true;
            }
            else
            {
                Engine.e.fileMenuReference.saveSlots[i].saveName.GetComponent<TMP_Text>().text = string.Empty;
                Engine.e.fileMenuReference.saveSlots[i].saveLvl.GetComponent<TMP_Text>().text = string.Empty;
                Engine.e.fileMenuReference.saveSlots[i].saveLocation.GetComponent<TMP_Text>().text = string.Empty;
            }
        }
    }

    public static GameData LoadGame(int saveSlot)
    {
        string path = Application.persistentDataPath + "/game" + saveSlot + ".save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();

            Debug.Log("Load complete!");

            return gameData;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteFile(int saveSlot)
    {
        string path = Application.persistentDataPath + "/game" + saveSlot + ".save";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
