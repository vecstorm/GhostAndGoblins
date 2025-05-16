using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameData : MonoBehaviour
{
    public static PlayerInfoSerialized gameData;

    public static void SaveDataInfo(PlayerInfoSerialized game)
    {
        string json = JsonUtility.ToJson(game);
        File.WriteAllText(Application.persistentDataPath + "GameData.txt", json);
        Debug.Log(Application.persistentDataPath);
        Debug.Log("Partida guardada en la BBDD");




    }

    public static void SaveDataBinary(PlayerInfoSerialized game)
    {
        string path = Application.persistentDataPath + "/GhostNGoblinsGameData.dat";

        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(game.highScore);
        }

        Debug.Log("Guardado en binario en: " + path);
    }
}
