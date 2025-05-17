using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameData : MonoBehaviour
{
    public static PlayerInfoSerialized gameData;

    public static void SaveDataInfo(PlayerInfoSerialized game)
    {
        string json = JsonUtility.ToJson(game);// Converteix l'objecte game a una cadena JSON
        File.WriteAllText(Application.persistentDataPath + "GameData.txt", json);// Desa la cadena JSON en un fitxer de text dins la ruta persistent del sistema


        Debug.Log(Application.persistentDataPath);
        Debug.Log("Partida guardada en la BBDD");

    }

    public static void SaveDataBinary(PlayerInfoSerialized game)
    {
        string path = Application.persistentDataPath + "/GhostNGoblinsGameData.dat";// Ruta completa on es desarà l'arxiu binari

         // Crea i obre el fitxer per escriure-hi dades binàries
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(game.highScore);// Escriu el valor de la puntuació més alta (highScore)
        }

        Debug.Log("Guardado en binario en: " + path);
    }
}
