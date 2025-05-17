using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LeerDatosBinarios : MonoBehaviour
{

    void Start()
    {
        Debug.Log("Ruta de datos persistentes: " + Application.persistentDataPath);

        string path = Application.persistentDataPath + "/GhostNGoblinsGameData.dat";

        // Comprova si el fitxer existeix
        if (File.Exists(path))
        {
            // Obre el fitxer en mode lectura binària
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                int highScore = reader.ReadInt32();
                highScore >>= 16;// Desplaça els 16 bits superiors cap a la dreta

                // Fa operacions bit a bit per descompondre i recompondre bytes
                int word = (highScore & 0xFFFF);
                int hword = (word & 0x00FF) << 8;
                int lword = (word & 0xFF00) >> 8;
                int res = hword | lword;

                Debug.Log("highScore: " + res);// Mostra el valor del highScore processat

                Debug.Log("Ruta de datos persistentes: " + Application.persistentDataPath);
                Debug.Log("Archivo buscado: " + path);
            }
        }
        else
        {
            Debug.LogWarning("Archivo .dat no encontrado: " + path);
        }
    }
}
