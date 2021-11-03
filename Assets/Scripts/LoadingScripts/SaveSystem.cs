using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(Engine gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(gameManager);

        formatter.Serialize(stream, gameData);
        stream.Close();

        Debug.Log("Saved!");
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/game.save";
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
}
